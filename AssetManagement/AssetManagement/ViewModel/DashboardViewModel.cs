using Android.Content.Res;
using AssetManagement.Constants;
using AssetManagement.Model;
using AssetManagement.View;
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

namespace AssetManagement.ViewModel
{
    public class DashboardViewModel:BaseViewModel
    {
       // public List<Person> Data { get; set; }
       // public List<GetFountMissingCount> Data { get; set; }
        public DashboardViewModel()
        {
            /* Data = new List<GetFountMissingCount>()
               {
                   new GetFountMissingCount { FoundMissing = "Found", count = 180 },
                   new GetFountMissingCount { FoundMissing = "Missing", count = 17 }

               };*/
            GetBranches();

        }
        int _currentBranch = 0;

        public int CurrentBranch
        {
            get { return _currentBranch; }

            set
            {
                if (_currentBranch != value)
                {
                    _currentBranch = value;
                    // OnPropertyChanged("ObjPaymentList");
                    NotifyPropertyChanged("CurrentBranch");
                }
            }
        }

        //..................
        int _assetRepair=0;

        public int AssetRepair
        {
            get { return _assetRepair; }

            set
            {
                if (_assetRepair != value)
                {
                    _assetRepair = value;
                    // OnPropertyChanged("ObjPaymentList");
                    NotifyPropertyChanged("AssetRepair");
                }
            }
        }
        //....................

        //..................
        int _assetAMC = 0;

        public int AssetAMC
        {
            get { return _assetAMC; }

            set
            {
                if (_assetAMC != value)
                {
                    _assetAMC = value;
                    // OnPropertyChanged("ObjPaymentList");
                    NotifyPropertyChanged("AssetAMC");
                }
            }
        }
        //....................

        int _TotalAssets = 0;

        public int TotalAssets
        {
            get { return _TotalAssets; }

            set
            {
                if (_TotalAssets != value)
                {
                    _TotalAssets = value;
                    // OnPropertyChanged("ObjPaymentList");
                    NotifyPropertyChanged("TotalAssets");
                }
            }
        }

        
        int _ASSETMOVE = 0;

        public int ASSETMOVE
        {
            get { return _ASSETMOVE; }

            set
            {
                if (_ASSETMOVE != value)
                {
                    _ASSETMOVE = value;
                    // OnPropertyChanged("ObjPaymentList");
                    NotifyPropertyChanged("ASSETMOVE");
                }
            }
        }
        //..............................................
        int _ASSETDISPOSED = 0;

        public int ASSETDISPOSED
        {
            get { return _ASSETDISPOSED; }

            set
            {
                if (_ASSETDISPOSED != value)
                {
                    _ASSETDISPOSED = value;
                    // OnPropertyChanged("ObjPaymentList");
                    NotifyPropertyChanged("ASSETDISPOSED");
                }
            }
        }



        public async Task GetBranches()
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


                    var client = new RestClient(ProjectConstants.GETBRANCHES_API);
                    var request = new RestRequest(Method.GET);
                    // request.AddHeader("postman-token", "e3fa53b1-0f94-c75d-d04a-e97018565406");
                    request.AddHeader("cache-control", "no-cache");
                    request.AddHeader("content-type", "application/x-www-form-urlencoded");
                    //  request.AddParameter("application/x-www-form-urlencoded", "Truck_No=GJ01DX8008&CType=Loading&Branch_Id=130", ParameterType.RequestBody);
                    IRestResponse response = client.Execute(request);

                    // Extracting output data from received response
                    string strresponse = response.Content;

                    BranchMasterResp stocktake = new BranchMasterResp();

