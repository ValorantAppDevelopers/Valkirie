using MahApps.Metro.Controls;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text;
using Valkirie.Client.Components.Window.LoginPopup;
using Valkirie.Client.Utilities;

namespace Valkirie.Client
{
    public class MainWindowViewModel : INotifyPropertyChanged
    {
        private AppManager appManager;
        private LoginView loginView;

        #region Prop
        private ObservableCollection<HamburgerMenuGlyphItem> _hamburgerMenuGlyphItems;

        public ObservableCollection<HamburgerMenuGlyphItem> HamburgerMenuGlyphItems
        {
            get => _hamburgerMenuGlyphItems;

            set
            {
                if (_hamburgerMenuGlyphItems != value)
                {
                    _hamburgerMenuGlyphItems = value;
                    NotifyPropertyChanged(nameof(HamburgerMenuGlyphItems));
                }
            }
        }
        #endregion

        #region Ctr
        public MainWindowViewModel(AppManager appManager)
        {
            this.appManager = appManager;
            loginView = new LoginView(appManager);
            loginView.ShowDialog();

            HamburgerMenuGlyphItems = new ObservableCollection<HamburgerMenuGlyphItem>();

            HamburgerMenuGlyphItems.Add(new HamburgerMenuGlyphItem()
            {
                Label = "Overview",
                Tag = "overview",
                Glyph = "InformationCircle" //Use icon as Glyph beacuse it is a string Type
            });
            HamburgerMenuGlyphItems.Add(new HamburgerMenuGlyphItem()
            {
                Label = "Rank",
                Tag = "rank",
                Glyph = "Graphline" //Use icon as Glyph beacuse it is a string Type
            });
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
