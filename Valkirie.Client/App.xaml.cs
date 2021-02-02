using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using ControlzEx.Theming;

namespace Valkirie.Client
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            //TODO Switching theme
            ThemeManager.Current.AddTheme(RuntimeThemeGenerator.Current.GenerateRuntimeTheme("Dark", Colors.Red));
            ThemeManager.Current.AddTheme(RuntimeThemeGenerator.Current.GenerateRuntimeTheme("Light", Colors.Red));

            ThemeManager.Current.ChangeTheme(this, "Dark.Red");
        }
    }
}
