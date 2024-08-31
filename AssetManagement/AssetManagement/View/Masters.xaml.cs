using AssetManagement.Constants;
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
    public partial class Masters : ContentPage
    {
        public Masters()
        {
            InitializeComponent();
            var location = new TapGestureRecognizer();
            location.Tapped += async (s, e) =>
            {


                // await ValidateStock(branch_id);
                await Navigation.PushAsync(new LocationMaster());


            };
            txt_createlocation.GestureRecognizers.Add(location);
            var branch = new TapGestureRecognizer();
            branch.Tapped += async (s, e) =>
            {


                // await ValidateStock(branch_id);
                await Navigation.PushAsync(new BranchMaster());


            };
            txt_branch.GestureRecognizers.Add(branch);

           

            var category = new TapGestureRecognizer();
            category.Tapped += async (s, e) =>
            {


                // await ValidateStock(branch_id);
                await Navigation.PushAsync(new CategoryMaster());


            };
            txt_createCategory.GestureRecognizers.Add(category);

           
            var subCategory = new TapGestureRecognizer();
            subCategory.Tapped += async (s, e) =>
            {


                // await ValidateStock(branch_id);
                await Navigation.PushAsync(new SubCategoryMaster());


            };
            txt_createsubcategory.GestureRecognizers.Add(subCategory);

            var employee = new TapGestureRecognizer();
            employee.Tapped += async (s, e) =>
            {


                // await ValidateStock(branch_id);
                await Navigation.PushAsync(new EmployeeMaster());


            };
            txt_createemployee.GestureRecognizers.Add(employee);

            var vendor = new TapGestureRecognizer();
            vendor.Tapped += async (s, e) =>
            {


                // await ValidateStock(branch_id);
                await Navigation.PushAsync(new VendorMaster());


            };
            txt_createvendor.GestureRecognizers.Add(vendor);

            var department = new TapGestureRecognizer();
            department.Tapped += async (s, e) =>
            {


                // await ValidateStock(branch_id);
                await Navigation.PushAsync(new DepartmentMaster());


            };
            txt_createdepartment.GestureRecognizers.Add(department);

        }

        private void logout_Clicked(object sender, EventArgs e)
        {
            Preferences.Set(Pref.LOGINSTATUS, false);

            Application.Current.MainPage = new MainPage();
        }
    }
}