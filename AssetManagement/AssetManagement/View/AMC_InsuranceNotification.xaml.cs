using AssetManagement.Constants;
using AssetManagement.Interface;
using AssetManagement.Model;
using AssetManagement.ViewModel;
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
    public partial class AMC_InsuranceNotification : TabbedPage
    {
        AMC_InsuranceNotifyViewModel vm;
        public AMC_InsuranceNotification()
        {
            InitializeComponent();
            vm = new AMC_InsuranceNotifyViewModel();
            BindingContext = vm;
            docketView1.ItemTapped += DocketView1_ItemTapped;
            entrydocket1.TextChanged += Entrydocket1_TextChanged;
            entrydocket1.Completed += Entrydocket1_Completed;

            docketView2.ItemTapped += DocketView2_ItemTapped;
            entrydocket2.TextChanged += Entrydocket2_TextChanged;
            entrydocket2.Completed += Entrydocket2_Completed;

            // scan assets
            var searchtapped = new TapGestureRecognizer();
            searchtapped.Tapped += async (s, e) =>
            {
                vm.ASSETID = entrydocket1.Text;
                vm.SearchAMC();
              
            };
            imgsearch1.GestureRecognizers.Add(searchtapped);

            var searchtapped1 = new TapGestureRecognizer();
            searchtapped1.Tapped += async (s, e) =>
            {
                vm.ASSETID = entrydocket2.Text;
                vm.SearchIns();
            };
            imgsearch2.GestureRecognizers.Add(searchtapped1);


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
                  //  Crashes.TrackError(excp);
                }
                finally
                {
                    // entrydocket.CursorPosition = entrydocket.Text.Length + 1;
                    //viewModel.ASSETID = "";
                }

            };
            qrcode1.GestureRecognizers.Add(verifyDocket1);

            var verifyDocket2 = new TapGestureRecognizer();
            verifyDocket2.Tapped += async (s, e) =>
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

                            entrydocket2.Text = result.Text.Trim();


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
                    //  Crashes.TrackError(excp);
                }
                finally
                {
                    // entrydocket.CursorPosition = entrydocket.Text.Length + 1;
                    //viewModel.ASSETID = "";
                }

            };
            qrcode2.GestureRecognizers.Add(verifyDocket2);
        }

        private void Entrydocket2_Completed(object sender, EventArgs e)
        {
            vm.ASSETID = entrydocket2.Text;
            vm.SearchIns();
        }

        private void Entrydocket2_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (vm.ASSETID.Equals(""))
            {
                vm.InsuranceList = vm.InsuranceListSearch;

            }
        }

        private void DocketView2_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            vm.ShowMain2 = false;
            vm.Show_popuplayout2 = true;

            var assets = e.Item as InsuranceList;
            vm.ASSET_ID = assets.Asset_id;
            vm.ASSET_NAME = assets.Asset_Name;
            vm.Policy_Date = assets.Policy_Date.ToString();
            vm.InsuranceCompany = assets.InsuranceCompany_Name;
            vm.Policy_No = assets.Policy_No;
            vm.Alert_Date = assets.Alert_Date.ToString();
            vm.Policy_Name = assets.Policy_Name;
            vm.Premium = assets.Premium;
            vm.Due_Date = assets.Due_Date.ToString();
        }

        private void Entrydocket1_Completed(object sender, EventArgs e)
        {
            vm.ASSETID = entrydocket1.Text;
            vm.SearchAMC();
        }

        private void Entrydocket1_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (vm.ASSETID.Equals(""))
            {
                vm.AMCList = vm.AMCListSearch;

            }
        }

        private void DocketView1_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            vm.ShowMain1 = false;
            vm.Show_popuplayout1 = true;

            var assets = e.Item as AMCList;
            vm.ASSET_ID = assets.Asset_id;
            vm.ASSET_NAME = assets.Asset_Name;
            vm.VENDOR = assets.Vendor_Name;
            vm.AMC_TYPE = assets.AMC_Type;
            vm.STARTDATE = assets.AMC_StartDate.ToString();
            vm.ENDDATE = assets.AMC_EndDate.ToString();
            vm.ALERTDATE = assets.AMC_AlertDate.ToString();
            vm.DESCRIPTION = assets.AMC_Description;
            vm.AMC_VALUE = assets.AMC_Value;
        }

        private void logout2_Clicked(object sender, EventArgs e)
        {
            Preferences.Set(Pref.LOGINSTATUS, false);

            Application.Current.MainPage = new MainPage();
        }

        private void logout1_Clicked(object sender, EventArgs e)
        {
            Preferences.Set(Pref.LOGINSTATUS, false);

            Application.Current.MainPage = new MainPage();
        }

        private void btncancel1_Clicked(object sender, EventArgs e)
        {
            vm.ShowMain1 = true;
            vm.Show_popuplayout1 = false;
        }

        private void btncancel2_Clicked(object sender, EventArgs e)
        {
            vm.ShowMain2 = true;
            vm.Show_popuplayout2 = false;
        }

      
    }
}