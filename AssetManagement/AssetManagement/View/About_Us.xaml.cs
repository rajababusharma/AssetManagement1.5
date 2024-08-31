using System;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AssetManagement.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class About_Us : ContentPage
    {
        Uri uri;
        public About_Us()
        {
            InitializeComponent();

            var tapwebsite = new TapGestureRecognizer();
            tapwebsite.Tapped += async (s, e) =>
            {


                // await ValidateStock(branch_id);
                uri = new Uri("https://aniche-solutions.com/");
               // await Browser.OpenAsync(uri, BrowserLaunchType.SystemPreferred);
                await Browser.OpenAsync(uri,BrowserLaunchMode.SystemPreferred);
            };
            website.GestureRecognizers.Add(tapwebsite);
        }
    }
}