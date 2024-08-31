using AssetManagement.Model;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Xamarin.Forms;

namespace AssetManagement.Converters
{
    class TextColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
                return false;
            var itemdata = value as STockTallyDetails;
           
            string scanstatus = itemdata.Status;
            //if (article.Equals(scanned_article))
            if (scanstatus.Equals("Found"))
            {

                return Color.Green;
            }
            else
            {
                return Color.Black;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
