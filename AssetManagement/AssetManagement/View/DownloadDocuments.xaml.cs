using Android.Graphics;
using Android.Util;
using AssetManagement.Constants;
using AssetManagement.Interface;
using AssetManagement.Model;
using AssetManagement.ViewModel;
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
    public partial class DownloadDocuments : ContentPage
    {
        DownloadImagesViewModel vm;
        public DownloadDocuments()
        {
            InitializeComponent();
            vm = new DownloadImagesViewModel();
            BindingContext = vm;

           


            var searchtapped = new TapGestureRecognizer();
            searchtapped.Tapped += async (s, e) =>
            {
                vm.ASSETID = entrydocket.Text;
                vm.GetAssetImage();
            };
            imgsearch.GestureRecognizers.Add(searchtapped);


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
                   // Crashes.TrackError(excp);
                }
                finally
                {
                    // entrydocket.CursorPosition = entrydocket.Text.Length + 1;
                    // viewModel.ASSETID = "";
                }

            };
            qrcode.GestureRecognizers.Add(verifyDocket);
        }

        private void logout_Clicked(object sender, EventArgs e)
        {
            Preferences.Set(Pref.LOGINSTATUS, false);

            Application.Current.MainPage = new MainPage();
        }

        private async void image_download_Clicked(object sender, EventArgs e)
        {
            try
            {
                var assetetails = (assetimages)((ImageButton)sender).BindingContext;
                var csvPath = DependencyService.Get<IShareReports>().GetImagePath(vm.ASSETID);
                var stream = new FileStream(csvPath, FileMode.OpenOrCreate);
                Base64ToBitmap(assetetails.Images).Compress(Bitmap.CompressFormat.Png, 100, stream); ;
                // bitmap.Compress(Bitmap.CompressFormat.Png, 100, stream);
                stream.Close();

                await Share.RequestAsync(new ShareFileRequest
                {
                    Title = assetetails.ImageType,
                    File = new ShareFile(csvPath)
                });
            }
            catch(Exception excp)
            {
                await DisplayAlert("Exception", excp.ToString(), "OK");
            }
        }
        public Bitmap Base64ToBitmap(String base64String)
        {
            byte[] imageAsBytes = Base64.Decode(base64String, Base64Flags.Default);
            return BitmapFactory.DecodeByteArray(imageAsBytes, 0, imageAsBytes.Length);
        }
    }
}