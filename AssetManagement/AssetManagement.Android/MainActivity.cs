using System;

using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Android.Support.V4.Content;
using Android;
using Android.Support.V4.App;
using Acr.UserDialogs;
using Android.Content;
using AssetManagement.View;
using Android.Content.Res;
using MediaManager;

namespace AssetManagement.Droid
{
    [Activity(LaunchMode = LaunchMode.SingleTop, Label = "Asset Management", Icon = "@drawable/icon", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation | ConfigChanges.UiMode | ConfigChanges.ScreenLayout | ConfigChanges.SmallestScreenSize )]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;
            
            //Register Syncfusion license
            Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("NTgxMDIxQDMxMzgyZTM0MmUzMGpYYzZQNVBqZDF3Ri9XVTk4dGNaeXpSZWV3Yi9kVlhraUxSbVIyMzFORlU9");
            base.OnCreate(savedInstanceState);
            
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            global::Xamarin.Forms.Forms.Init(this, savedInstanceState);
            UserDialogs.Init(this);
            LoadApplication(new App());
        }
        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
        protected override void OnResume()
        {
            base.OnResume();

            if (ContextCompat.CheckSelfPermission(this, Manifest.Permission.Camera) == (int)Android.Content.PM.Permission.Granted)
            {
            }
            else
            {
                ActivityCompat.RequestPermissions(this, new string[] { Manifest.Permission.Camera }, 533);
            }

           
            /*  if (ContextCompat.CheckSelfPermission(this, Manifest.Permission.WriteExternalStorage) == (int)Android.Content.PM.Permission.Granted)
              {
              }
              else
              {
                  ActivityCompat.RequestPermissions(this, new string[] { Manifest.Permission.WriteExternalStorage }, 534);
              }
              if (ContextCompat.CheckSelfPermission(this, Manifest.Permission.ReadExternalStorage) == (int)Android.Content.PM.Permission.Granted)
              {
              }
              else
              {
                  ActivityCompat.RequestPermissions(this, new string[] { Manifest.Permission.ReadExternalStorage }, 535);
              }*/

            /* if (ContextCompat.CheckSelfPermission(this, Manifest.Permission.AccessCoarseLocation) == (int)Android.Content.PM.Permission.Granted)
             {
             }
             else
             {
                 ActivityCompat.RequestPermissions(this, new string[] { Manifest.Permission.AccessCoarseLocation }, 536);
             }*/
            if (ContextCompat.CheckSelfPermission(this, Manifest.Permission.AccessFineLocation) == (int)Android.Content.PM.Permission.Granted)
            {
            }
            else
            {
                ActivityCompat.RequestPermissions(this, new string[] { Manifest.Permission.AccessFineLocation }, 537);
            }
           // shouldRun = false;
           // Preferences.Set(ProjectConstants.SHOULDRUN, true);

        }

        protected override void OnNewIntent(Intent intent)
        {
            base.OnNewIntent(intent);
            var action = intent.Action;

            App.Current.MainPage.Navigation.PushAsync(new MoveNotification());
        }
    }
}