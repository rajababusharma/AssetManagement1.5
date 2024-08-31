using AssetManagement.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace AssetManagement.ViewModel
{
    public class ChartDetailViewModel : BaseViewModel
    {
        public ChartDetailViewModel(List<AssetCategoryCountList> lists,string branch)
        {
            TOOLTRIPBRANCH = branch;
            ASSETCATEGORYCOUNT=lists;
        }
        string _Tooltripbranch = "";

        public string TOOLTRIPBRANCH
        {
            get { return _Tooltripbranch; }

            set
            {
                if (_Tooltripbranch != value)
                {
                    _Tooltripbranch = value;
                    // OnPropertyChanged("ObjPaymentList");
                    NotifyPropertyChanged("TOOLTRIPBRANCH");
                }
            }
        }

        List<AssetCategoryCountList> _assetcategoryCount;

        public List<AssetCategoryCountList> ASSETCATEGORYCOUNT
        {
            get { return _assetcategoryCount; }

            set
            {
                if (_assetcategoryCount != value)
                {
                    _assetcategoryCount = value;
                    // OnPropertyChanged("ObjPaymentList");
                    NotifyPropertyChanged("ASSETCATEGORYCOUNT");
                }
            }
        }
    }
}
