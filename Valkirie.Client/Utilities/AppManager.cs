using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Threading.Tasks;
using ValorantNET;
using static Valkirie.Client.Utilities.UserConfig;
using static ValorantNET.Enums;

namespace Valkirie.Client.Utilities
{
    public class AppManager
    {
        private string username;
        private string tag;
        private Regions region;
        private string uuid;
        private bool isLoading;
        private ValorantClient valorantClient;

        public event EventHandler<dynamic> propertyChanged;

        public UserConfigXml UserConfigApplication;
        public ValorantClient ValorantClient
        {
            get => valorantClient;
            set
            {
                if (valorantClient != value)
                {
                    valorantClient = value;
                    NotifyPropertyChanged(nameof(ValorantClient), ValorantClient);
                    GetUUID();
                }
            }
        }

        public string Username
        {
            get => username;
            set
            {
                if(username != value)
                {
                    username = value;
                    NotifyPropertyChanged(nameof(Username),Username);
                }
            }
        }

        public string Tag
        {
            get => tag;
            set
            {
                if(tag != value)
                {
                    tag = value;
                    NotifyPropertyChanged(nameof(Tag),Tag);
                }
            }
        }

        public Regions Region
        {
            get => region;

            set
            {
                if (region != value)
                {
                    region = value;
                    NotifyPropertyChanged(nameof(Region), Region);
                }
            }
        }

        public string UUID
        {
            get => uuid;
            set
            {
                if(uuid != value)
                {
                    uuid = value;
                    NotifyPropertyChanged(nameof(UUID), UUID);
                }
            }
        }

        public bool IsLoading
        {
            get => isLoading;
            set
            {
                if (isLoading != value)
                {
                    isLoading = value;
                    NotifyPropertyChanged(nameof(IsLoading), IsLoading);
                }
            }
        }

        public AppManager()
        {
            UserConfigApplication = GetUserConfig();
        }

        private void GetUUID()
        {
            var task = new Task(async () =>
            {
                var result = await ValorantClient.GetPUUIDAsync();
                UUID = result.data.puuid;
            });
            task.Start();
        }

        public void NotifyPropertyChanged(string propertyname, dynamic attribute)
        {
            propertyChanged?.Invoke(propertyname, attribute);
        }
    }
}
