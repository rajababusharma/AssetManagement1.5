using AssetManagement.Constants;
using AssetManagement.Model;
using AssetManagement.ViewModel;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace AssetManagement.Converters
{
    public class DisplayNotification : IValueConverter
    {
       
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
           
            bool isvisible = false;
            string notificationText = (string)value;
            // int count= Preferences.Get(Pref.MOVENOTIFICATION, 0);
            if (notificationText != null)
            {
                if (notificationText.Equals("0") || string.IsNullOrEmpty(notificationText))
                {
                    isvisible = false;
                }
                else
                {
                    isvisible = true;
                }
            }
           
            return isvisible;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }


       
       
       
    }
}
