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
    public class AssetDisposeViewModel : BaseViewModel
    {
        public AssetDisposeViewModel()
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
        private Xamarin.Forms.ImageSource image2 = (FileImageSource)ImageSource.FromFile("camera.png");
        public Xamarin.Forms.ImageSource Image2
        {
            get { return image2; }
            set
            {
                image2 = value;
                NotifyPropertyChanged("Image2");
            }
        }

        private Xamarin.Forms.ImageSource image3 = (FileImageSource)ImageSource.FromFile("camera.png");
        public Xamarin.Forms.ImageSource Image3
        {
            get { return image3; }
            set
            {
                image3 = value;
                NotifyPropertyChanged("Image3");
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

        List<DisposalReportList> _objstockList;

        public List<DisposalReportList> ObjStockList
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

        List<DisposalReportList> _searchObject;

        public List<DisposalReportList> SEARCHOBJECT
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

        private string asset_id = "";
        public string ASSET_ID
        {
            get { return asset_id; }
            set
            {
                asset_id = value;
                NotifyPropertyChanged("ASSET_ID");
            }
        }

        private string asset_name = "";
        public string ASSET_NAME
        {
            get { return asset_name; }
            set
            {
                asset_name = value;
                NotifyPropertyChanged("ASSET_NAME");
            }
        }

        private string location = "";
        public string LOCATION
        {
            get { return location; }
            set
            {
                location = value;
                NotifyPropertyChanged("LOCATION");
            }
        }

        private string branch = "";
        public string BRANCH
        {
            get { return branch; }
            set
            {
                branch = value;
                NotifyPropertyChanged("BRANCH");
            }
        }

        private string reason = "";
        public string REASONFOR_DISPOSE
        {
            get { return reason; }
            set
            {
                reason = value;
                NotifyPropertyChanged("REASONFOR_DISPOSE");
            }
        }

        private string mode = "";
        public string MODEOF_DISPOSAL
        {
            get { return mode; }
            set
            {
                mode = value;
                NotifyPropertyChanged("MODEOF_DISPOSAL");
            }
        }

        private string authorise = "";
        public string AUTHORIZED_BY
        {
            get { return authorise; }
            set
            {
                authorise = value;
                NotifyPropertyChanged("AUTHORIZED_BY");
            }
        }

        private string date = "";
        public string ASSETDISPOSE_DATE
        {
            get { return date; }
            set
            {
                date = value;
                NotifyPropertyChanged("ASSETDISPOSE_DATE");
            }
        }

        private string value = "";
        public string RESIDUAL_VALUE
        {
            get { return value; }
            set
            {
                value = value;
                NotifyPropertyChanged("RESIDUAL_VALUE");
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
            int userrole = Preferences.Get(Pref.User_Role, 2);
            try
            {
                IsBusy = true;
                IsEnable = true;
                IsVisible = true;

                /* string ctype = "Unloading";*/
                // string ctype = Preferences.Get(ProjectConstants.CTYPE, "");


                var client = new System.Net.Http.HttpClient();
                //  client.BaseAddress = new Uri("http://114.143.156.30/");
                client.BaseAddress = new Uri(ProjectConstants.GETDISPOSEDDATA_API);



                var response = await client.GetAsync("GetDisposedData?Branch=" + branch_id + "&userrole=" + userrole);
                var responseJson = response.Content.ReadAsStringAsync().Result;

                DisposeReportResponse stocktake = new DisposeReportResponse();

                List<DisposalReportList> mystocklist = new List<DisposalReportList>();
                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    stocktake = JsonConvert.DeserializeObject<DisposeReportResponse>(responseJson);
                    if (stocktake.Status.Equals("true"))
                    {
                        ObjStockList = stocktake.DisposalReportList;
                        SEARCHOBJECT = stocktake.DisposalReportList;
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
            List<DisposalReportList> dkt = new List<DisposalReportList>();
            if (string.IsNullOrEmpty(ASSETID))
            {
                // await App.Current.MainPage..DisplayAlert("Alert", "Please enter docket number first.", "Ok");
                await App.Current.MainPage.DisplayAlert("Alert", "Please Scan Asset Id first.", "Ok");
            }
            else
            {
                foreach (DisposalReportList docketforpayment in SEARCHOBJECT)
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
    }
}
