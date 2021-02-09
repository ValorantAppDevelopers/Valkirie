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
using System.Windows.Navigation;
using System.Windows.Shapes;
using Valkirie.Client.Components.Page.Overview;
using Valkirie.Client.Utilities;

namespace Valkirie.Client.Components.View.Overview
{
    /// <summary>
    /// Interaction logic for OverviewView.xaml
    /// </summary>
    public partial class OverviewView : MetroContentControl
    {
        private OverviewViewModel overviewViewModel;
        public OverviewView(AppManager appManager)
        {
            InitializeComponent();
            overviewViewModel = new OverviewViewModel(appManager);
            DataContext = overviewViewModel;
        }
    }
}
