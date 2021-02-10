using MahApps.Metro.Controls;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Valkirie.Client.Components.View.Overview;
using Valkirie.Client.Components.View.Rank;
using Valkirie.Client.Components.Window.LoginPopup;
using Valkirie.Client.Utilities;

namespace Valkirie.Client
{
    public class MainWindowViewModel : INotifyPropertyChanged
    {
        private AppManager appManager;
        private MainWindow mainWindow;
        private LoginView loginView;
        private OverviewView overviewView;
        private RankView rankView;

        #region Prop
        private ObservableCollection<HamburgerMenuGlyphItem> hamburgerMenuGlyphItems;
        private ObservableCollection<HamburgerMenuGlyphItem> hamburgerMenuGlyphItemsOption;
        private HamburgerMenuGlyphItem selectedPage;
        private HamburgerMenuGlyphItem selectedOption;

        public string Username => appManager.Username;
        public string Tag => appManager.Tag;
        public bool IsLoading => appManager.IsLoading;

        public ObservableCollection<HamburgerMenuGlyphItem> HamburgerMenuGlyphItems
        {
            get => hamburgerMenuGlyphItems;

            set
            {
                if (hamburgerMenuGlyphItems != value)
                {
                    hamburgerMenuGlyphItems = value;
                    NotifyPropertyChanged(nameof(HamburgerMenuGlyphItems));
                }
            }
        }
        
        public ObservableCollection<HamburgerMenuGlyphItem> HamburgerMenuGlyphItemsOption
        {
            get => hamburgerMenuGlyphItemsOption;

            set
            {
                if (hamburgerMenuGlyphItemsOption != value)
                {
                    hamburgerMenuGlyphItemsOption = value;
                    NotifyPropertyChanged(nameof(HamburgerMenuGlyphItemsOption));
                }
            }
        }

        public HamburgerMenuGlyphItem SelectedPage
        {
            get => selectedPage;

            set
            {
                if (selectedPage != value)
                {
                    selectedPage = value;
                    NotifyPropertyChanged(nameof(SelectedPage));
                    ChangeView(SelectedPage);
                }
            }
        }

        public HamburgerMenuGlyphItem SelectedOption
        {
            get => selectedOption;

            set
            {
                if (selectedOption != value)
                {
                    selectedOption = value;
                    NotifyPropertyChanged(nameof(SelectedOption));
                    ChangeView(SelectedOption);
                }
            }
        }
        #endregion

        #region Ctr
        public MainWindowViewModel(AppManager appManager, MainWindow mainWindow)
        {
            this.appManager = appManager;
            this.mainWindow = mainWindow;

            appManager.propertyChanged += AppManager_propertyChanged;
            LoginViewButton.Execute(null);

            HamburgerMenuGlyphItems = new ObservableCollection<HamburgerMenuGlyphItem>();

            HamburgerMenuGlyphItems.Add(new HamburgerMenuGlyphItem()
            {
                Label = "Overview",
                Tag = "overview",
                TargetPageType = new OverviewView(appManager).GetType(),
                Glyph = "InformationCircle" //Use icon as Glyph beacuse it is a string Type
            });
            HamburgerMenuGlyphItems.Add(new HamburgerMenuGlyphItem()
            {
                Label = "Rank",
                Tag = "rank",
                Glyph = "Graphline" //Use icon as Glyph beacuse it is a string Type
            });

            HamburgerMenuGlyphItemsOption = new ObservableCollection<HamburgerMenuGlyphItem>();

            HamburgerMenuGlyphItemsOption.Add(new HamburgerMenuGlyphItem()
            {
                Label = Username + "#" + Tag,
                Tag = "login",
                Glyph = "User",
                Command = LoginViewButton
            });

            ChangeView(HamburgerMenuGlyphItems.First());
        }
        #endregion

        #region Methods
        public void ChangeView(HamburgerMenuGlyphItem item)
        {
            switch (item?.Tag)
            {
                case "overview":
                    if (overviewView == null)
                        overviewView = new OverviewView(appManager);
                    mainWindow.HamburgerMenuControl.Content = (overviewView);
                    break;
                case "rank":
                    if (rankView == null)
                        rankView = new RankView(appManager);
                    mainWindow.HamburgerMenuControl.Content = (rankView);
                    break;
                default:
                    break;
            }
        }
        #endregion

        #region Commands
        private ICommand loginViewButton;
        public ICommand LoginViewButton
        {
            get
            {
                if (loginViewButton == null)
                {
                    loginViewButton = new RelayCommands(obj =>
                    {
                        loginView = new LoginView(appManager);
                        loginView.ShowDialog();

                        if (appManager.UUID == null)
                            Application.Current.Shutdown();

                        SelectedOption = null;
                    }, obj => true);
                }
                return loginViewButton;
            }
        }
        #endregion

        #region Events
        private void AppManager_propertyChanged(object sender, dynamic e)
        {
            if (sender.ToString() == nameof(appManager.Username))
            {
                NotifyPropertyChanged(nameof(Username));
            }
            
            if (sender.ToString() == nameof(appManager.IsLoading))
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
