using AssetManagement.ViewModel;
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
    public partial class SubCategoryMaster : ContentPage
    {
        BranchMasterViewModel viewModel;
        public SubCategoryMaster()
        {
            InitializeComponent();
            viewModel = new BranchMasterViewModel();
            BindingContext = viewModel;
        }

        private void pkrcategory_ItemSelected(object sender, CustomRenderer.ItemSelectedEventArgs e)
        {
            viewModel.CATEGORY_DESCRIPTION = viewModel.CategoryList[e.SelectedIndex];
        }
    }
}