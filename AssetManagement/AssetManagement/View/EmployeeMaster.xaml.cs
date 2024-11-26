using AssetManagement.Constants;
using AssetManagement.Model;
using AssetManagement.ViewModel;
using Plugin.Connectivity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AssetManagement.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class EmployeeMaster : ContentPage
    {
        EmployeeMasterViewModel viewModel;
        public EmployeeMaster()
        {
            InitializeComponent();
            viewModel = new EmployeeMasterViewModel();
            BindingContext = viewModel;
        }

        private void pkrbranch_ItemSelected(object sender, CustomRenderer.ItemSelectedEventArgs e)
        {
            viewModel.BRANCH = viewModel.BranchList[e.SelectedIndex];
        }

        private async void pkrlocation_ItemSelected(object sender, CustomRenderer.ItemSelectedEventArgs e)
        {
            viewModel.LOCATION = viewModel.LocationList[e.SelectedIndex];
           await viewModel.GetBranches(viewModel.LOCATION);
        }

        private void pkrdepartment_ItemSelected(object sender, CustomRenderer.ItemSelectedEventArgs e)
        {
            viewModel.DEPARTMENT = viewModel.DepartmentList[e.SelectedIndex];
        }
    }
}