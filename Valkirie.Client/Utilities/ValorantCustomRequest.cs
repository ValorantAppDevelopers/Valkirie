using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using static ValorantNET.Enums;

namespace Valkirie.Client.Utilities
{
    //TODO To export on ValorantNET library
    public class ValorantCustomRequest
    {
        private string username;
        private string password;
        private Regions region;
        private string endpoint;
        private HttpClient session;

        public event EventHandler<PlayerDTO> loginReceived;

        public ValorantCustomRequest(string username, string password, Regions region)
        {
            this.username = username;
            this.password = password;
            this.region = region;
            this.endpoint = $"https://pd.{region.ToString()}.a.pvp.net/";
            session = new HttpClient();

            UserInfo();
        }

        private string GetAuthorization()
        {
            dynamic data = new JObject();

            data.client_id = "play-valorant-web-prod";
            data.nonce = "1";
            data.redirect_uri = "https://playvalorant.com/opt_in";
            data.response_type = "token id_token";

            var result = Post<dynamic>("https://auth.riotgames.com/api/v1/authorization", data).type;
            return result;
        }

        private UserParameters GetToken()
        {
            dynamic userData = new JObject();

            userData.type = GetAuthorization();
            userData.username = username;
            userData.password = password;

            var result = Put<UserParameters>("https://auth.riotgames.com/api/v1/authorization", userData);
            string uri = result.response.parameters.uri;
            char[] separators = { '#', '&' };
            var authParameterResponse = uri.Split(separators);

            session.DefaultRequestHeaders.Add("Authorization", "Bearer " + authParameterResponse[1].Replace("access_token=",""));

            return result;
        }

        private string GetEntitlementsToken()
        {
            GetToken();
            string result = Post<dynamic>("https://entitlements.auth.riotgames.com/api/token/v1", new JObject().ToString()).entitlements_token;

            session.DefaultRequestHeaders.Add("X-Riot-Entitlements-JWT", result);

            return result;
        }

        private void UserInfo()
        {
            var task = new Task(async () =>
            {
                GetEntitlementsToken();
                dynamic data = new JObject();
                string playerId = Post<dynamic>($"https://auth.riotgames.com/userinfo", data).sub;
                data = "[\"" + playerId + "\"]";
                List<PlayerDTO> result = Put<List<PlayerDTO>>($"{endpoint}name-service/v2/players", data);
                loginReceived?.Invoke(this, result.FirstOrDefault());
            });
            task.Start();
        }

        #region HttpMethods
        private T Post<T>(string url, dynamic data)
        {
            var result = session.PostAsync(url, new StringContent(data.ToString(), Encoding.UTF8, "application/json")).Result;

            if (result.IsSuccessStatusCode)
            {
                var resultString = result.Content.ReadAsStringAsync().Result;
                return JsonConvert.DeserializeObject<T>(resultString);
            }
            else
            {
                if (result.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                    throw new Exception();
                throw new Exception("POST Call failure");
            }
        }

        private T Get<T>(string url)
        {
            var result = session.GetAsync(url).Result;
            if (result.IsSuccessStatusCode)
            {
                var resultString = result.Content.ReadAsStringAsync().Result;
                return JsonConvert.DeserializeObject<T>(resultString);
            }
            else
            {
                if (result.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                    throw new Exception();
                throw new Exception("GET Call failure");
            }
        }

        private T Put<T>(string url, dynamic data)
        {
            var result = session.PutAsync(url, new StringContent(data.ToString(), Encoding.UTF8, "application/json")).Result;

            if (result.IsSuccessStatusCode)
            {
                var resultString = result.Content.ReadAsStringAsync().Result;
                return JsonConvert.DeserializeObject<T>(resultString);
            }
            else
            {
                if (result.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                    throw new Exception();
                throw new Exception("PUT Call failure");
            }
        }
        #endregion

        public class UserParameters
        {
            public string type { get; set; }
            public Response response { get; set; }
            public string country { get; set; }

            public class Response
            {
                public string mode { get; set; }
                public Parameters parameters { get; set; }
            }

            public class Parameters
            {
                public string uri { get; set; }
            }

        }

        public class PlayerDTO
        {
            public string DisplayName { get; set; }
            [JsonProperty("Subject")]
            public string PlayerId { get; set; }
            public string GameName { get; set; }
            public string TagLine { get; set; }
        }
    }
}
