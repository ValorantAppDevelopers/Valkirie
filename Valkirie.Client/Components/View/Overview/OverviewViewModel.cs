using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Windows.Input;
using Valkirie.Client.Utilities;
using static ValorantNET.Enums;

namespace Valkirie.Client.Components.Page.Overview
{
    public class OverviewViewModel : INotifyPropertyChanged
    {
        private AppManager appManager;
        private OverviewModel overviewModel;

        #region Prop
        private float kda;
        private float winPerc;
        private string playTime;
        private Dictionary<string, Regions> regions;
        private string selectedRegion;
        private Regions selectedRegionEnum;
        private string username;
        private string tag;

        public bool IsLoading => appManager.IsLoading;
        public bool IsRefreshEnable => !string.IsNullOrEmpty(appManager.UUID);
        public float KDA
        {
            get => kda;

            set
            {
                if(kda != value)
                {
                    kda = value;
                    NotifyPropertyChanged(nameof(KDA));
                }
            }
        }

        public float WinPerc
        {
            get => winPerc;

            set
            {
                if (winPerc != value)
                {
                    winPerc = value;
                    NotifyPropertyChanged(nameof(WinPerc));
                }
            }
        }

        public string PlayTime
        {
            get => playTime;

            set
            {
                if (playTime != value)
                {
                    playTime = value;
                    NotifyPropertyChanged(nameof(PlayTime));
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
                    SelectedRegionEnum = (Regions)Enum.Parse(typeof(Regions), SelectedRegion.Split(',')[1].Replace("]", ""));
                }
            }
        }

        public Regions SelectedRegionEnum
        {
            get => selectedRegionEnum;

            set
            {
                if (selectedRegionEnum != value)
                {
                    selectedRegionEnum = value;
                    NotifyPropertyChanged(nameof(SelectedRegionEnum));
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
                }
            }
        }

        public string Tag
        {
            get => tag;

            set
            {
                if (tag != value)
                {
                    tag = value;
                    NotifyPropertyChanged(nameof(Tag));
                }
            }
        }
        #endregion

        #region Ctr
        public OverviewViewModel(AppManager appManager)
        {
            this.appManager = appManager;
            appManager.propertyChanged += AppManager_propertyChanged;
            RegisterRegions();
            this.overviewModel = new OverviewModel(this.appManager);
            this.overviewModel.statsReceived += OverviewModel_statsReceived;
        }
        #endregion

        #region Commands
        private ICommand refresh;
        public ICommand Refresh
        {
            get
            {
                if (refresh == null)
                {
                    refresh = new RelayCommands(obj =>
                    {
                        if(appManager.ValorantClient != null)
                            RefreshContent();
                    }, obj => true);
                }
                return refresh;
            }
        }

        private ICommand search;
        public ICommand Search
        {
            get
            {
                if (search == null)
                {
                    search = new RelayCommands(obj =>
                    {
                        appManager.Username = Username;
                        appManager.Tag = Tag;
                        appManager.Region = SelectedRegionEnum;
                        appManager.ValorantClient = new ValorantNET.ValorantClient(appManager.Username, appManager.Tag, appManager.Region);
                    }, obj => true);
                }
                return search;
            }
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

        private void RefreshContent()
        {
            appManager.IsLoading = true;
            overviewModel.GetGeneralStats();
        }
        #endregion

        #region Events
        private void OverviewModel_statsReceived(object sender, ValorantNET.Models.Player e)
        {
            if(e != null)
            {
                KDA = e.stats.kdratio;
                WinPerc = e.stats.winpercentage;
                PlayTime = e.stats.playtime.playtimepatched;
            }

            appManager.IsLoading = false;
        }

        private void AppManager_propertyChanged(object sender, dynamic e)
        {
            if(sender.ToString() == nameof(appManager.IsLoading))
            {
                NotifyPropertyChanged(nameof(IsLoading));
            }

            if(sender.ToString() == nameof(appManager.UUID))
            {
                RefreshContent();
                NotifyPropertyChanged(nameof(IsRefreshEnable));
            }
        }
        #endregion

        public event PropertyChangedEventHandler PropertyChanged;
        public void NotifyPropertyChanged(string propertyname)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyname));
        }
    }
}
