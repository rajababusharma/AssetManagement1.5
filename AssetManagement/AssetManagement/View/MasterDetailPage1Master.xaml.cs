using AssetManagement.Constants;
using AssetManagement.Model;
using AssetManagement.ViewModel;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AssetManagement.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MasterDetailPage1Master : ContentPage
    {
        public ListView ListView;
        MasterDetailPage1MasterViewModel viewModel;
        public MasterDetailPage1Master()
        {
            InitializeComponent();
            viewModel=new MasterDetailPage1MasterViewModel();
            BindingContext = viewModel;

           
            ListView = MenuItemsListView;

            var verifyDocket = new TapGestureRecognizer();
            verifyDocket.Tapped += async (s, e) =>
            {
                //your code
                await Navigation.PushModalAsync(new MoveNotification());
            };
            stkNotification.GestureRecognizers.Add(verifyDocket);

            var amcnotification = new TapGestureRecognizer();
            amcnotification.Tapped += async (s, e) =>
            {
                //your code
                await Navigation.PushModalAsync(new AMC_InsuranceNotification());
               // await App.Current.MainPage.Navigation.PushAsync(new AMC_InsuranceNotification());
            };
            AMCNotification.GestureRecognizers.Add(amcnotification);
        }

      
        protected async override void OnAppearing()
        {
            base.OnAppearing();
             await CommonClass.GetMove_AMCNotification();
           
           // viewModel.NOTIFICATIONTEXT = "2";
        }

       
    }
}