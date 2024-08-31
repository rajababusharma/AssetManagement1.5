using AssetManagement.Constants;
using AssetManagement.Model;
using AssetManagement.ViewModel;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace AssetManagement.Converters
{
    public class TooltipConverter : IValueConverter
    {
        DashboardViewModel viewModel;
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            viewModel = new DashboardViewModel();
            if (value != null)
            {
               // var model = value as BranchWiseAssets;
                //TooltipLabel is another property in data model 
               // return model.TooltipLabel + " : " + model.YValue;
               // viewModel.BRANCH = value.ToString();
                Preferences.Set(Pref.TOOLTRIPBRANCH, value.ToString());
            }

            return value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
