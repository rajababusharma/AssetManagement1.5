using AssetManagement.Constants;
using AssetManagement.Interface;
using AssetManagement.Model;
using AssetManagement.ViewModel;
using Microsoft.AppCenter.Crashes;
using System;
using System.Collections.Generic;
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
    public partial class ManagementAssets : ContentPage
    {
        //  AssetReportViewModel viewModel;
        ManageAssetsViewModel viewModel;
        public ManagementAssets()
        {
            InitializeComponent();
            viewModel = new ManageAssetsViewModel();
            BindingContext = viewModel;
            entrydocket.TextChanged += Entrydocket_TextChanged;
            entrydocket.Completed += Entrydocket_Completed;
            entrydocket.Focus();
            // scan assets
            var searchtapped = new TapGestureRecognizer();
            searchtapped.Tapped += async (s, e) =>
            {
                viewModel.ASSETID = entrydocket.Text;
                viewModel.SearchAsset();
               
            };
            imgsearch.GestureRecognizers.Add(searchtapped);

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


        private async void btnmove_Clicked(object sender, EventArgs e)
        {
          //  var item = e as STockTallyDetails;
            var assetetails = (STockTallyDetails)((Button)sender).BindingContext;
          
            bool ststus = Preferences.Get(Pref.Move_Asset, false);
            if (ststus)
            {
                await Navigation.PushAsync(new Move(assetetails));
            }
            else
            {
                await App.Current.MainPage.DisplayAlert("Alert", "You don't have permission to move assets. Kindly contact to your admin.", "Ok");
            }
        }

        private async void btntransfer_Clicked(object sender, EventArgs e)
        {
            var assetetails = (STockTallyDetails)((Button)sender).BindingContext;
         
            bool ststus = Preferences.Get(Pref.Transfer_Asset, false);
            if (ststus)
            {
                await Navigation.PushAsync(new Transfer(assetetails));
            }
            else
            {
                await App.Current.MainPage.DisplayAlert("Alert", "You don't have permission to transfer assets. Kindly contact to your admin.", "Ok");
            }
        }

        private async void btndispose_Clicked(object sender, EventArgs e)
        {
            var assetetails = (STockTallyDetails)((Button)sender).BindingContext;
           
            bool ststus = Preferences.Get(Pref.Dispose_Asset, false);
            if (ststus)
            {
                await Navigation.PushAsync(new Dispose(assetetails));
            }
            else
            {
                await App.Current.MainPage.DisplayAlert("Alert", "You don't have permission to dispose assets. Kindly contact to your admin.", "Ok");
            }
        }

        private async void btnrepair_Clicked(object sender, EventArgs e)
        {
            var assetetails = (STockTallyDetails)((Button)sender).BindingContext;
           bool ststus= Preferences.Get(Pref.Repair_Asset,false);
            if (ststus)
            {
                await Navigation.PushAsync(new Repair(assetetails));
            }
            else
            {
                await App.Current.MainPage.DisplayAlert("Alert", "You don't have permission to send assets for repair. Kindly contact to your admin.", "Ok");
            }
           
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            viewModel.GetAllAssetsData();

        }

        private async void btnamc_Clicked(object sender, EventArgs e)
        {
            var assetetails = (STockTallyDetails)((Button)sender).BindingContext;
            await Navigation.PushAsync(new CreateAMC(assetetails));
           // await Navigation.PushAsync(new CreateAMC());
        }

        private async void btninsurance_Clicked(object sender, EventArgs e)
        {
            var assetetails = (STockTallyDetails)((Button)sender).BindingContext;
            await Navigation.PushAsync(new CreateInsurance(assetetails));
        }

        private async void btnedit_Clicked(object sender, EventArgs e)
        {
            var assetetails = (STockTallyDetails)((Button)sender).BindingContext;
            await Navigation.PushAsync(new CreateAssets(assetetails.Asset_id));
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
                        // viewModel.ASSETID = entrydocket.Text;
                        // viewModel.SearchAsset();
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

        private async void btnreport_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Reports());
        }

        private async void btncreate_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new CreateAssets(""));
        }
    }
}