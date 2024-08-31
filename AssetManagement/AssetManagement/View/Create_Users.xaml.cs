using AssetManagement.ViewModel;
using Microsoft.AppCenter.Crashes;
using Plugin.Media;
using System;
using System.Threading.Tasks;
using Android.Graphics;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Color = Xamarin.Forms.Color;
using System.Threading;

namespace AssetManagement.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Create_Users : ContentPage
    {
        CreateUserViewModel viewModel;
        public Create_Users()
        {
            InitializeComponent();
            viewModel = new CreateUserViewModel();
            BindingContext = viewModel;

            // image click event to capture images
            var _imageCapture1 = new TapGestureRecognizer();
            _imageCapture1.Tapped += async (s, e) =>
            {
                await CaptureImage1();
            };
            imguser.GestureRecognizers.Add(_imageCapture1);
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
                            Name = "user.jpg"
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
                // Crashes.TrackError(excp);
            }

        }

        private void confirmpassword_TextChanged(object sender, TextChangedEventArgs e)
        {
           
            try
            {
                string theBase = viewModel.PASSWORD;
                string confirmation = e.NewTextValue;
              var IsValid = (bool)theBase?.Equals(confirmation);
                ((Entry)sender).TextColor = IsValid ? Color.FromHex("#000000")/*correct*/ : Color.FromHex("#ff0000")/*incorrect*/;
            }
            catch (System.InvalidOperationException excp)
            {
                Crashes.TrackError(excp);
                //string theBase = viewModel.PASSWORD;
                // string confirmation = e.NewTextValue;
                //((Entry)sender).TextColor = IsValid ? Color.FromHex("#000000")/*correct*/ : Color.FromHex("#ff0000")/*incorrect*/;
            }
        }

        private void pkrlocation_ItemSelected(object sender, CustomRenderer.ItemSelectedEventArgs e)
        {
            viewModel.LOCATION = viewModel.LocationList[e.SelectedIndex];
        }

        private void pkrbranch_ItemSelected(object sender, CustomRenderer.ItemSelectedEventArgs e)
        {
            viewModel.BRANCH = viewModel.BranchList[e.SelectedIndex];
        }

        private void pkrdepartment_ItemSelected(object sender, CustomRenderer.ItemSelectedEventArgs e)
        {
            viewModel.DEPARTMENT = viewModel.DepartmentList[e.SelectedIndex];
        }
    }
}