using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Valkirie.Client.Utilities;
using ValorantNET.Models;

namespace Valkirie.Client.Components.Page.Overview
{
    public class OverviewModel
    {
        private AppManager appManager;

        public event EventHandler<Player> statsReceived;

        public OverviewModel(AppManager appManager)
        {
            this.appManager = appManager;
        }

        public void GetGeneralStats()
        {
            var task = new Task(async () =>
            {
                var result = await appManager.ValorantClient.GetStatsAsync();
                statsReceived?.Invoke(this, result);
            });
            task.Start();
        }
    }
}
