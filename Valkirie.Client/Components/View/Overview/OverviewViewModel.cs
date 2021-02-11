using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Windows.Input;
using Valkirie.Client.Utilities;

namespace Valkirie.Client.Components.Page.Overview
{
    public class OverviewViewModel : INotifyPropertyChanged
    {
        private AppManager appManager;
        private OverviewModel overviewModel;

        private float kda;
        private float winPerc;
        private string playTime;

        public string Username => appManager.Username + "#" + appManager.Tag;

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

        public OverviewViewModel(AppManager appManager)
        {
            this.appManager = appManager;
            this.overviewModel = new OverviewModel(this.appManager);
            RefreshContent();
            this.overviewModel.statsReceived += OverviewModel_statsReceived;
        }

        private ICommand refresh;
        public ICommand Refresh
        {
            get
            {
                if (refresh == null)
                {
                    refresh = new RelayCommands(obj =>
                    {
                        RefreshContent();
                    }, obj => true);
                }
                return refresh;
            }
        }

        private void RefreshContent()
        {
            appManager.IsLoading = true;
            overviewModel.GetGeneralStats();
        }

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

        public event PropertyChangedEventHandler PropertyChanged;
        public void NotifyPropertyChanged(string propertyname)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyname));
        }
    }
}
