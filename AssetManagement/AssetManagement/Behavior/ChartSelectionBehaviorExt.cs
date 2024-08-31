using AssetManagement.Constants;
using AssetManagement.Model;
using AssetManagement.ViewModel;
using Syncfusion.SfChart.XForms;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Essentials;

namespace AssetManagement.Behavior
{
    public class ChartSelectionBehaviorExt : ChartSelectionBehavior
    {
        DashboardViewModel viewModel;
        //  public List<BranchWiseAssets> data = new List<BranchWiseAssets>();
        protected async override void OnSelectionChanged(ChartSelectionEventArgs arg)
        {
            base.OnSelectionChanged(arg);
            //var selectedSeres = arg.SelectedSeries;
            //var dataPointIndex = arg.SelectedDataPointIndex;

          /*  viewModel = new DashboardViewModel();
            string branch = Preferences.Get(Pref.TOOLTRIPBRANCH, "");
           await viewModel.GetData(branch);*/
           

        }
       

       
    }
}
