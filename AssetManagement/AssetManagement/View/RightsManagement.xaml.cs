using AssetManagement.Constants;
using AssetManagement.CustomRenderer;
using AssetManagement.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AssetManagement.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RightsManagement : ContentPage
    {
        RightsManageentViewModel viewModel;
        public RightsManagement()
        {
            InitializeComponent();
            viewModel = new RightsManageentViewModel();
            BindingContext = viewModel;
        }

        private void logout_Clicked(object sender, EventArgs e)
        {
            Preferences.Set(Pref.LOGINSTATUS, false);

            Application.Current.MainPage = new MainPage();
        }

        private void pkrusers_ItemSelected(object sender, ItemSelectedEventArgs e)
        {
            viewModel.SELECTESUSER = viewModel.UsersList[e.SelectedIndex];
            viewModel.GetUsersRights(viewModel.SELECTESUSER);
        }
    }
}