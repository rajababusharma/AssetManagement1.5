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
    public partial class Reports : ContentPage
    {
        public Reports()
        {
            InitializeComponent();

            // asset found missing report
            var foundmissing = new TapGestureRecognizer();
            foundmissing.Tapped += async (s, e) =>
            {


                // await ValidateStock(branch_id);
               await Navigation.PushAsync(new FoundMissingReports());


            };
            txt_foundreport.GestureRecognizers.Add(foundmissing);

            // asset all reports
            var assetreport = new TapGestureRecognizer();
            assetreport.Tapped += async (s, e) =>
            {


                // await ValidateStock(branch_id);
                await Navigation.PushAsync(new AssetsReport());


            };
            txt_astreport.GestureRecognizers.Add(assetreport);

            // asset disposal report
            var assetdispose = new TapGestureRecognizer();
            assetdispose.Tapped += async (s, e) =>
            {


                // await ValidateStock(branch_id);
                await Navigation.PushAsync(new DisposalReport());


            };
            txt_disposereport.GestureRecognizers.Add(assetdispose);

            // asset AMC report
            var assetamc = new TapGestureRecognizer();
            assetamc.Tapped += async (s, e) =>
            {


                // await ValidateStock(branch_id);
                await Navigation.PushAsync(new AMCReports());


            };
            txt_amcreport.GestureRecognizers.Add(assetamc);

            // asset Insurance report
            var assetinsurance = new TapGestureRecognizer();
            assetinsurance.Tapped += async (s, e) =>
            {


                // await ValidateStock(branch_id);
                await Navigation.PushAsync(new IsuranceReports());


            };
            txt_insurance.GestureRecognizers.Add(assetinsurance);

            // asset move report
            var assetmove = new TapGestureRecognizer();
            assetmove.Tapped += async (s, e) =>
            {


                // await ValidateStock(branch_id);
                await Navigation.PushAsync(new MoveReports());


            };
            txt_movereport.GestureRecognizers.Add(assetmove);

            // asset repair report
            var assetrepair = new TapGestureRecognizer();
            assetrepair.Tapped += async (s, e) =>
            {


                // await ValidateStock(branch_id);
                await Navigation.PushAsync(new RepairReports());


            };
            txt_repair.GestureRecognizers.Add(assetrepair);
        }

       

        private void logout_Clicked(object sender, EventArgs e)
        {
            Preferences.Set(Pref.LOGINSTATUS, false);

            Application.Current.MainPage = new MainPage();
        }
    }
}