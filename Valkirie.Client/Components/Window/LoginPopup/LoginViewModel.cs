using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Input;
using Valkirie.Client.Utilities;
using static Valkirie.Client.Utilities.ValorantCustomRequest;
using static ValorantNET.Enums;

namespace Valkirie.Client.Components.Window.LoginPopup
{
    public class LoginViewModel : INotifyPropertyChanged
    {
        private LoginView loginView;
        private AppManager appManager;
        private ValorantCustomRequest valorantCustomRequest;

        #region Prop
        private string username;
        private string password;
        private Dictionary<string,Regions> regions;
        private string selectedRegion;
        private bool staySignedChecked;
        private Visibility isPasswordVisible = Visibility.Visible;
        private Visibility isPasswordTextVisible = Visibility.Collapsed;

        public bool IsLoading => appManager.IsLoading;
        public bool LoginEnable => !string.IsNullOrWhiteSpace(Username) && !string.IsNullOrWhiteSpace(Password) && !string.IsNullOrWhiteSpace(SelectedRegion) && !IsLoading;
        public Visibility IsPasswordVisible
        {
            get => isPasswordVisible;

            set
            {
                if (isPasswordVisible != value)
                {
                    isPasswordVisible = value;
                    NotifyPropertyChanged(nameof(IsPasswordVisible));
                }
            }
        }

        public Visibility IsPasswordTextVisible
        {
            get => isPasswordTextVisible;

            set
            {
                if (isPasswordTextVisible != value)
                {
                    isPasswordTextVisible = value;
                    NotifyPropertyChanged(nameof(IsPasswordTextVisible));
                }
            }
        }

        public string Username
        {
            get => username;

            set
            {
                if (username != value)
                {
                    username = value;
                    NotifyPropertyChanged(nameof(Username));
                    NotifyPropertyChanged(nameof(LoginEnable));
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
                    NotifyPropertyChanged(nameof(LoginEnable));
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
                    NotifyPropertyChanged(nameof(LoginEnable));
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

            appManager.propertyChanged += AppManager_propertyChanged;

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
                        appManager.IsLoading = true;
                        NotifyPropertyChanged(nameof(LoginEnable));
                        valorantCustomRequest = new ValorantCustomRequest(Username, Password, ValorantNET.Enums.Regions.EU);
                        valorantCustomRequest.loginReceived += ValorantCustomRequest_loginReceived;
                        //this.loginView.Close();
                    }, obj => true);
                }
                return loginButton;
            }
        }

        private void ValorantCustomRequest_loginReceived(object sender, PlayerDTO e)
        {
            if (!string.IsNullOrEmpty(e?.PlayerId))
            {
                appManager.Username = e.DisplayName;
                appManager.UUID = e.PlayerId;
                appManager.Tag = e.TagLine;

                appManager.IsLoading = false;

                Application.Current.Dispatcher.Invoke(() =>
                {
                    this.loginView.Close();
                });
            }
        }
        #endregion

        #region Events
        private void AppManager_propertyChanged(object sender, dynamic e)
        {
            if(sender.ToString() == nameof(appManager.IsLoading))
            {
                NotifyPropertyChanged(nameof(IsLoading));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void NotifyPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion
    }
}
