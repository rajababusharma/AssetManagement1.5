using AssetManagement.Constants;
using AssetManagement.ViewModel;
using Plugin.Media;
using System;
using Android.Graphics;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using ZXing.Mobile;
using ZXing.Net.Mobile.Forms;
using System.Threading;
using AssetManagement.Model;

namespace AssetManagement.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CreateAMC : ContentPage
    {
        CreateAMCViewModel vm;
        STockTallyDetails _details;
        public CreateAMC(STockTallyDetails details)
        {
            InitializeComponent();
            vm = new CreateAMCViewModel();
            BindingContext = vm;
            _details = new STockTallyDetails();
            _details = details;
            vm.ASSETID = _details.Asset_id;
            vm.Asset_name = _details.Asset_name;
            // image click event to capture images
            var _imageCapture1 = new TapGestureRecognizer();
            _imageCapture1.Tapped += async (s, e) =>
            {
                await CaptureImage1();
            };
            click1.GestureRecognizers.Add(_imageCapture1);
            
        }
        public CreateAMC(string ast_id,string astname, string vENDOR, string aMC_TYPE, DateTime sTARTDATE, DateTime eNDDATE, DateTime aLERTDATE, string dESCRIPTION, string aMC_VALUE)
        {
            InitializeComponent();
            vm = new CreateAMCViewModel();
            BindingContext = vm;
            _details = new STockTallyDetails();
           
            vm.ASSETID = ast_id;
            vm.Asset_name = astname;
            // image click event to capture images
            var _imageCapture1 = new TapGestureRecognizer();
            _imageCapture1.Tapped += async (s, e) =>
            {
                await CaptureImage1();
            };
            click1.GestureRecognizers.Add(_imageCapture1);
            vm.SELECTEDVENDOR_INDEX = vm.VendorList.IndexOf(vENDOR);
            vm.Vendor = vENDOR;
            vm.AMCTYPE_INDEX = vm.AMCTYPELIST.IndexOf(aMC_TYPE);
            vm.AMCTYPE = aMC_TYPE;
            vm.START_DATE = sTARTDATE;
            vm.END_DATE = eNDDATE;
            vm.ALERT_DATE = aLERTDATE;
            vm.AMCVALUE = aMC_VALUE;
            vm.DESCRIPTION = dESCRIPTION;

        }

        private void logout_Clicked(object sender, EventArgs e)
        {
            Preferences.Set(Pref.LOGINSTATUS, false);

            Application.Current.MainPage = new MainPage();
        }

        private void pkrvendor_ItemSelected(object sender, CustomRenderer.ItemSelectedEventArgs e)
        {
            vm.Vendor = vm.VendorList[e.SelectedIndex];
        }

        private void pkramctype_ItemSelected(object sender, CustomRenderer.ItemSelectedEventArgs e)
        {
            vm.AMCTYPE = vm.AMCTYPELIST[e.SelectedIndex];
        }

        private void pkrAMCStartDate_DateSelected(object sender, DateChangedEventArgs e)
        {
            vm.START_DATE = e.NewDate.Date;
        }

        private void pkrAMCEndDate_DateSelected(object sender, DateChangedEventArgs e)
        {
            vm.END_DATE = e.NewDate.Date;
        }

        private void pkrAMCAlertDate_DateSelected(object sender, DateChangedEventArgs e)
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