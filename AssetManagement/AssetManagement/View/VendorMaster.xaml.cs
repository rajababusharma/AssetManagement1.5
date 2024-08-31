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
    public partial class VendorMaster : ContentPage
    {
        VendorMasterViewModel viewModel;
        public VendorMaster()
        {
            InitializeComponent();
            viewModel = new VendorMasterViewModel();
            BindingContext = viewModel;
        }
    }
}