using AssetManagement.Constants;
using AssetManagement.Model;
using Microsoft.AppCenter.Crashes;
using Newtonsoft.Json;
using Plugin.Connectivity;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using static Android.Graphics.Paint;

namespace AssetManagement.ViewModel
{
    public class AssetReportViewModel:BaseViewModel
    {
        List<string> sub = new List<string>(new string[] { "No Subcategory" });

        public AssetReportViewModel()
        {
            IsBusy = false;
            IsEnable = false;
            IsVisible = false;
            GetCat_Sub_Dept_Vendor();

        }

        private int _selectedsubcategory_index = 0;
        public int SELECTEDSUBCATEGORY_INDEX
        {
            get { return _selectedsubcategory_index; }
            set
            {
                _selectedsubcategory_index = value;
                NotifyPropertyChanged("SELECTEDSUBCATEGORY_INDEX");
            }
        }

        private string _SubCategory;
        public string SubCategory
        {
            get { return _SubCategory; }
            set
            {
                _SubCategory = value;
                NotifyPropertyChanged("SubCategory");
            }
        }

        List<String> _SubCategoryList = null;

        public List<String> SubCategoryList
        {
            get { return _SubCategoryList; }

            set
            {
                if (_SubCategoryList != value)
                {
                    _SubCategoryList = value;
                    // OnPropertyChanged("ObjPaymentList");
                    NotifyPropertyChanged("SubCategoryList");
                }
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

        List<STockTallyDetails> _objstockList;

        public List<STockTallyDetails> ObjStockList
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

        List<CreateAssetRequest> _AssetList;

        public List<CreateAssetRequest> AssetList
        {
            get { return _AssetList; }

            set
            {
                if (_AssetList != value)
                {
                    _AssetList = value;
                    // OnPropertyChanged("ObjPaymentList");
                    NotifyPropertyChanged("AssetList");
                }
            }
        }

        List<STockTallyDetails> _searchObject;

        public List<STockTallyDetails> SEARCHOBJECT
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
                client.BaseAddress = new Uri(ProjectConstants.GETSTOCKTALLYDATA_API);



                var response = await client.GetAsync("GetStockData?Branch=" + branch_id);
                var responseJson = response.Content.ReadAsStringAsync().Result;

                StockTallyResponse stocktake = new StockTallyResponse();

                List<STockTallyDetails> mystocklist = new List<STockTallyDetails>();
                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    stocktake = JsonConvert.DeserializeObject<StockTallyResponse>(responseJson);
                    if (stocktake.Status.Equals("true"))
                    {
                        ObjStockList = stocktake.STockTallyDetails;
                       SEARCHOBJECT= stocktake.STockTallyDetails;
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
            List<STockTallyDetails> dkt = new List<STockTallyDetails>();
            if (string.IsNullOrEmpty(ASSETID))
            {
                // await App.Current.MainPage..DisplayAlert("Alert", "Please enter docket number first.", "Ok");
                await App.Current.MainPage.DisplayAlert("Alert", "Please Scan Asset Id first.", "Ok");
            }
            else
            {
                foreach (STockTallyDetails docketforpayment in SEARCHOBJECT)
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

        public async void SearchAsset_bySubCat()
        {
            bool status = false;
            List<STockTallyDetails> dkt = new List<STockTallyDetails>();
           /* if (string.IsNullOrEmpty(ASSETID))
            {
                // await App.Current.MainPage..DisplayAlert("Alert", "Please enter docket number first.", "Ok");
                await App.Current.MainPage.DisplayAlert("Alert", "Please Scan Asset Id first.", "Ok");
            }
            else
            {*/
                foreach (STockTallyDetails docketforpayment in SEARCHOBJECT)
                {
                    if (SubCategory.Equals(docketforpayment.SubCategory))
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

           // }
        }

        public async Task GetAssetsData()
        {

            if (CrossConnectivity.Current.IsConnected)
            {
                
                string branch_id = Preferences.Get(Pref.BRANCH, "");
                try
                {
                    IsBusy = true;
                    IsEnable = true;
                    IsVisible = true;
                    var client = new System.Net.Http.HttpClient();
                    //  client.BaseAddress = new Uri("http://114.143.156.30/");
                    client.BaseAddress = new Uri(ProjectConstants.GETASSETINFO_API);



                    var response = await client.GetAsync("GetAssets?branch=" + branch_id);

                    var responseJson = response.Content.ReadAsStringAsync().Result;

                    AssetDataResponse stocktake = new AssetDataResponse();

                    if (response.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        stocktake = JsonConvert.DeserializeObject<AssetDataResponse>(responseJson);
                        if (stocktake.Status.Equals("true"))
                        {
                            AssetList = stocktake.Assets;
                        }
                        else
                        {
                            //DependencyService.Get<IAudio>().PlayAudioFile(ProjectConstants.audio_alert_fail);
                            await App.Current.MainPage.DisplayAlert("Exception", stocktake.Msg.ToString(), "Ok");
                            // ObjStockList = null;
                        }

                    }
                    else
                    {
                        // DependencyService.Get<IAudio>().PlayAudioFile(ProjectConstants.audio_alert_fail);
                        await App.Current.MainPage.DisplayAlert("Exception", response.ReasonPhrase, "Ok");
                        // ObjStockList = null;
                    }
                    IsBusy = false;
                    IsEnable = false;
                    IsVisible = false;

                }
                catch (Exception excp)
                {
                    IsBusy = false;
                    IsEnable = false;
                    IsVisible = false;
                }
            }
            else
            {
                await App.Current.MainPage.DisplayAlert("Alert", "No internet connection, please check and try again later.", "OK");
            }
        }

        public async Task GetCat_Sub_Dept_Vendor()
        {
            if (CrossConnectivity.Current.IsConnected)
            {


                try
                {
                    IsBusy = true;
                    IsEnable = true;
                    IsVisible = true;

                    /* string ctype = "Unloading";*/
                    // string ctype = Preferences.Get(ProjectConstants.CTYPE, "");


                    var client = new RestClient(ProjectConstants.GETCAT_SUB_DEPT_VENDOR_API);
                    var request = new RestRequest(Method.GET);
                    // request.AddHeader("postman-token", "e3fa53b1-0f94-c75d-d04a-e97018565406");
                    request.AddHeader("cache-control", "no-cache");
                    request.AddHeader("content-type", "application/x-www-form-urlencoded");
                    //  request.AddParameter("application/x-www-form-urlencoded", "Truck_No=GJ01DX8008&CType=Loading&Branch_Id=130", ParameterType.RequestBody);
                    IRestResponse response = client.Execute(request);
                    // Extracting output data from received response
                    string strresponse = response.Content;

                    Cate_Sub_Dept_VendorResp apiresponse = new Cate_Sub_Dept_VendorResp();

                    // List<BranchWiseAssets> mystocklist = new List<BranchWiseAssets>();
                    if (response.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        apiresponse = JsonConvert.DeserializeObject<Cate_Sub_Dept_VendorResp>(strresponse);
                        if (apiresponse.Status.Equals("true"))
                        {

                            SubCategoryList = apiresponse.SubCategoryList ?? sub;
                        
                        }
                        else
                        {

                        }

                    }
                    else
                    {

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

                  //  BranchList = null;
                    //await App.Current.MainPage.DisplayAlert("Exception", "Request could n, please try again later", "Ok");
                    Crashes.TrackError(excp);

                }

            }
            else
            {

                await App.Current.MainPage.DisplayAlert("Alert", "No internet connection, please check and try again later.", "OK");
            }
        }

    }
}
