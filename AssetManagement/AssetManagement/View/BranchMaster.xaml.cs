﻿using AssetManagement.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AssetManagement.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class BranchMaster : ContentPage
    {
        BranchMasterViewModel viewModel;
        public BranchMaster()
        {
            InitializeComponent();
            viewModel = new BranchMasterViewModel();
            BindingContext = viewModel;
        }

        private void pkrlocation_ItemSelected(object sender, CustomRenderer.ItemSelectedEventArgs e)
        {
            viewModel.LOCATION = viewModel.LocationList[e.SelectedIndex];
        }
    }
}