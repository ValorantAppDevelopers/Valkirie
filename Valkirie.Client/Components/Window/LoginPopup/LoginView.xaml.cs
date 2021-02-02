using MahApps.Metro.Controls;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Valkirie.Client.Utilities;

namespace Valkirie.Client.Components.Window.LoginPopup
{
    /// <summary>
    /// Interaction logic for LoginView.xaml
    /// </summary>
    public partial class LoginView : MetroWindow
    {
        private LoginViewModel loginViewModel;
        private AppManager appManager;

        public LoginView(AppManager appManager)
        {
            InitializeComponent();
            this.appManager = appManager;
            loginViewModel = new LoginViewModel(this, appManager);
            DataContext = loginViewModel;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }

        private void password_PasswordChanged(object sender, RoutedEventArgs e)
        {

        }
    }
}
