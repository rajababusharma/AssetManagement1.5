﻿using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using AssetManagement.CustomRenderer;
using AssetManagement.Droid.Implimentations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
[assembly:ExportRenderer(typeof(EntryBorder),typeof(EntryBorderRenderer))]
namespace AssetManagement.Droid.Implimentations
{
  
    public class EntryBorderRenderer:EntryRenderer
    {
        public EntryBorderRenderer(Context context) : base(context)
        {

        }

        protected override void OnElementChanged(ElementChangedEventArgs<Entry> e)
        {
            base.OnElementChanged(e);
            if (Control != null)
            {
                Control.Background = this.Resources.GetDrawable(Resource.Drawable.RoundedCornerEntry);
                
                Control.SetPadding(20, 10, 10, 3);
            }
        }
    }
}