using AssetManagement.Constants;
using AssetManagement.DB;
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
    public partial class StockTally : ContentPage
    {
        StockTakeViewModel viewModel;
        DatabaseController database;
        bool focus_status = false;
        string branch_id = "";
        string error = "";
        public StockTally()
        {
            InitializeComponent();

            viewModel = new StockTakeViewModel();
            BindingContext = viewModel;
            try
            {
                database = new DatabaseController();
            }
            catch(Exception exp)
            {
                error = exp.Message;
            }
            branch_id = Preferences.Get(Pref.BRANCH, "");
            entrydocket.Completed += Entrydocket_Completed; ;
            entrydocket.Unfocused += Entrydocket_Unfocused; ;

            var searchtapped= new TapGestureRecognizer();
            searchtapped.Tapped += async (s, e) =>
            {
                await ValidateStock(branch_id);
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
                            await ValidateStock(branch_id);
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

        private void Entrydocket_Unfocused(object sender, FocusEventArgs e)
        {
            Device.StartTimer(TimeSpan.FromMilliseconds(2000.0), TimerElapsed);

            bool TimerElapsed()
            {
                Device.BeginInvokeOnMainThread(() =>
                {
                    if (!focus_status)
                    {
                        //put here your code 
                        entrydocket.CursorPosition = entrydocket.Text.Length + 1;
                    }
                    //viewModel.DOCKETNUMBER = "";
                    // entrydocket.Focus();

                });
                //return true to keep timer reccurring
                //return false to stop timer
                return false;
            }
        }

        private async void Entrydocket_Completed(object sender, EventArgs e)
        {
            await ValidateStock(branch_id);
        }

        /* private void Entrydocket_Unfocused(object sender, FocusEventArgs e)
         {
             Device.StartTimer(TimeSpan.FromMilliseconds(2000.0), TimerElapsed);

             bool TimerElapsed()
             {
                 Device.BeginInvokeOnMainThread(() =>
                 {
                     if (!focus_status)
                     {
                         //put here your code 
                         entrydocket.CursorPosition = entrydocket.Text.Length + 1;
                     }
                     //viewModel.DOCKETNUMBER = "";
                     // entrydocket.Focus();

                 });
                 //return true to keep timer reccurring
                 //return false to stop timer
                 return false;
             }
         }*/

        /* private async void Entrydocket_Completed(object sender, EventArgs e)
         {
             await ValidateStock(branch_id);
         }*/
        private async Task ValidateStock(string branch_id)
        {
            viewModel.IsBusy = true;
            viewModel.IsEnable = true;
            viewModel.IsVisible = true;

            try
            {

                if (!string.IsNullOrEmpty(entrydocket.Text))
                {
                    /* if (entrydocket.Text.Length == 10)
                     {*/
                    viewModel.ASSETID = entrydocket.Text;
                    //DisplayAlert("Alert", "Entry text changed", "Ok");

                    bool duplicatestatus = database.CheckDuplicateStockTally(viewModel.ASSETID);
                    if (duplicatestatus)
                    {
                        viewModel.IsBusy = false;
                        viewModel.IsEnable = false;
                        viewModel.IsVisible = false;
                        ProjectConstants.SpeakNow("Already Scanned");
                        // entrydocket.CursorPosition = 0;
                        Device.BeginInvokeOnMainThread(() => {
                            entrydocket.CursorPosition = entrydocket.Text.Length + 1;
                            viewModel.ASSETID = "";
                        });

                    }
                    else
                    {

                        // List<FillStockList> docketdetails = new List<FillStockList>();
                        var docketdetails = from STockTallyDetails mydata in viewModel.ObjStockList where mydata.Asset_id == viewModel.ASSETID select mydata;
                        List<STockTallyDetails> plist = new List<STockTallyDetails>();
                        // FillStockList sTockTake = new FillStockList();
                        STockTallyDetails sTockTake = new STockTallyDetails();
                        if (docketdetails.Count<STockTallyDetails>() > 0)
                        {


                            database.Update_Stock(viewModel.ASSETID);

                            ProjectConstants.SpeakNow("ok");


                        }
                        else
                        {
                            await DisplayAlert("Alert", "Scanned stock (" + entrydocket.Text + ") does not belong to available stock", "OK");

                        }

                        // int k = database.Insert_Package(plist);
                        // ProjectConstants.SpeakNow("ok");

                    }


                }
                else
                {
                    viewModel.IsBusy = false;
                    viewModel.IsEnable = false;
                    viewModel.IsVisible = false;
                    // DependencyService.Get<IAudio>().PlayAudioFile(ProjectConstants.audio_alert_fail);
                    //await DisplayAlert("Alert", "Invalid article(" + entrydocket.Text + ") scanned", "OK");
                    // entrydocket.CursorPosition = 0;
                    // viewModel.DOCKETNUMBER = "";
                    Device.BeginInvokeOnMainThread(() => {
                        entrydocket.CursorPosition = entrydocket.Text.Length + 1;
                        viewModel.ASSETID = "";
                    });
                }


                //updating UI in main Thread
                Device.BeginInvokeOnMainThread(() => {


                    viewModel.ObjStockList = database.GetStockList(branch_id);

                    int totalArticle = viewModel.ObjStockList.Count;
                    int scanned_item = 0;
                    foreach (var articlecount in viewModel.ObjStockList)
                    {
                        if (articlecount.Status == "Found")
                        {
                            scanned_item = scanned_item + 1;
                        }

                        // totalScanned_Article = totalScanned_Article + count.Scanned_Article;
                    }
                    viewModel.TOTAL_STOCK = totalArticle;
                    viewModel.SCANNED_STOCK = scanned_item;

                    viewModel.IsBusy = false;
                    viewModel.IsEnable = false;
                    viewModel.IsVisible = false;
                    // entrydocket.CursorPosition = 0;
                    entrydocket.CursorPosition = entrydocket.Text.Length + 1;
                    viewModel.ASSETID = "";
                });
                viewModel.IsBusy = false;
                viewModel.IsEnable = false;
                viewModel.IsVisible = false;
                Device.BeginInvokeOnMainThread(() => {
                    entrydocket.CursorPosition = entrydocket.Text.Length + 1;
                    viewModel.ASSETID = "";
                });
            }
            catch (Exception excp)
            {
                viewModel.IsBusy = false;
                viewModel.IsEnable = false;
                viewModel.IsVisible = false;
                Crashes.TrackError(excp);
                // Common.SaveLogs(excp.StackTrace);
                Device.BeginInvokeOnMainThread(() => {
                    entrydocket.CursorPosition = entrydocket.Text.Length + 1;
                    viewModel.ASSETID = "";
                });
            }

        }

        private void logout_Clicked(object sender, EventArgs e)
        {
            Preferences.Set(Pref.LOGINSTATUS, false);

            Application.Current.MainPage = new MainPage();
        }

       
    }
}