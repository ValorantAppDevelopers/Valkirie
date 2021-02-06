using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
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

        public ValorantCustomRequest(string username, string password, Regions region)
        {
            this.username = username;
            this.password = password;
            this.region = region;
            this.endpoint = $"https://pd.{region.ToString()}.a.pvp.net/";

            GetToken();
        }

        private string GetAuthorization()
        {
            dynamic data = new JObject();

            data.client_id = "play-valorant-web-prod";
            data.nonce = "1";
            data.redirect_uri = "https://playvalorant.com/opt_in";
            data.response_type = "token id_token";

            return Post<dynamic>("https://auth.riotgames.com/api/v1/authorization", data).type;
        }

        private UserParameters GetToken()
        {
            dynamic userData = new JObject();

            userData.type = GetAuthorization();
            userData.username = username;
            userData.password = password;
            
            var result = Put<UserParameters>("https://auth.riotgames.com/api/v1/authorization", userData);
            return result;
        }

        #region HttpMethods
        private T Post<T>(string url, dynamic data)
        {
            using(var session = new HttpClient())
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
        }
        private T Get<T>(string url)
        {
            using (var session = new HttpClient())
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
        }

        private T Put<T>(string url, dynamic data)
        {
            using (var session = new HttpClient())
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
    }
}
