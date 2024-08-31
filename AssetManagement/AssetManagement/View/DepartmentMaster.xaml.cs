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
    public partial class DepartmentMaster : ContentPage
    {
        DepartmentMasterViewModel Department;
        public DepartmentMaster()
        {
            InitializeComponent();
            Department = new DepartmentMasterViewModel();
            BindingContext = Department;
        }
    }
}