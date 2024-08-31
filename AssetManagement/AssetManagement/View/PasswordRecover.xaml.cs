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
    public partial class PasswordRecover : ContentPage
    {
        RecoverPasswordViewModel viewModel;
        public PasswordRecover()
        {
            InitializeComponent();
            viewModel = new RecoverPasswordViewModel();
            BindingContext = viewModel;
        }
    }
}