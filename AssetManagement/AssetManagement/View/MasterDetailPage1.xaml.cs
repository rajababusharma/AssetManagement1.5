﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AssetManagement.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MasterDetailPage1 : MasterDetailPage
    {
        public MasterDetailPage1()
        {
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);
            MasterPage.ListView.ItemSelected += ListView_ItemSelected;
        }

        private void ListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var item = e.SelectedItem as MasterDetailPage1MasterMenuItem;
            if (item == null)
                return;

            var page = (Page)Activator.CreateInstance(item.TargetType);
            page.Title = item.Title;

           // Detail = new NavigationPage(page);
            (this.Detail as NavigationPage).PushAsync(page);
            IsPresented = false;

            MasterPage.ListView.SelectedItem = null;
        }
    }
}