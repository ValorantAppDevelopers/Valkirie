using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Windows.Input;
using Valkirie.Client.Utilities;

namespace Valkirie.Client.Components.Window.LoginPopup
{
    public class LoginViewModel : INotifyPropertyChanged
    {
        private LoginView loginView;
        private AppManager appManager;

        #region Prop
        private string exampleText;

        public string ExampleText
        {
            get => exampleText;

            set
            {
                if (exampleText != value)
                {
                    exampleText = value;
                    NotifyPropertyChanged(nameof(ExampleText));
                }
            }
        }
        #endregion

        #region Ctr
        public LoginViewModel(LoginView loginView, AppManager appManager)
        {
            this.loginView = loginView;
            this.appManager = appManager;
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
                        this.loginView.Close();
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
