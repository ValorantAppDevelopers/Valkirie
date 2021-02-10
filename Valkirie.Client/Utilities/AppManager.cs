using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using ValorantNET;
using static Valkirie.Client.Utilities.UserConfig;

namespace Valkirie.Client.Utilities
{
    public class AppManager
    {
        private string username;
        private string tag;
        private string uuid;
        private bool isLoading;

        public event EventHandler<dynamic> propertyChanged;

        public UserConfigXml UserConfigApplication;
        public ValorantClient ValorantClient;
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

        public void NotifyPropertyChanged(string propertyname, dynamic attribute)
        {
            propertyChanged?.Invoke(propertyname, attribute);
        }
    }
}
