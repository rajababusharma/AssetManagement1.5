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

namespace AssetManagement.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Transfer : ContentPage
    {
        TransferViewModel ViewModel;
        STockTallyDetails _details;
        MasterDetailPage1MasterViewModel masterDetail;
        public Transfer(STockTallyDetails details)
        {
            InitializeComponent();
            ViewModel = new TransferViewModel(details);
            BindingContext = ViewModel;
            _details = new STockTallyDetails();
            _details = details;
            masterDetail = new MasterDetailPage1MasterViewModel();
            masterDetail.USERNAME = "Rajababu Sharma";
           // ViewModel.GetEmployeeList();


        }

        private void logout_Clicked(object sender, EventArgs e)
        {
            Preferences.Set(Pref.LOGINSTATUS, false);

            Application.Current.MainPage = new MainPage();
        }

        private void pkremployee_ItemSelected(object sender, ItemSelectedEventArgs e)
        {
            ViewModel.TOEMPLOYEE = ViewModel.EmployeeList[e.SelectedIndex];
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
          
        }
    }
}