using AssetManagement.Constants;
using AssetManagement.Interface;
using AssetManagement.Model;
using AssetManagement.ViewModel;
using Microsoft.AppCenter.Crashes;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using ZXing.Mobile;
using ZXing.Net.Mobile.Forms;

namespace AssetManagement.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AssetsReport : ContentPage
    {
        AssetReportViewModel viewModel;
       
        public AssetsReport()
        {
            InitializeComponent();
            viewModel = new AssetReportViewModel();
            BindingContext = viewModel;
/*            entrydocket.Completed += Entrydocket_Completed;
            entrydocket.TextChanged += Entrydocket_TextChanged;*/
            docketView.ItemTapped += DocketView_ItemTapped;
          //  entrydocket.Focus();
          /*  var searchtapped = new TapGestureRecognizer();
            searchtapped.Tapped += async (s, e) =>
            {
                viewModel.ASSETID = entrydocket.Text;
                viewModel.SearchAsset();
            };
            imgsearch.GestureRecognizers.Add(searchtapped);*/

            var searchtapped1 = new TapGestureRecognizer();
            searchtapped1.Tapped += async (s, e) =>
            {
               // viewModel.ASSETID = entrydocket.Text;
                viewModel.SearchAsset_bySubCat();
            };
            imgsearch1.GestureRecognizers.Add(searchtapped1);

            // scan assets
    /*        var verifyDocket = new TapGestureRecognizer();
            verifyDocket.Tapped += async (s, e) =>
            {
                //your code
                try
                {
                    var options = new MobileBarcodeScanningOptions
                    {
                        AutoRotate = false,
                        UseFrontCameraIfAvailable = false,
                        TryHarder = true
                    };

                    var overlay = new ZXingDefaultOverlay
                    {

                        TopText = "Please scan QR code",
                        BottomText = "Align the QR code within the frame"
                    };

                    var QRScanner = new ZXingScannerPage(options, overlay);

                    await Navigation.PushModalAsync(QRScanner);

                    QRScanner.OnScanResult += (result) =>
                    {
                        // Stop scanning
                        QRScanner.IsScanning = false;

                        // Pop the page and show the result
                        Device.BeginInvokeOnMainThread(async () =>
                        {
                            Navigation.PopModalAsync(true);

                            entrydocket.Text = result.Text.Trim();
                            DependencyService.Get<IAudio>().PlayAudioFile(ProjectConstants.BEEP);
                            // viewModel.ASSETID = entrydocket.Text;
                            // viewModel.SearchAsset();
                           
                        });

                    };

                }
                catch (Exception excp)
                {
                    // Common.SaveLogs(excp.ToString());
                    // Common.SaveLogs(excp.StackTrace);
                    //GlobalScript.SeptemberDebugMessages("ERROR", "BtnScanQR_Clicked", "Opening ZXing Failed: " + ex);
                    await DisplayAlert("Alert", "Please try again", "OK");
                    Crashes.TrackError(excp);
                }
                finally
                {
                    //entrydocket.CursorPosition = entrydocket.Text.Length + 1;
                   // viewModel.ASSETID = "";
                }

            };
            qrcode.GestureRecognizers.Add(verifyDocket);*/

            //...................
        }

        private async void DocketView_ItemTapped(object sender, ItemTappedEventArgs e)
        {
           // var assets = e.Item as STockTallyDetails;
           // await Navigation.PushAsync(new CreateAssets(assets.Asset_id));
        }

    /*    private void Entrydocket_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (viewModel.ASSETID.Equals(""))
            {
                viewModel.ObjStockList = viewModel.SEARCHOBJECT;
                
            }
        }*/

       /* private void Entrydocket_Completed(object sender, EventArgs e)
        {
            viewModel.ASSETID = entrydocket.Text;
            viewModel.SearchAsset();
          
        }*/

        private void logout_Clicked(object sender, EventArgs e)
        {
            Preferences.Set(Pref.LOGINSTATUS, false);

            Application.Current.MainPage = new MainPage();
        }

        private async void share_Clicked(object sender, EventArgs e)
        {
           /* string filename = "Asset_Reports";
             CommonClass.SubmitDetails(filename, viewModel.ObjStockList);
            CommonClass.ShareFile(filename);*/

            string filename = "Asset_Reports";
              await viewModel.GetAssetsData();
             await CommonClass.GetAssetDetails(filename, viewModel.AssetList);
             CommonClass.ShareFile(filename);
            // await DisplayAlert("Alert", "Work is in progress", "OK");
        }


        protected override void OnAppearing()
        {
            base.OnAppearing();
            viewModel.GetAllAssetsData();
        }

        private async void btnaddassets_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new CreateAssets(""));
        }

      

        private void pkremployee_ItemSelected(object sender, CustomRenderer.ItemSelectedEventArgs e)
        {
            viewModel.FilteredItem = (int)FilterList.Employee;
            viewModel.Employee = viewModel.EmployeeList[e.SelectedIndex];
             pkrsubcat.SelectedIndex = 0;
            pkrlocation.SelectedIndex = 0;
            pkrbranch.SelectedIndex = 0;
            pkrcategory.SelectedIndex = 0;
          //  pkremployee.SelectedIndex = 0;
        }

        private void pkrlocation_ItemSelected(object sender, CustomRenderer.ItemSelectedEventArgs e)
        {
            viewModel.FilteredItem = (int)FilterList.Location;
            viewModel.Location = viewModel.LocationList[e.SelectedIndex];
             pkrsubcat.SelectedIndex = 0;
           // pkrlocation.SelectedIndex = 0;
            pkrbranch.SelectedIndex = 0;
            pkrcategory.SelectedIndex = 0;
            pkremployee.SelectedIndex = 0;
        }

        private void pkrbranch_ItemSelected(object sender, CustomRenderer.ItemSelectedEventArgs e)
        {
            viewModel.FilteredItem = (int)FilterList.Branch;
            viewModel.Branch = viewModel.BranchList[e.SelectedIndex];
             pkrsubcat.SelectedIndex = 0;
            pkrlocation.SelectedIndex = 0;
           // pkrbranch.SelectedIndex = 0;
            pkrcategory.SelectedIndex = 0;
            pkremployee.SelectedIndex = 0;
        }

        private void pkrcategory_ItemSelected(object sender, CustomRenderer.ItemSelectedEventArgs e)
        {
            viewModel.FilteredItem = (int)FilterList.Category;
            viewModel.Category = viewModel.CategoryList[e.SelectedIndex];
             pkrsubcat.SelectedIndex = 0;
            pkrlocation.SelectedIndex = 0;
            pkrbranch.SelectedIndex = 0;
           // pkrcategory.SelectedIndex = 0;
            pkremployee.SelectedIndex = 0;
        }

        private void pkrsubcat_ItemSelected(object sender, CustomRenderer.ItemSelectedEventArgs e)
        {
            viewModel.FilteredItem = (int)FilterList.Subcategory;
            viewModel.SubCategory = viewModel.SubCategoryList[e.SelectedIndex];
           // pkrsubcat.SelectedIndex = 0;
            pkrlocation.SelectedIndex = 0;
            pkrbranch.SelectedIndex = 0;
            pkrcategory.SelectedIndex = 0;
            pkremployee.SelectedIndex = 0;
           
        }

        private void ToolbarItem_Clicked(object sender, EventArgs e)
        {
           viewModel.ObjStockList = viewModel.SEARCHOBJECT;
          
            pkrsubcat.SelectedIndex = 0;
            pkrlocation.SelectedIndex = 0;
            pkrbranch.SelectedIndex = 0;
            pkrcategory.SelectedIndex = 0;
            pkremployee.SelectedIndex = 0;
        }
    }
}