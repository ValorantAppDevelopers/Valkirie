using System;
using System.Collections.Generic;
using System.ComponentModel;
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

namespace Valkirie.Client.CustomControls
{
    /// <summary>
    /// Interaction logic for StatsViewPanel.xaml
    /// </summary>
    public partial class StatsViewPanel : UserControl
    {
        public StatsViewPanel()
        {
            InitializeComponent();
            DataContext = this;
        }

        public static DependencyProperty TitleProp =
            DependencyProperty.Register("Title", typeof(string), typeof(StatsViewPanel));

        public static DependencyProperty ValueProp =
            DependencyProperty.Register("Value", typeof(string), typeof(StatsViewPanel));

        public string Title
        {
            get { return (string)GetValue(TitleProp); }
            set { SetValue(TitleProp, value); }
        }

        public object Value
        {
            get { return (string)GetValue(ValueProp); }
            set { SetValue(ValueProp, value); }
        }
    }
}
