using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Xamarin.Forms;

namespace AssetManagement.Converters
{
    public class ImageConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string input = value as string;
            ImageSource One = null;
            if (input.Equals("Found"))
            {
                One = (FileImageSource)ImageSource.FromFile("ok.png");// new BitmapImage(new Uri("ms-appx:///Assets/nil.png"));
            }
            else
            {
                One = (FileImageSource)ImageSource.FromFile("notok.png");// new BitmapImage(new Uri("ms-appx:///Assets/nil.png"));
            }
            return One;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    public class ImageConverter_IsRepaired : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            int input = (int)value;
            ImageSource One = null;
            if (input==1)
            {
                One = (FileImageSource)ImageSource.FromFile("ok.png");// new BitmapImage(new Uri("ms-appx:///Assets/nil.png"));
            }
            else
            {
                One = (FileImageSource)ImageSource.FromFile("notok.png");// new BitmapImage(new Uri("ms-appx:///Assets/nil.png"));
            }
            return One;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    public class Menu_ImageConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string input = (string)value;
            ImageSource One = null;
            switch (input)
            {

                case "Stock Inventory":
                    One = (FileImageSource)ImageSource.FromFile("inventory.png");
                    break;

                case "Rights Management":
                    One = (FileImageSource)ImageSource.FromFile("rights.png");
                    break;
                case "Asset Management":
                    One = (FileImageSource)ImageSource.FromFile("assign.png");
                    break;
                case "Reports":
                    One = (FileImageSource)ImageSource.FromFile("report.png");
                    break;
                case "Asset's Notifications":
                    One = (FileImageSource)ImageSource.FromFile("notification.png");
                    break;
                case "Create Assets":
                    One = (FileImageSource)ImageSource.FromFile("createassets.png");
                    break;

                case "Search Asset":
                    One = (FileImageSource)ImageSource.FromFile("search.png");
                    break;

                case "Create New Users":
                    One = (FileImageSource)ImageSource.FromFile("users.jpg");
                    break;
                case "Assets Document":
                    One = (FileImageSource)ImageSource.FromFile("download.png");
                    break;
                case "About Us":
                    One = (FileImageSource)ImageSource.FromFile("icon.png");
                    break;
                default:
                    One = (FileImageSource)ImageSource.FromFile("inventory.png");
                    break;
            }


            return One;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
