using AssetManagement.Constants;
using AssetManagement.CustomRenderer;
using AssetManagement.Interface;
using AssetManagement.Model;
using AssetManagement.ViewModel;
using Microsoft.AppCenter.Crashes;
using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using static Android.App.Assist.AssistStructure;

namespace AssetManagement.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MasterDetailPage1Detail : ContentPage
    {
        DashboardViewModel viewModel;
        
        public MasterDetailPage1Detail()
        {
            InitializeComponent();
            viewModel = new DashboardViewModel();
            BindingContext = viewModel;
           
        }

        private void logout_Clicked(object sender, EventArgs e)
        {
            Preferences.Set(Pref.LOGINSTATUS, false);

            Application.Current.MainPage = new MainPage();
        }
        protected async override void OnAppearing()
        {
            base.OnAppearing();
            string branch_id = Preferences.Get(Pref.BRANCH, "");
            // pkrBranch.SelectedItem = branch_id;
            int cbranch = viewModel.BranchList.IndexOf(branch_id);
            viewModel.CurrentBranch = cbranch;
            viewModel.GetAMCDetails(branch_id);
            viewModel.GetStockDetailsBranchWise();
            // viewModel.GetStockInventory(branch_id);
           // DependencyService.Get<IToolbarItemBadgeService>().SetBadge(this, ToolbarItems.First(), "1", Color.Red, Color.White);
        }

       
        private async void pkrBranch_ItemSelected(object sender, ItemSelectedEventArgs e)
        {
           // string selectedBranch = pkrBranch.SelectedItem.ToString();
            string selectedBranch= viewModel.BranchList[e.SelectedIndex];
           await viewModel.GetAMCDetails(selectedBranch);
        }

        private void SfChart_SelectionChanged(object sender, Syncfusion.SfChart.XForms.ChartSelectionEventArgs e)
        {
            // IList items = e.SelectedSeries.ItemsSource as IList;
            // BranchWiseAssets assets = new BranchWiseAssets();

            // viewModel = new DashboardViewModel();
            string branch = Preferences.Get(Pref.TOOLTRIPBRANCH, "");
            GetData(branch);
            
            // viewModel.IsPopup = true;
            // viewModel.IsMain = false;
        }
        public async Task GetData(string branch_id)
        {
            // string branch_id = Preferences.Get(Pref.BRANCH, "");
            try
            {
                viewModel.IsBusy = true;
                viewModel.IsEnable = true;
                viewModel.IsVisible = true;

                /* string ctype = "Unloading";*/
                // string ctype = Preferences.Get(ProjectConstants.CTYPE, "");


                var client = new System.Net.Http.HttpClient();
                //  client.BaseAddress = new Uri("http://114.143.156.30/");
                client.BaseAddress = new Uri(ProjectConstants.GETASSETCATEGORYCOUNT_API);



                var response = await client.GetAsync("GetAssetCategoryCount?Branch=" + branch_id);
                var responseJson = response.Content.ReadAsStringAsync().Result;

                AssetCategoryCountResp stocktake = new AssetCategoryCountResp();

                // List<AssetCategoryCountList> mystocklist = new List<AssetCategoryCountList>();
                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    stocktake = JsonConvert.DeserializeObject<AssetCategoryCountResp>(responseJson);
                    if (stocktake.Status.Equals("true"))
                    {
                        var lists = stocktake.AssetCategoryCountList;
                        Device.BeginInvokeOnMainThread(async () => {
                           
                            Navigation.PushAsync(new ChartDetailPage(lists, branch_id));
                            // await Navigation.PushAsync(new ChartDetailPage());
                            //await App.Current.MainPage.Navigation.PushAsync(new ChartDetailPage());
                        });

                    }
                    else
                    {
                        //DependencyService.Get<IAudio>().PlayAudioFile(ProjectConstants.audio_alert_fail);
                        await App.Current.MainPage.DisplayAlert("Exception", stocktake.Msg.ToString(), "Ok");
                        // ASSETCATEGORYCOUNT = null;
                       
                    }

                }
                else
                {
                    // DependencyService.Get<IAudio>().PlayAudioFile(ProjectConstants.audio_alert_fail);
                    await App.Current.MainPage.DisplayAlert("Exception", response.ReasonPhrase, "Ok");
                    // ASSETCATEGORYCOUNT = null;
                    
                }
                viewModel.IsBusy = false;
                viewModel.IsEnable = false;
                viewModel.IsVisible = false;

            }
            catch (Exception excp)
            {
                // Common.SaveLogs(excp.StackTrace);
                viewModel.IsBusy = false;
                viewModel.IsEnable = false;
                viewModel.IsVisible = false;
               
                // ASSETCATEGORYCOUNT = stocktake.AssetCategoryCountList;
                //await App.Current.MainPage.DisplayAlert("Exception", "Request could n, please try again later", "Ok");

                Crashes.TrackError(excp);
            }
        }
        private async void btnaddassets_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new CreateAssets(""));
        }
    }
}