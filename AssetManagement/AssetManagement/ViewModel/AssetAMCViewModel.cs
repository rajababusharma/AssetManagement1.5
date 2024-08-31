using AssetManagement.Constants;
using AssetManagement.Model;
using Microsoft.AppCenter.Crashes;
using Newtonsoft.Json;
using Plugin.Connectivity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace AssetManagement.ViewModel
{
    public class AssetAMCViewModel : BaseViewModel
    {
        public AssetAMCViewModel()
        {
            IsBusy = false;
            IsEnable = false;
            IsVisible = false;
            ShowMain = true;
            GetAllAssetsData();
        }

        private Xamarin.Forms.ImageSource image1 = (FileImageSource)ImageSource.FromFile("camera.png");
        public Xamarin.Forms.ImageSource Image1
        {
            get { return image1; }
            set
            {
                image1 = value;
                NotifyPropertyChanged("Image1");
            }
        }

        private bool _ShowMain = false;
        public bool ShowMain
        {
            get { return _ShowMain; }
            set
            {
                _ShowMain = value;
                NotifyPropertyChanged("ShowMain");
            }
        }

        private bool _Show_popuplayout = false;
        public bool Show_popuplayout
        {
            get { return _Show_popuplayout; }
            set
            {
                _Show_popuplayout = value;
                NotifyPropertyChanged("Show_popuplayout");
            }
        }


        private bool isBusy = false;
        public bool IsBusy
        {
            get { return isBusy; }
            set
            {
                isBusy = value;
                NotifyPropertyChanged("IsBusy");
            }
        }

        private bool isEnable = false;
        public bool IsEnable
        {
            get { return isEnable; }
            set
            {
                isEnable = value;
                NotifyPropertyChanged("IsEnable");
            }
        }
        private bool isvisible = false;
        public bool IsVisible
        {
            get { return isvisible; }
            set
            {
                isvisible = value;
                NotifyPropertyChanged("IsVisible");
            }
        }

        List<AssetAMCList> _objstockList;

        public List<AssetAMCList> ObjStockList
        {
            get { return _objstockList; }

            set
            {
                if (_objstockList != value)
                {
                    _objstockList = value;
                    // OnPropertyChanged("ObjPaymentList");
                    NotifyPropertyChanged("ObjStockList");
                }
            }
        }

        List<AssetAMCList> _searchObject;

        public List<AssetAMCList> SEARCHOBJECT
        {
            get { return _searchObject; }

            set
            {
                if (_searchObject != value)
                {
                    _searchObject = value;
                    // OnPropertyChanged("ObjPaymentList");
                    NotifyPropertyChanged("SEARCHOBJECT");
                }
            }
        }

        private string _ASSETID;
        public string ASSETID
        {
            get
            {
                return _ASSETID;
            }
            set
            {
                _ASSETID = value;
                NotifyPropertyChanged("ASSETID");

                //put here your code  
                // SYear = selected_year.Year;
            }
        }

        private string assetid;
        public string ASSET_ID
        {
            get
            {
                return assetid;
            }
            set
            {
                assetid = value;
                NotifyPropertyChanged("ASSET_ID");

                //put here your code  
                // SYear = selected_year.Year;
            }
        }

        private string assetname;
        public string ASSET_NAME
        {
            get
            {
                return assetname;
            }
            set
            {
                assetname = value;
                NotifyPropertyChanged("ASSET_NAME");

                //put here your code  
                // SYear = selected_year.Year;
            }
        }


        private string vendor;
        public string VENDOR_NAME
        {
            get
            {
                return vendor;
            }
            set
            {
                vendor = value;
                NotifyPropertyChanged("VENDOR_NAME");

                //put here your code  
                // SYear = selected_year.Year;
            }
        }

        private string amctype;
        public string AMC_TYPE
        {
            get
            {
                return amctype;
            }
            set
            {
                amctype = value;
                NotifyPropertyChanged("AMC_TYPE");

                //put here your code  
                // SYear = selected_year.Year;
            }
        }

        private string startdate;
        public string AMC_STARTDATE
        {
            get
            {
                return startdate;
            }
            set
            {
                startdate = value;
                NotifyPropertyChanged("AMC_STARTDATE");

                //put here your code  
                // SYear = selected_year.Year;
            }
        }

        private string enddate;
        public string AMC_ENDDATE
        {
            get
            {
                return enddate;
            }
            set
            {
                enddate = value;
                NotifyPropertyChanged("AMC_ENDDATE");

                //put here your code  
                // SYear = selected_year.Year;
            }
        }

        private string description;
        public string AMC_DESCRIPTION
        {
            get
            {
                return description;
            }
            set
            {
                description = value;
                NotifyPropertyChanged("AMC_DESCRIPTION");

                //put here your code  
                // SYear = selected_year.Year;
            }
        }

        private string amc_value;
        public string AMC_VALUE
        {
            get
            {
                return amc_value;
            }
            set
            {
                amc_value = value;
                NotifyPropertyChanged("AMC_VALUE");
            }
        }

        public async void GetAllAssetsData()
        {
            //null or empty field validation, check weather email and password is null or empty  

            if (CrossConnectivity.Current.IsConnected)
            {

                await GetData();


                // Task.Delay(10000).Wait();

            }
            else
            {
                // DependencyService.Get<IAudio>().PlayAudioFile(ProjectConstants.audio_alert_fail);
                await App.Current.MainPage.DisplayAlert("Alert", "No internet connection, please check and try again later.", "OK");

            }
            // App.Current.MainPage.DisplayAlert("Empty Values", "welcome", "OK");

        }

        public async Task GetData()
        {
            string branch_id = Preferences.Get(Pref.BRANCH, "");
            try
            {
                IsBusy = true;
                IsEnable = true;
                IsVisible = true;

                /* string ctype = "Unloading";*/
                // string ctype = Preferences.Get(ProjectConstants.CTYPE, "");


                var client = new System.Net.Http.HttpClient();
                //  client.BaseAddress = new Uri("http://114.143.156.30/");
                client.BaseAddress = new Uri(ProjectConstants.GETAMCDATA_API);



                var response = await client.GetAsync("GetAMCData");
                var responseJson = response.Content.ReadAsStringAsync().Result;

                AMCDetailsResponse stocktake = new AMCDetailsResponse();

                List<AssetAMCList> mystocklist = new List<AssetAMCList>();
                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    stocktake = JsonConvert.DeserializeObject<AMCDetailsResponse>(responseJson);
                    if (stocktake.Status.Equals("true"))
                    {
                        ObjStockList = stocktake.AssetAMCList;
                        SEARCHOBJECT = stocktake.AssetAMCList;
                        // int totalArticle = ObjStockList.Count;

                        // TOTAL_STOCK = totalArticle;


                    }
                    else
                    {
                        //DependencyService.Get<IAudio>().PlayAudioFile(ProjectConstants.audio_alert_fail);
                        await App.Current.MainPage.DisplayAlert("Exception", stocktake.Msg.ToString(), "Ok");
                        ObjStockList = null;
                    }

                }
                else
                {
                    // DependencyService.Get<IAudio>().PlayAudioFile(ProjectConstants.audio_alert_fail);
                    await App.Current.MainPage.DisplayAlert("Exception", response.ReasonPhrase, "Ok");
                    ObjStockList = null;
                }
                IsBusy = false;
                IsEnable = false;
                IsVisible = false;

            }
            catch (Exception excp)
            {
                // Common.SaveLogs(excp.StackTrace);
                IsBusy = false;
                IsEnable = false;
                IsVisible = false;

                // ObjStockList = database.GetStockList(branch_id);
                //await App.Current.MainPage.DisplayAlert("Exception", "Request could n, please try again later", "Ok");
                Crashes.TrackError(excp);

            }
        }
        public async void SearchAsset()
        {
            bool status = false;
            List<AssetAMCList> dkt = new List<AssetAMCList>();
            if (string.IsNullOrEmpty(ASSETID))
            {
                // await App.Current.MainPage..DisplayAlert("Alert", "Please enter docket number first.", "Ok");
                await App.Current.MainPage.DisplayAlert("Alert", "Please Scan Asset Id first.", "Ok");
            }
            else
            {
                foreach (AssetAMCList docketforpayment in SEARCHOBJECT)
                {
                    if (ASSETID.Trim().Equals(docketforpayment.Asset_id))
                    {
                        dkt.Add(docketforpayment);
                        ObjStockList = dkt;
                        status = true;
                        break;
                    }

                }
                if (!status)
                {
                    ObjStockList = null;
                    await App.Current.MainPage.DisplayAlert("Alert", "Asset not found", "Ok");
                }

            }
        }

        public Command CLEAR
        {
            get
            {

                return new Command(ShowHideLayout);
            }
        }

        private void ShowHideLayout(object obj)
        {
            ShowMain = true;
            Show_popuplayout = false;
        }
    }
}
