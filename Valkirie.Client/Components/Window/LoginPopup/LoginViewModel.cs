using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Windows.Input;
using Valkirie.Client.Utilities;
using static ValorantNET.Enums;

namespace Valkirie.Client.Components.Window.LoginPopup
{
    public class LoginViewModel : INotifyPropertyChanged
    {
        private LoginView loginView;
        private AppManager appManager;

        #region Prop
        private string username;
        private string password;
        private Dictionary<string,Regions> regions;
        private string selectedRegion;
        private bool staySignedChecked;

        public bool LoginEnable => true;
        public string Username
        {
            get => username;

            set
            {
                if (username != value)
                {
                    username = value;
                    NotifyPropertyChanged(nameof(Username));
                }
            }
        }

        public string Password
        {
            get => password;

            set
            {
                if (password != value)
                {
                    password = value;
                    NotifyPropertyChanged(nameof(Password));
                }
            }
        }

        public Dictionary<string, Regions> Regions
        {
            get => regions;

            set
            {
                if (regions != value)
                {
                    regions = value;
                    NotifyPropertyChanged(nameof(Regions));
                }
            }
        }

        public string SelectedRegion
        {
            get => selectedRegion;

            set
            {
                if (selectedRegion != value)
                {
                    selectedRegion = value;
                    NotifyPropertyChanged(nameof(SelectedRegion));
                }
            }
        }

        public bool StaySignedChecked
        {
            get => staySignedChecked;

            set
            {
                if (staySignedChecked != value)
                {
                    staySignedChecked = value;
                    NotifyPropertyChanged(nameof(StaySignedChecked));
                }
            }
        }
        #endregion

        #region Ctr
        public LoginViewModel(LoginView loginView, AppManager appManager)
        {
            this.loginView = loginView;
            this.appManager = appManager;

            RegisterRegions();
        }
        #endregion

        #region Methods
        private void RegisterRegions()
        {
            Regions = new Dictionary<string, Regions>();
            foreach (Regions region in (Regions[])Enum.GetValues(typeof(Regions)))
            {
                Regions.Add(region.ToString(), region);
            }
        }
        #endregion

        #region Commands
        private ICommand loginButton;
        public ICommand LoginButton
        {
            get
            {
                if(loginButton == null)
                {
                    loginButton = new RelayCommands(obj =>
                    {
                        var vc = new ValorantCustomRequest(Username, Password, ValorantNET.Enums.Regions.EU);
                        //this.loginView.Close();
                    }, obj => true);
                }
                return loginButton;
            }
        }
        #endregion

        #region Ctr
        public event PropertyChangedEventHandler PropertyChanged;
        public void NotifyPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion
    }
}
