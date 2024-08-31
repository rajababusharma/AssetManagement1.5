using AssetManagement.Model;
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
    public partial class ChartDetailPage : ContentPage
    {
        ChartDetailViewModel viewModel;
        public ChartDetailPage(List<AssetCategoryCountList> lists, string branch)
        {
            InitializeComponent();
            viewModel = new ChartDetailViewModel(lists, branch);
            BindingContext = viewModel;
        }
    }
}