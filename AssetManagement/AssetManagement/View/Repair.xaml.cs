using Android.Graphics;
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
using AssetManagement.CustomRenderer;
using static AssetManagement.View.MasterDetailPage1Master;
using Microsoft.AppCenter.Crashes;

namespace AssetManagement.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Repair : ContentPage
    {
        RepairViewModel ViewModel;
        STockTallyDetails _details;
        MasterDetailPage1MasterViewModel masterDetail;
        public Repair(STockTallyDetails details)
        {
            InitializeComponent();
            ViewModel = new RepairViewModel(details);
            BindingContext = ViewModel;
            _details = new STockTallyDetails();
            _details = details;
            masterDetail = new MasterDetailPage1MasterViewModel();
            masterDetail.USERNAME = "Rajababu Sharma";
          

            // image click event to capture images
            var _imageCapture1 = new TapGestureRecognizer();
            _imageCapture1.Tapped += async (s, e) =>
            {
                await CaptureImage1();
            };
            click1.GestureRecognizers.Add(_imageCapture1);

            // image click event to capture images
            var _imageCapture2 = new TapGestureRecognizer();
            _imageCapture2.Tapped += async (s, e) =>
            {
                await CaptureImage2();
            };
            click2.GestureRecognizers.Add(_imageCapture2);

            // image click event to capture images
            var _imageCapture3 = new TapGestureRecognizer();
            _imageCapture3.Tapped += async (s, e) =>
            {
                await CaptureImage3();
            };
            click3.GestureRecognizers.Add(_imageCapture3);
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
                            Name = _details.Asset_id + "click1.jpg"
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

                    ViewModel.IMAGE1_BASE64 = base64;

                    //  database.Update_TruckDetails_Images1(base64, viewModel.SELECTEDPUSLIST);
                    // await DisplayAlert("File Location", file.Path, "OK");
                    // Preferences.Set(ProjectConstants.BASE64_LOADING1, base64);
                    // Preferences.Set(ProjectConstants.CLICK1_IMAGE_ACTUALLOADING, file.Path);
                    ViewModel.Image1 = ImageSource.FromStream(() =>
                    {
                        var stream = file.GetStream();
                        file.Dispose();
                        return stream;
                    });
                }
            }
            catch (Exception excp)
            {

                Crashes.TrackError(excp);
                await DisplayAlert("Error", excp.Message, "OK");

            }

        }

        private async Task CaptureImage3()
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
                            Name = _details.Asset_id + "click3.jpg"
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

                    ViewModel.IMAGE3_BASE64 = base64;

                    //  database.Update_TruckDetails_Images1(base64, viewModel.SELECTEDPUSLIST);
                    // await DisplayAlert("File Location", file.Path, "OK");
                    // Preferences.Set(ProjectConstants.BASE64_LOADING1, base64);
                    // Preferences.Set(ProjectConstants.CLICK1_IMAGE_ACTUALLOADING, file.Path);
                    ViewModel.Image3 = ImageSource.FromStream(() =>
                    {
                        var stream = file.GetStream();
                        file.Dispose();
                        return stream;
                    });
                }


            }
            catch (Exception excp)
            {
                Crashes.TrackError(excp);
                await DisplayAlert("Error", excp.Message, "OK");
            }
            finally
            {


            }
        }
        private async Task CaptureImage2()
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
                            Name = _details.Asset_id + "click2.jpg"
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

                    ViewModel.IMAGE2_BASE64 = base64;

                    //  database.Update_TruckDetails_Images1(base64, viewModel.SELECTEDPUSLIST);
                    // await DisplayAlert("File Location", file.Path, "OK");
                    // Preferences.Set(ProjectConstants.BASE64_LOADING1, base64);
                    // Preferences.Set(ProjectConstants.CLICK1_IMAGE_ACTUALLOADING, file.Path);
                    ViewModel.Image2 = ImageSource.FromStream(() =>
                    {
                        var stream = file.GetStream();
                        file.Dispose();
                        return stream;
                    });
                }


            }
            catch (Exception excp)
            {
                Crashes.TrackError(excp);
                await DisplayAlert("Error", excp.Message, "OK");
            }
            finally
            {


            }
        }

        private void logout_Clicked(object sender, EventArgs e)
        {
            Preferences.Set(Pref.LOGINSTATUS, false);

            Application.Current.MainPage = new MainPage();
        }

        private void pkrvendor_ItemSelected(object sender, ItemSelectedEventArgs e)
        {
            ViewModel.SELECTEDVENDOR = ViewModel.VendorList[e.SelectedIndex];
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
          
        }

        private void datepicker1_DateSelected(object sender, DateChangedEventArgs e)
        {
            ViewModel.SELECTEDDTAE = e.NewDate.ToShortDateString();
;        }
    }
}