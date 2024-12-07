﻿using Android.Graphics;
using AssetManagement.Constants;
using AssetManagement.Interface;
using AssetManagement.ViewModel;
using Microsoft.AppCenter.Crashes;
using Plugin.Media;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration.iOSSpecific;
using Xamarin.Forms.Xaml;
using ZXing.Mobile;
using ZXing.Net.Mobile.Forms;

namespace AssetManagement.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class UpdateAsset : ContentPage
    {
        UpdateAssetViewModel viewModel;
        public UpdateAsset()
        {
            InitializeComponent();
            viewModel = new UpdateAssetViewModel();
            BindingContext = viewModel;
            // image click event to capture images
            var _imageCapture1 = new TapGestureRecognizer();
            _imageCapture1.Tapped += async (s, e) =>
            {
                await CaptureImage1();
            };
            click1.GestureRecognizers.Add(_imageCapture1);

            var _search = new TapGestureRecognizer();
            _search.Tapped += async (s, e) =>
            {
                viewModel.SELECTED_EVENT = false;
                //search asset
                viewModel.GetData(viewModel.ASSETID);
                entrydocket.CursorPosition = entrydocket.Text.Length + 1;
                viewModel.ASSETID = "";
            };
            imgsearch.GestureRecognizers.Add(_search);



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
                            viewModel.ASSETID = result.Text.Trim();
                            DependencyService.Get<IAudio>().PlayAudioFile(ProjectConstants.BEEP);
                            // await ValidateStock(branch_id);
                            entrydocket.CursorPosition = entrydocket.Text.Length + 1;
                            viewModel.ASSETID = "";

                        });

                    };

                }
                catch (Exception excp)
                {
                    // Common.SaveLogs(excp.ToString());
                    // Common.SaveLogs(excp.StackTrace);
                    //GlobalScript.SeptemberDebugMessages("ERROR", "BtnScanQR_Clicked", "Opening ZXing Failed: " + ex);
                    await DisplayAlert("Alert", "Please try again", "OK");
                  
                }
                finally
                {
                    // entrydocket.CursorPosition = entrydocket.Text.Length + 1;
                    // viewModel.ASSETID = "";
                }

            };
            qrcode.GestureRecognizers.Add(verifyDocket);
        }

        private async Task CaptureImage1()
        {
            try
            {
                Plugin.Media.Abstractions.MediaFile file = null;
                await CrossMedia.Current.Initialize();
                string[] myaction = new string[2];
                myaction[0] = "Pick from device";
                myaction[1] = "Take a photo";
                var choice = await Acr.UserDialogs.UserDialogs.Instance.ActionSheetAsync("Please select the action", "Cancel", "", CancellationToken.None, myaction);
                if (!string.IsNullOrEmpty(choice) && choice != "Cancel")
                {
                    if (choice.Equals("Take a photo"))
                    {


                        if (!CrossMedia.Current.IsCameraAvailable || !CrossMedia.Current.IsTakePhotoSupported)
                        {

                            await DisplayAlert("No Camera", ":( No camera avaialble.", "OK");
                            return;
                        }

                        file = await CrossMedia.Current.TakePhotoAsync(new Plugin.Media.Abstractions.StoreCameraMediaOptions
                        {
                            PhotoSize = Plugin.Media.Abstractions.PhotoSize.Small,
                            Directory = "Sample",
                            Name = viewModel.ASSETID + ".jpg"
                        });



                    }
                    else
                    {
                        //pick photo
                        if (!CrossMedia.Current.IsPickPhotoSupported)
                        {
                            await DisplayAlert("Oops", "You Cannot pick an image", "Ok");
                            return;
                        }
                        file = await CrossMedia.Current.PickPhotoAsync(new Plugin.Media.Abstractions.PickMediaOptions
                        {
                            PhotoSize = Plugin.Media.Abstractions.PhotoSize.Small,
                            MaxWidthHeight = 300,
                            SaveMetaData = false
                        });
                    }

                    if (file == null)
                        return;

                    string base64 = "";
                    try
                    {
                        Bitmap mybitmap = BitmapFactory.DecodeFile(file.Path);


                        Bitmap resizedImage = Bitmap.CreateScaledBitmap(mybitmap, 300, 300, false);
                        var stream = new System.IO.MemoryStream();

                        resizedImage.Compress(Android.Graphics.Bitmap.CompressFormat.Png, 100, stream);
                        byte[] imageBytes = stream.ToArray();
                        base64 = Convert.ToBase64String(imageBytes);

                    }
                    catch (Exception excp)
                    {
                        Crashes.TrackError(excp);

                    }

                    viewModel.IMAGE1_BASE64 = base64;

                    //  database.Update_TruckDetails_Images1(base64, viewModel.SELECTEDPUSLIST);
                    // await DisplayAlert("File Location", file.Path, "OK");
                    // Preferences.Set(ProjectConstants.BASE64_LOADING1, base64);
                    // Preferences.Set(ProjectConstants.CLICK1_IMAGE_ACTUALLOADING, file.Path);
                    viewModel.Image1 = ImageSource.FromStream(() =>
                    {
                        var stream = file.GetStream();
                        file.Dispose();
                        return stream;
                    });
                }
            }
            catch (Exception excp)
            {


                await DisplayAlert("Error", excp.Message, "OK");
                Crashes.TrackError(excp);
            }

        }

       /* private void logout_Clicked(object sender, EventArgs e)
        {
            Preferences.Set(Pref.LOGINSTATUS, false);

           // Application.Current.MainPage = new MainPage();
        }*/

        private void pkrassetlife_ItemSelected(object sender, CustomRenderer.ItemSelectedEventArgs e)
        {
            viewModel.Asset_life = viewModel.asset_lifeList[e.SelectedIndex];
           // viewModel.SELECTEDLIFE_INDEX = e.SelectedIndex;
        }

        private void pkrvendor_ItemSelected(object sender, CustomRenderer.ItemSelectedEventArgs e)
        {
            try
            {
                viewModel.Vendor = viewModel.VendorList[e.SelectedIndex];
              //  viewModel.SELECTEDVENDOR_INDEX = e.SelectedIndex;
            }
            catch (Exception excp)
            {
                viewModel.Vendor = "No Data";
            }
        }

        private void pkrPurchaseDate_DateSelected(object sender, DateChangedEventArgs e)
        {
            viewModel.Purchase_date = e.NewDate.ToShortDateString();
        }

        private void pkrInstallDate_DateSelected(object sender, DateChangedEventArgs e)
        {
            viewModel.Install_date = e.NewDate.ToShortDateString();
        }

        private void pkrMFDDate_DateSelected(object sender, DateChangedEventArgs e)
        {
            viewModel.Mfd_date = e.NewDate.ToShortDateString();
        }

        private void pkrwarranty_ItemSelected(object sender, CustomRenderer.ItemSelectedEventArgs e)
        {
            viewModel.Warranty_period = viewModel.asset_warrantyList[e.SelectedIndex];
           // viewModel.SELECTEDWARRANT_INDEX = e.SelectedIndex;
        }

        private void pkrdepartment_ItemSelected(object sender, CustomRenderer.ItemSelectedEventArgs e)
        {
            viewModel.Department = viewModel.DepartmentList[e.SelectedIndex];
           // viewModel.SELECTEDDEPARTMENT_INDEX = e.SelectedIndex;
        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();
           /* if (!string.IsNullOrEmpty(viewModel.ASSETID))
            {
                await viewModel.GetData(viewModel.ASSETID);
            }*/
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            Preferences.Set(Pref.Asset_Id, null);
        }

        private void pkrlocation_SelectedIndexChanged(object sender, EventArgs e)
        {
           

            // Check if the selected index is valid
            if (pkrlocation.SelectedIndex != -1 && viewModel.SELECTED_EVENT)
            {
                // Get the selected item
                viewModel.Location = pkrlocation.SelectedItem.ToString();
               // viewModel.SELECTEDLOCATION_INDEX = pkrlocation.SelectedIndex;
                viewModel.GetBranches(viewModel.Location);
                pkrbranch.SelectedIndex = 1;
            }
        }

        private void pkrcategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Check if the selected index is valid
            if (pkrcategory.SelectedIndex != -1 && viewModel.SELECTED_EVENT)
            {
                // Get the selected item
                viewModel.Category = pkrcategory.SelectedItem.ToString();
               // viewModel.SELECTEDCATEGORY_INDEX = pkrcategory.SelectedIndex;
                viewModel.GetSubCategory(viewModel.Category);
                pkrsubcategory.SelectedIndex = 1;
            }
        }
    }
}