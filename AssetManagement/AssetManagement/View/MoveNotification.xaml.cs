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
using static AssetManagement.View.MasterDetailPage1Master;

namespace AssetManagement.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MoveNotification : ContentPage
    {
        AssetMoveViewModel viewModel;
        public MoveNotification()
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
                viewModel.SearchMovedAsset();
               
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



                            viewModel.ASSETID = entrydocket.Text;
                            DependencyService.Get<IAudio>().PlayAudioFile(ProjectConstants.BEEP);
                            viewModel.SearchMovedAsset();
                           
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
                    entrydocket.CursorPosition = entrydocket.Text.Length + 1;
                    viewModel.ASSETID = "";
                }

            };
            qrcode.GestureRecognizers.Add(verifyDocket);
        }

        private void DocketView_ItemTapped(object sender, ItemTappedEventArgs e)
        {
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
        }

        private void Entrydocket_Completed(object sender, EventArgs e)
        {
            viewModel.ASSETID = entrydocket.Text;
            viewModel.SearchMovedAsset();
        }

        private void Entrydocket_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (viewModel.ASSETID.Equals(""))
            {
                viewModel.Objmovenotification= viewModel.SEARCHOBJECT;

            }
        }

        private void btnsave_Clicked(object sender, EventArgs e)
        {
            viewModel.UpdateMoveNotification();
        }

        private void btncancel_Clicked(object sender, EventArgs e)
        {
            viewModel.ShowMain = true;
            viewModel.Show_popuplayout = false;
        }

        private void logout_Clicked(object sender, EventArgs e)
        {
            Preferences.Set(Pref.LOGINSTATUS, false);

            Application.Current.MainPage = new MainPage();
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
           
        }
    }
}