using System;
using System.Collections.Generic;
using System.Text;
using Valkirie.Client.Utilities;

namespace Valkirie.Client.Components.Page.Overview
{
    public class OverviewViewModel
    {
        private AppManager appManager;
        public OverviewViewModel(AppManager appManager)
        {
            this.appManager = appManager;
        }
    }
}
