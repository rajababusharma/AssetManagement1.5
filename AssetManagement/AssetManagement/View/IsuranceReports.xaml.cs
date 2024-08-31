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
    public partial class IsuranceReports : ContentPage
    {
        AssetInsuranceViewModel viewModel;
        public IsuranceReports()
        {
            InitializeComponent();
            viewModel = new AssetInsuranceViewModel();
            BindingContext = viewModel;
            entrydocket.Completed += Entrydocket_Completed;
            entrydocket.TextChanged += Entrydocket_TextChanged;
            entrydocket.Focus();
          

            var searchtapped = new TapGestureRecognizer();
            searchtapped.Tapped += async (s, e) =>
            {
                viewModel.ASSETID = entrydocket.Text;
                viewModel.SearchAsset();
               
            };
            imgsearch.GestureRecognizers.Add(searchtapped);

            // scan assets
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
                    Crashes.TrackError(excp);
                }
                finally
                {
                   // entrydocket.CursorPosition = entrydocket.Text.Length + 1;
                   // viewModel.ASSETID = "";
                }

            };
            qrcode.GestureRecognizers.Add(verifyDocket);
        }

        private void Entrydocket_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (viewModel.ASSETID.Equals(""))
            {
                viewModel.ObjStockList = viewModel.SEARCHOBJECT;
              
            }
        }

        private void Entrydocket_Completed(object sender, EventArgs e)
        {
            viewModel.ASSETID = entrydocket.Text;
            viewModel.SearchAsset();
        }

        private void logout_Clicked(object sender, EventArgs e)
        {
            Preferences.Set(Pref.LOGINSTATUS, false);

            Application.Current.MainPage = new MainPage();
        }

        private void share_Clicked(object sender, EventArgs e)
        {
            string filename = "Asset_Insurance_Reports";
            CommonClass.SubmitInsuranceDetails(filename, viewModel.ObjStockList);
            CommonClass.ShareFile(filename);
        }

        private void docketView_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            viewModel.Image1 = null;
            viewModel.ShowMain = false;
            viewModel.Show_popuplayout = true;

            var assets = e.Item as InsuranceDetailList;
            viewModel.ASSET_ID = assets.Asset_id;
            viewModel.INSURANCE_DATE = assets.Insurance_Date;
            viewModel.PREMIUM = assets.Premium;
            viewModel.POLICY_DATE = assets.Policy_Date;
            viewModel.INSURANCECOMPANY_NAME = assets.InsuranceCompany_Name;
            viewModel.MATURITY_DATE = assets.Maturity_Date;
            viewModel.POLICY_NAME = assets.Policy_Name;
            viewModel.POLICY_NO = assets.Policy_No;
            // Displaying captured image
            if (!assets.Image1.Equals(""))
            {
                viewModel.Image1 = Xamarin.Forms.ImageSource.FromStream(() => new MemoryStream(Convert.FromBase64String(assets.Image1)));
            }
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
        }
    }
}