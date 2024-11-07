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
    public partial class AMCReports : ContentPage
    {
        AssetAMCViewModel viewModel;
        public AMCReports()
        {
            InitializeComponent();
            viewModel = new AssetAMCViewModel();
            BindingContext = viewModel;
            entrydocket.TextChanged += Entrydocket_TextChanged;
            entrydocket.Completed += Entrydocket_Completed;
            docketView.ItemTapped += DocketView_ItemTapped;
            // scan assets

            var searchtapped = new TapGestureRecognizer();
            searchtapped.Tapped += async (s, e) =>
            {
                viewModel.ASSETID = entrydocket.Text;
                viewModel.SearchAsset();
              
            };
            imgsearch.GestureRecognizers.Add(searchtapped);


         /*   var verifyDocket = new TapGestureRecognizer();
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
                   // entrydocket.CursorPosition = entrydocket.Text.Length + 1;
                   // viewModel.ASSETID = "";
                }

            };
            qrcode.GestureRecognizers.Add(verifyDocket);*/
        }

        private void DocketView_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            viewModel.Image1 = null;
            viewModel.ShowMain = false;
            viewModel.Show_popuplayout = true;

            var assets = e.Item as AssetAMCList;
            viewModel.ASSET_ID = assets.Asset_id;
            viewModel.ASSET_NAME = assets.Asset_Name;
            viewModel.AMC_DESCRIPTION = assets.AMC_Description;
            viewModel.AMC_ENDDATE = assets.AMC_EndDate;
            viewModel.AMC_STARTDATE = assets.AMC_StartDate;
            viewModel.AMC_TYPE = assets.AMC_Type;
            viewModel.AMC_VALUE = assets.AMC_Value;
            viewModel.VENDOR_NAME = assets.Vendor_Name;
            // Displaying captured image
            if (!assets.Image1.Equals(""))
            {
                viewModel.Image1 = Xamarin.Forms.ImageSource.FromStream(() => new MemoryStream(Convert.FromBase64String(assets.Image1)));
            }
        }

        private void Entrydocket_Completed(object sender, EventArgs e)
        {
            viewModel.ASSETID = entrydocket.Text;
            viewModel.SearchAsset();
        }

        private void Entrydocket_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (viewModel.ASSETID.Equals(""))
            {
                viewModel.ObjStockList = viewModel.SEARCHOBJECT;

            }
        }

        private void logout_Clicked(object sender, EventArgs e)
        {
            Preferences.Set(Pref.LOGINSTATUS, false);

            Application.Current.MainPage = new MainPage();
        }

        private void share_Clicked(object sender, EventArgs e)
        {
            string filename = "Asset_AMC_Reports";
            CommonClass.SubmitAMCDetails(filename, viewModel.ObjStockList);
            CommonClass.ShareFile(filename);
        }

        private async void btnsearchassets_Clicked(object sender, EventArgs e)
        { //your code
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
                         viewModel.ASSETID = entrydocket.Text;
                         viewModel.SearchAsset();

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
                // entrydocket.CursorPosition = entrydocket.Text.Length + 1;
                // viewModel.ASSETID = "";
            }

        }
    }
}