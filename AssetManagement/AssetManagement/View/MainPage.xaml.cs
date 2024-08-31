using AssetManagement.Interface;
using AssetManagement.View;
using AssetManagement.ViewModel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace AssetManagement
{
    public partial class MainPage : ContentPage
    {
        LoginViewModel viewmodel;
        public MainPage()
        {
            InitializeComponent();
            viewmodel = new LoginViewModel();
            BindingContext = viewmodel;

           /* uint duration = 10 * 60 * 1000;
            imgmain.RotateTo(307 * 360, duration);
            imgmain.RotateXTo(251 * 360, duration);
            imgmain.RotateYTo(199 * 360, duration);*/

            NavigationPage.SetHasNavigationBar(this, false);
            appversion.Text = "App Version: " + DependencyService.Get<IAppVersionAndBuild>().GetVersionNumber();
            // label click event to recover password
            var _recoverPassword = new TapGestureRecognizer();
            _recoverPassword.Tapped += async (s, e) =>
            {
               // await DisplayAlert("Alert", "Coming Soon.....", "Ok");
                await App.Current.MainPage.Navigation.PushModalAsync(new PasswordRecover());
            };
            forgotpassword.GestureRecognizers.Add(_recoverPassword);

          //  viewmodel.IsCompanyExist();
        }

        /* private void btnregister_Clicked(object sender, EventArgs e)
         {
            // await Navigation.PushAsync(new Create_Users());
             Application.Current.MainPage = new NavigationPage(new Create_Users());
         }*/

        protected async override void OnAppearing()
        {
            viewmodel.IsCompanyExist();

            /*var iscompanyexist = CommonClass.IsCompanyExist();
            if(await CommonClass.IsCompanyExist())
            {
                viewmodel.IsEnableRegister = false;
            }*/
            // viewmodel.IsCompanyExist();
        }
    }
}
