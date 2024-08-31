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
    public partial class RepairReports : TabbedPage
    {
        AssetRepairViewModel viewModel;
        public RepairReports()
        {
            InitializeComponent();
            viewModel = new AssetRepairViewModel();
            BindingContext = viewModel;
            docketView2.ItemTapped += DocketView2_ItemTapped;
            docketView1.ItemTapped += DocketView1_ItemTapped;
            entrydocket.Completed += Entrydocket_Completed;
            entrydocket.TextChanged += Entrydocket_TextChanged;
            entrydocket1.Completed += Entrydocket1_Completed;
            entrydocket1.TextChanged += Entrydocket1_TextChanged;
            chkisreturn.CheckedChanged += Chkisreturn_CheckedChanged;

           
            // scan assets
            var searchtapped = new TapGestureRecognizer();
            searchtapped.Tapped += async (s, e) =>
            {
                viewModel.ASSETID = entrydocket.Text;
                viewModel.SearchAsset();
            };
            imgsearch.GestureRecognizers.Add(searchtapped);

            var searchtapped1 = new TapGestureRecognizer();
            searchtapped1.Tapped += async (s, e) =>
            {
                viewModel.ASSETID = entrydocket1.Text;
                viewModel.SearchAsset1();
        
            };
            imgsearch1.GestureRecognizers.Add(searchtapped1);


            var verifyDocket = new TapGestureRecognizer();
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
                            //  viewModel.ASSETID = entrydocket.Text;
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
                    //viewModel.ASSETID = "";
                }

            };
            qrcode.GestureRecognizers.Add(verifyDocket);

            var verifyDocket1 = new TapGestureRecognizer();
            verifyDocket1.Tapped += async (s, e) =>
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

                            entrydocket1.Text = result.Text.Trim();


                            DependencyService.Get<IAudio>().PlayAudioFile(ProjectConstants.BEEP);
                            //  viewModel.ASSETID = entrydocket.Text;
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
                    //viewModel.ASSETID = "";
                }

            };
            qrcode1.GestureRecognizers.Add(verifyDocket1);
        }

        private void Entrydocket1_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (viewModel.ASSETID.Equals(""))
            {
                viewModel.IsReturnReports = viewModel.SEARCHOBJECT1;

            }
        }

        private void Entrydocket1_Completed(object sender, EventArgs e)
        {
            viewModel.ASSETID = entrydocket1.Text;
            viewModel.SearchAsset1();
        }

        private void DocketView1_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            viewModel.Image1 = null;
            viewModel.Image2 = null;
            viewModel.Image3 = null;
            viewModel.ShowMain1 = false;
            viewModel.Show_popuplayout1 = true;

            var assets = e.Item as AssetRepairList;
            viewModel.ASSET_ID = assets.Asset_id;
            viewModel.ASSET_NAME = assets.Asset_name;
            viewModel.REASON = assets.ReasonFor_Repair;
            viewModel.EXPECTEDRETURNDATE = assets.ExpectedReturn_Date;
            viewModel.VENDOR_NAME = assets.Vendor_Name;
            viewModel.RETURNDATE = assets.Returned_On;
            viewModel.DELAY_REASON = assets.ReasonFor_Delay;

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

        private void Chkisreturn_CheckedChanged(object sender, CheckedChangedEventArgs e)
        {
            bool ischecked = e.Value;
            if (ischecked)
            {
                viewModel.IsReturned = true;
            }
            else
            {
                viewModel.IsReturned = false;
            }
        }

        private void DocketView2_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            viewModel.ShowMain = false;
            viewModel.Show_popuplayout = true;

            var assets = e.Item as AssetRepairList;
            viewModel.ASSET_ID = assets.Asset_id;
            viewModel.ASSET_NAME = assets.Asset_name;
            viewModel.REASON = assets.ReasonFor_Repair;
            viewModel.EXPECTEDRETURNDATE = assets.ExpectedReturn_Date;
            viewModel.VENDOR_NAME = assets.Vendor_Name;
            viewModel.RETURNDATE = assets.Returned_On;
            viewModel.DELAY_REASON = assets.ReasonFor_Delay;

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

        private void Entrydocket_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (viewModel.ASSETID.Equals(""))
            {
                viewModel.IsNotReturnReports = viewModel.SEARCHOBJECT;

            }
        }

        private void Entrydocket_Completed(object sender, EventArgs e)
        {
            viewModel.ASSETID = entrydocket.Text;
            viewModel.SearchAsset();
        }

        private void logout1_Clicked(object sender, EventArgs e)
        {
            Preferences.Set(Pref.LOGINSTATUS, false);

            Application.Current.MainPage = new MainPage();
        }

        private void share1_Clicked(object sender, EventArgs e)
        {
            string filename = "Asset_Repair";
            CommonClass.SubmitRepairDetails(filename, viewModel.IsReturnReports);
            CommonClass.ShareFile(filename);
        }

        private void logout2_Clicked(object sender, EventArgs e)
        {
            Preferences.Set(Pref.LOGINSTATUS, false);

            Application.Current.MainPage = new MainPage();
        }

        private void share2_Clicked(object sender, EventArgs e)
        {
            string filename = "Asset_Repair_Report";
            CommonClass.SubmitRepairDetails(filename, viewModel.IsReturnReports);
            CommonClass.ShareFile(filename);
        }

        private void btncancel_Clicked(object sender, EventArgs e)
        {
            viewModel.ShowMain = true;
            viewModel.Show_popuplayout = false;
        }

        private void btncancel1_Clicked(object sender, EventArgs e)
        {
            viewModel.ShowMain1 = true;
            viewModel.Show_popuplayout1 = false;
        }
    }
}