                    // List<BranchWiseAssets> mystocklist = new List<BranchWiseAssets>();
                    if (response.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        stocktake = JsonConvert.DeserializeObject<BranchMasterResp>(strresponse);
                        if (stocktake.Status.Equals("true"))
                        {

                            BranchList = stocktake.Branches;
                           

                        }
                        else
                        {
                            //DependencyService.Get<IAudio>().PlayAudioFile(ProjectConstants.audio_alert_fail);
                            // await App.Current.MainPage.DisplayAlert("Exception", stocktake.Msg.ToString(), "Ok");
                            //BranchwiseList = stocktake.BranchWiseAssets;
                        }

                    }
                    else
                    {
                        // DependencyService.Get<IAudio>().PlayAudioFile(ProjectConstants.audio_alert_fail);
                        // await App.Current.MainPage.DisplayAlert("Exception", response.ReasonPhrase, "Ok");
                        //BranchwiseList = stocktake.BranchWiseAssets;
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

                    BranchList = null;
                    //await App.Current.MainPage.DisplayAlert("Exception", "Request could n, please try again later", "Ok");
                    Crashes.TrackError(excp);

                }

            }
            else
            {

                await App.Current.MainPage.DisplayAlert("Alert", "No internet connection, please check and try again later.", "OK");
            }
        }

        public async Task GetAMCDetails(string branch_id)
        {
            if (CrossConnectivity.Current.IsConnected)
            {

                //string branch_id = Preferences.Get(Pref.BRANCH, "");
                try
                {
                    IsBusy = true;
                    IsEnable = true;
                    IsVisible = true;

                    /* string ctype = "Unloading";*/
                    // string ctype = Preferences.Get(ProjectConstants.CTYPE, "");


                    var client = new System.Net.Http.HttpClient();
                    //  client.BaseAddress = new Uri("http://114.143.156.30/");
                    client.BaseAddress = new Uri(ProjectConstants.GETAMCDETAILS_API);



                    var response = await client.GetAsync("GetAMCRepair?Branch=" + branch_id);
                    var responseJson = response.Content.ReadAsStringAsync().Result;

                    GetAMC_RepairCount stocktake = new GetAMC_RepairCount();
                   // GetFountMissingCountOnBranch stocktake = new GetFountMissingCountOnBranch();

                    // List<BranchWiseAssets> mystocklist = new List<BranchWiseAssets>();
                    if (response.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        stocktake = JsonConvert.DeserializeObject<GetAMC_RepairCount>(responseJson);
                        if (stocktake.Status.Equals("true"))
                        {
                            


                            ObjStockList = stocktake.GetAssetTypeCount;



                        }
                        else
                        {
                            //DependencyService.Get<IAudio>().PlayAudioFile(ProjectConstants.audio_alert_fail);
                           // await App.Current.MainPage.DisplayAlert("Exception", stocktake.Msg.ToString(), "Ok");
                            //BranchwiseList = stocktake.BranchWiseAssets;
                        }

                    }
                    else
                    {
                        // DependencyService.Get<IAudio>().PlayAudioFile(ProjectConstants.audio_alert_fail);
                       // await App.Current.MainPage.DisplayAlert("Exception", response.ReasonPhrase, "Ok");
                        //BranchwiseList = stocktake.BranchWiseAssets;
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

                    ObjStockList = null;
                    //await App.Current.MainPage.DisplayAlert("Exception", "Request could n, please try again later", "Ok");
                    Crashes.TrackError(excp);

                }

            }
            else
            {

                await App.Current.MainPage.DisplayAlert("Alert", "No internet connection, please check and try again later.", "OK");
            }
        }
        public async Task GetStockDetailsBranchWise()
        {
            if (CrossConnectivity.Current.IsConnected)
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
                    client.BaseAddress = new Uri(ProjectConstants.GETBRANCHWISEASSETCOUNT_API);



                    var response = await client.GetAsync("GetBranchWiseAssetCount");
                    var responseJson = response.Content.ReadAsStringAsync().Result;

                    BranchWiseAssetCount stocktake = new BranchWiseAssetCount();

                    List<BranchWiseAssets> mystocklist = new List<BranchWiseAssets>();
                    if (response.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        stocktake = JsonConvert.DeserializeObject<BranchWiseAssetCount>(responseJson);
                        if (stocktake.Status.Equals("true"))
                        {


                            BranchwiseList = stocktake.BranchWiseAssets;



                        }
                        else
                        {
                            //DependencyService.Get<IAudio>().PlayAudioFile(ProjectConstants.audio_alert_fail);
                           // await App.Current.MainPage.DisplayAlert("Exception", stocktake.Msg.ToString(), "Ok");
                            BranchwiseList = stocktake.BranchWiseAssets;
                        }

                    }
                    else
                    {
                        // DependencyService.Get<IAudio>().PlayAudioFile(ProjectConstants.audio_alert_fail);
                       // await App.Current.MainPage.DisplayAlert("Exception", response.ReasonPhrase, "Ok");
                        BranchwiseList = stocktake.BranchWiseAssets;
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

                    ObjStockList = null;
                    //await App.Current.MainPage.DisplayAlert("Exception", "Request could n, please try again later", "Ok");

                    Crashes.TrackError(excp);
                }

            }
            else
            {

                await App.Current.MainPage.DisplayAlert("Alert", "No internet connection, please check and try again later.", "OK");
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

        //..................
        List<GetAssetTypeCount> _objstockList;

        public List<GetAssetTypeCount> ObjStockList
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
        //....................
        string _branch;

        public string BRANCH
        {
            get { return _branch; }

            set
            {
                if (_branch != value)
                {
                    _branch = value;
                    // OnPropertyChanged("ObjPaymentList");
                    NotifyPropertyChanged("BRANCH");
                }
            }
        }
        //..................
        List<BranchWiseAssets> _branchwiseList;

        public List<BranchWiseAssets> BranchwiseList
        {
            get { return _branchwiseList; }

            set
            {
                if (_branchwiseList != value)
                {
                    _branchwiseList = value;
                    // OnPropertyChanged("ObjPaymentList");
                    NotifyPropertyChanged("BranchwiseList");
                }
            }
        }
        //....................

        //..................
        List<String> _branchList;

        public List<String> BranchList
        {
            get { return _branchList; }

            set
            {
                if (_branchList != value)
                {
                    _branchList = value;
                    // OnPropertyChanged("ObjPaymentList");
                    NotifyPropertyChanged("BranchList");
                }
            }
        }


        //....................
       
    }
}
