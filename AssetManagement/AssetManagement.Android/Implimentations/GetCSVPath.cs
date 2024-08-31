using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using AssetManagement.Droid.Implimentations;
using AssetManagement.Interface;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Xamarin.Essentials;
using Xamarin.Forms;

[assembly: Dependency(typeof(GetCSVPath))]
namespace AssetManagement.Droid.Implimentations
{
    public class GetCSVPath : IShareReports
    {
        public string GetImagePath(string filename)
        {
            string filePath = "";
            var dir = FileSystem.AppDataDirectory;
           // var dir = Android.OS.Environment.GetExternalStoragePublicDirectory(Android.OS.Environment.DirectoryDownloads);
           // var pictures = dir.AbsolutePath;

            filePath = Path.Combine(dir, filename + ".png");


            return filePath;
        }

        string IShareReports.GetCSVPath(string filename)
        {
            string filePath = "";
            var dir = FileSystem.AppDataDirectory;
           // var dir = Android.OS.Environment.GetExternalStoragePublicDirectory(Android.OS.Environment.DirectoryDownloads);
           // var pictures = dir;

            filePath = Path.Combine(dir, filename+".csv");


            return filePath;
        }
    }
}