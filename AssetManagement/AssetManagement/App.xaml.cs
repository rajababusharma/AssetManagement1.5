using AssetManagement.Constants;
using AssetManagement.View;
using MediaManager;
using Microsoft.AppCenter;
using Microsoft.AppCenter.Analytics;
using Microsoft.AppCenter.Crashes;
using System;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AssetManagement
{
    public partial class App : Application
    {
        public App()
        {
            Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("NTgxMDIxQDMxMzgyZTM0MmUzMGpYYzZQNVBqZDF3Ri9XVTk4dGNaeXpSZWV3Yi9kVlhraUxSbVIyMzFORlU9");
            InitializeComponent();
            //Register Syncfusion license
            //https://www.syncfusion.com/account/downloads?_gl=1*1tas5l*_ga*MTU4MzM3MjkyMC4xNjQzNzk2OTkz*_ga_WC4JKKPHH0*MTY0NDY2MTk0OS4yLjEuMTY0NDY2Mzk3NC4w&_ga=2.156393019.1659148184.1644661954-1583372920.1643796993&_gl=1*1tas5l*_ga*MTU4MzM3MjkyMC4xNjQzNzk2OTkz*_ga_WC4JKKPHH0*MTY0NDY2MTk0OS4yLjEuMTY0NDY2Mzk3NC4w&_ga=2.156393019.1659148184.1644661954-1583372920.1643796993
            

            MainPage = new MainPage();
        }

        protected override void OnStart()
        {
            bool loginStatus = Preferences.Get(Pref.LOGINSTATUS, false);
            if (loginStatus)
            {
                MainPage = new MasterDetailPage1();

            }
            else
            {
                MainPage = new MainPage();
            }

            AppCenter.Start("android={452e242a-120f-477a-b23b-2e88200bc456};" +
                  "uwp={Your UWP App secret here};" +
                  "ios={Your iOS App secret here};" +
                  "macos={Your macOS App secret here};",
                  typeof(Analytics), typeof(Crashes));
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
