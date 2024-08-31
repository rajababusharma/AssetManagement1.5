using AssetManagement.Constants;
using AssetManagement.Interface;
using AssetManagement.Model;
using AssetManagement.ViewModel;
using Newtonsoft.Json;
using Plugin.Connectivity;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AssetManagement.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class FoundMissingReports : TabbedPage
    {
        FoundMissingReportViewModel viewModel;
        public FoundMissingReports()
        {
            InitializeComponent();
            viewModel = new FoundMissingReportViewModel();
            BindingContext = viewModel;
            
        }

      

        private void logout1_Clicked(object sender, EventArgs e)
        {
            Preferences.Set(Pref.LOGINSTATUS, false);

            Application.Current.MainPage = new MainPage();
        }

        private void share1_Clicked(object sender, EventArgs e)
        {
            string filename = "Asset_Found_Report";
            CommonClass.SubmitDetails(filename, viewModel.FoundReports);
            CommonClass.ShareFile(filename);
        }

        private void logout2_Clicked(object sender, EventArgs e)
        {
            Preferences.Set(Pref.LOGINSTATUS, false);

            Application.Current.MainPage = new MainPage();
        }

        private void share2_Clicked(object sender, EventArgs e)
        {
            string filename = "Asset_Missing_Report";
            CommonClass.SubmitDetails(filename,viewModel.MissingReports);
            CommonClass.ShareFile(filename);
        }

        
    }
}