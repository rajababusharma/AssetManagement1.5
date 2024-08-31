﻿using Android.Graphics;
using AssetManagement.Constants;
using AssetManagement.Model;
using AssetManagement.ViewModel;
using Plugin.Media;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using ZXing.Mobile;
using ZXing.Net.Mobile.Forms;

namespace AssetManagement.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CreateInsurance : ContentPage
    {
        CreateInsuranceViewModel vm;
        STockTallyDetails _details;
        public CreateInsurance(STockTallyDetails details)
        {
            InitializeComponent();
            vm = new CreateInsuranceViewModel();
            BindingContext = vm;
            _details = new STockTallyDetails();
            _details = details;
            vm.ASSETID=_details.Asset_id;
            vm.ASSETNAME = _details.Asset_name;
            // image click event to capture images
            var _imageCapture1 = new TapGestureRecognizer();
            _imageCapture1.Tapped += async (s, e) =>
            {
                await CaptureImage1();
            };
            click1.GestureRecognizers.Add(_imageCapture1);
        }
        public CreateInsurance(string ast_id, string astnames, string policy_Date, string insuranceCompany, string policy_No, string alert_Date, string policy_Name, string premium, string due_Date)
        {
            InitializeComponent();
            vm = new CreateInsuranceViewModel();
           
            BindingContext = vm;
            _details = new STockTallyDetails();
           
            vm.ASSETID = ast_id;
            vm.ASSETNAME = astnames;
            // image click event to capture images
            var _imageCapture1 = new TapGestureRecognizer();
            _imageCapture1.Tapped += async (s, e) =>
            {
                await CaptureImage1();
            };
            click1.GestureRecognizers.Add(_imageCapture1);

            // vm.SELECTEDMODE_INDEX = vm.PAYMENTMODELIST.IndexOf();
            vm.START_DATE = Convert.ToDateTime(policy_Date);
            vm.INSURANCE_COMPANYNAME = insuranceCompany;
            vm.POLICYNUMBER = policy_No;
            vm.ALERT_DATE = Convert.ToDateTime(alert_Date);
            vm.POLICYNAME = policy_Name;
            vm.DUE_DATE = Convert.ToDateTime(due_Date);
            vm.PREMIUM = premium;
        }

        private void logout_Clicked(object sender, EventArgs e)
        {
            Preferences.Set(Pref.LOGINSTATUS, false);

            Application.Current.MainPage = new MainPage();
        }

        private void pkrPolicyDate_DateSelected(object sender, DateChangedEventArgs e)
        {
            vm.START_DATE= e.NewDate.Date;
        }

        private void pkrDueDate_DateSelected(object sender, DateChangedEventArgs e)
        {
            vm.DUE_DATE = e.NewDate.Date;
        }

        private void pkrmodeofpayment_ItemSelected(object sender, CustomRenderer.ItemSelectedEventArgs e)
        {
            vm.PAYMENTMODE = vm.PAYMENTMODELIST[e.SelectedIndex];
        }

        private void pkrAlertDate_DateSelected(object sender, DateChangedEventArgs e)
        {
            vm.ALERT_DATE = e.NewDate.Date;
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
                            Name = vm.ASSETID + ".jpg"
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
                        // Crashes.TrackError(excp);

                    }

                    vm.IMAGE1_BASE64 = base64;

                    //  database.Update_TruckDetails_Images1(base64, viewModel.SELECTEDPUSLIST);
                    // await DisplayAlert("File Location", file.Path, "OK");
                    // Preferences.Set(ProjectConstants.BASE64_LOADING1, base64);
                    // Preferences.Set(ProjectConstants.CLICK1_IMAGE_ACTUALLOADING, file.Path);
                    vm.Image1 = ImageSource.FromStream(() =>
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
               // Crashes.TrackError(excp);
            }

        }
    }
}