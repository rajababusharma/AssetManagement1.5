using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using AssetManagement.Droid.Implimentations;
using AssetManagement.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xamarin.Forms;

[assembly: Dependency(typeof(VersionAndBuild_Android))]
namespace AssetManagement.Droid.Implimentations
{
    public class VersionAndBuild_Android : IAppVersionAndBuild
    {
        PackageInfo _appInfo;
        public VersionAndBuild_Android()
        {
            var context = Android.App.Application.Context;
            _appInfo = context.PackageManager.GetPackageInfo(context.PackageName, 0);
        }
        public string GetBuildNumber()
        {
            return _appInfo.VersionCode.ToString();
        }

        public string GetVersionNumber()
        {
            return _appInfo.VersionName;
        }
    }
}