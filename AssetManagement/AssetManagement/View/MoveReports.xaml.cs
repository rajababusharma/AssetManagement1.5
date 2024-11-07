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
using static Android.App.Assist.AssistStructure;

namespace AssetManagement.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MoveReports : ContentPage
    {
        AssetMoveViewModel viewModel;
        public MoveReports()
        {
            InitializeComponent();
            viewModel = new AssetMoveViewModel();
            BindingContext = viewModel;
            entrydocket.TextChanged += Entrydocket_TextChanged;
            entrydocket.Completed += Entrydocket_Completed;
            docketView.ItemTapped += DocketView_ItemTapped;

            entrydocket.Focus();

            var searchtapped = new TapGestureRecognizer();
            searchtapped.Tapped += async (s, e) =>
            {
                viewModel.ASSETID = entrydocket.Text;
                viewModel.SearchAsset();
              
            };
            imgsearch.GestureRecognizers.Add(searchtapped);

            // scan assets
           /* var verifyDocket = new TapGestureRecognizer();
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
    

        public void DocketView_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            viewModel.Image1=null;
            viewModel.Image2 = null;
            viewModel.Image3 = null;
            viewModel.ShowMain = false;
            viewModel.Show_popuplayout = true;

            var assets = e.Item as AssetMoveList;
            viewModel.ASSET_ID = assets.Asset_id;
            viewModel.ASSETNAME = assets.Asset_name;
            viewModel.FROMLOCATION = assets.From_Location_Description;
            viewModel.TOLOCATION = assets.To_Location_Description;
            viewModel.FROMBRANCH = assets.From_Floor;
            viewModel.TOBRANCH = assets.To_Floor;
            viewModel.MOVEDATE = assets.AssetMove_Date;
            viewModel.STATUS = assets.Status;
            viewModel.Remarks = assets.Remarks;
            // Displaying captured image
            if (!assets.Image1.Equals(""))
            {
                viewModel.Image1 = Xamarin.Forms.ImageSource.FromStream(() => new MemoryStream(Convert.FromBase64String(assets.Image1)));
            }
            // Displaying captured image
            if (!assets.Image2.Equals(""))
            {
                viewModel.Image2 = Xamarin.Forms.ImageSource.FromStream(() => new MemoryStream(Convert.FromBase64String(assets.Image2)));
            }
            // Displaying captured image
            if (!assets.Image3.Equals(""))
            {
                viewModel.Image3 = Xamarin.Forms.ImageSource.FromStream(() => new MemoryStream(Convert.FromBase64String(assets.Image3)));
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
            string filename = "Asset_Move_Reports";
            CommonClass.SubmitMoveDetails(filename, viewModel.ObjStockList);
            CommonClass.ShareFile(filename);
        }

        private void btncancel_Clicked(object sender, EventArgs e)
        {
            viewModel.ShowMain = true;
            viewModel.Show_popuplayout = false;
        }

        private async void btnsearchassets_Clicked(object sender, EventArgs e)
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