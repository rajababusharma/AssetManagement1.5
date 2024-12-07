using AssetManagement.Constants;
using AssetManagement.Model;
using Microsoft.AppCenter.Crashes;
using Newtonsoft.Json;
using Plugin.Connectivity;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using static Android.App.Assist.AssistStructure;
using static Android.Graphics.Paint;

namespace AssetManagement.ViewModel
{
    public class AssetReportViewModel:BaseViewModel
    {
        List<string> loc = new List<string>(new string[] { "No Location" });
        List<string> br = new List<string>(new string[] { "No Branch" });
        List<string> emp = new List<string>(new string[] { "No Employee" });
        List<string> cat = new List<string>(new string[] { "No Category" });
        List<string> sub = new List<string>(new string[] { "No Subcategory" });
       // List<string> FilterList = new List<string>(new string[] { "Employee","Location","Branch","Category","Subcategory" });

        public AssetReportViewModel()
        {
            IsBusy = false;
            IsEnable = false;
            IsVisible = false;
              GetCat_Sub_Dept_Vendor();
             GetEmployeeList();
           // GetAssetsData();
        }

        private int _FilteredItem = 0;
        public int FilteredItem
        {
            get { return _FilteredItem; }
            set
            {
                _FilteredItem = value;
                NotifyPropertyChanged("FilteredItem");
            }
        }

        private string _location;
        public string Location
        {
            get { return _location; }
            set
            {
                _location = value;
                NotifyPropertyChanged("Location");
            }
        }

        private string _category;
        public string Category
        {
            get { return _category; }
            set
            {
                _category = value;
                NotifyPropertyChanged("Category");
            }
        }

        private string _subcat;
        public string SubCategory
        {
            get { return _subcat; }
            set
            {
                _subcat = value;
                NotifyPropertyChanged("SubCategory");
            }
        }

        private string _branch;
        public string Branch
        {
            get { return _branch; }
            set
            {
                _branch = value;
                NotifyPropertyChanged("Branch");
            }
        }

        private string _selectedemployee;
        public string Employee
        {
            get { return _selectedemployee; }
            set
            {
                _selectedemployee = value;
                NotifyPropertyChanged("Employee");
            }
        }


        List<String> _EmployeeList = new List<string>(100);

        public List<String> EmployeeList
        {
            get { return _EmployeeList; }

            set
            {
                if (_EmployeeList != value)
                {
                    _EmployeeList = value;
                    // OnPropertyChanged("ObjPaymentList");
                    NotifyPropertyChanged("EmployeeList");
                }
            }
        }

        List<String> _FilterList = new List<string>(new string[] { "Employee", "Location", "Branch", "Category", "Subcategory" });

        public List<String> FilterLists
        {
            get { return _FilterList; }

            set
            {
                if (_FilterList != value)
                {
                    _FilterList = value;
                    // OnPropertyChanged("ObjPaymentList");
                    NotifyPropertyChanged("FilterLists");
                }
            }
        }


        List<String> _branchList = null;

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

        List<String> _LocationList = null;

        public List<String> LocationList
        {
            get { return _LocationList; }

            set
            {
                if (_LocationList != value)
                {
                    _LocationList = value;
                    // OnPropertyChanged("ObjPaymentList");
                    NotifyPropertyChanged("LocationList");
                }
            }
        }

        List<String> _CategoryList = null;

        public List<String> CategoryList
        {
            get { return _CategoryList; }

            set
            {
                if (_CategoryList != value)
                {
                    _CategoryList = value;
                    // OnPropertyChanged("ObjPaymentList");
                    NotifyPropertyChanged("CategoryList");
                }
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

        List<CreateAssetRequest> _searchObject;

        public List<CreateAssetRequest> SEARCHOBJECT
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

      /*  List<STockTallyDetails> _searchObject;

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
        }*/

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

  /*      public async void GetAllAssetsData()
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

                *//* string ctype = "Unloading";*//*
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
        }*/
        public async void SearchAsset()
        {
            bool status = false;
            List<CreateAssetRequest> dkt = new List<CreateAssetRequest>();
            if (string.IsNullOrEmpty(ASSETID))
            {
                // await App.Current.MainPage..DisplayAlert("Alert", "Please enter docket number first.", "Ok");
                await App.Current.MainPage.DisplayAlert("Alert", "Please Scan Asset Id first.", "Ok");
            }
            else
            {
                foreach (CreateAssetRequest docketforpayment in SEARCHOBJECT)
                {
                    if (ASSETID.Trim().Equals(docketforpayment.Asset_id))
                    {
                        dkt.Add(docketforpayment);
                        AssetList = dkt;
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
            List<CreateAssetRequest> dkt = new List<CreateAssetRequest>();
           
              //  foreach (CreateAssetRequest docketforpayment in SEARCHOBJECT)
           // {
               
                    if (FilteredItem==(int)FilterList.Employee)
                    {
                        foreach (CreateAssetRequest docketforpayment in SEARCHOBJECT)
                        {
                            if (Employee.Equals(docketforpayment.Employee))
                            {
                                dkt.Add(docketforpayment);
                            }
                        }
                         AssetList = dkt;
                         status = true;
                    }
                    else if (FilteredItem == (int)FilterList.Location)
                    {
                        foreach (CreateAssetRequest docketforpayment in SEARCHOBJECT)
                        {
                            if (Location.Equals(docketforpayment.Location))
                            {
                                dkt.Add(docketforpayment);
                            }
                        }
                         AssetList = dkt;
                         status = true;
                    }
                    else if (FilteredItem == (int)FilterList.Branch)
                    {
                        foreach (CreateAssetRequest docketforpayment in SEARCHOBJECT)
                        {
                            if (Branch.Equals(docketforpayment.Branch))
                            {
                                dkt.Add(docketforpayment);
                            }
                        }
                         AssetList = dkt;
                         status = true;
                    }
                    else if (FilteredItem == (int)FilterList.Category)
                    {
                        foreach (CreateAssetRequest docketforpayment in SEARCHOBJECT)
                        {
                            if (Category.Equals(docketforpayment.Category))
                            {
                                dkt.Add(docketforpayment);
                            }
                        }
                         AssetList = dkt;
                         status = true;
                    }
                    else if (FilteredItem == (int)FilterList.Subcategory)
                    {
                        foreach (CreateAssetRequest docketforpayment in SEARCHOBJECT)
                        {
                            if (SubCategory.Equals(docketforpayment.SubCategory))
                            {
                                dkt.Add(docketforpayment);
                            }
                        }
                         AssetList = dkt;
                         status = true;
                    }
           // }
                if (!status)
                {
                    AssetList = null;
                    await App.Current.MainPage.DisplayAlert("Alert", "Asset not found", "Ok");
                }

           // }
        }

        public async Task GetAssetsData()
        {

            if (CrossConnectivity.Current.IsConnected)
            {
                int userrole = Preferences.Get(Pref.User_Role, 0);
                string branch_id = Preferences.Get(Pref.BRANCH, "");
                try
                {
                    IsBusy = true;
                    IsEnable = true;
                    IsVisible = true;
                    var client = new System.Net.Http.HttpClient();
                    //  client.BaseAddress = new Uri("http://114.143.156.30/");
                    client.BaseAddress = new Uri(ProjectConstants.GETASSETINFO_API);



                   // var response = await client.GetAsync("GetAssets?branch=" + branch_id);
                    var response = await client.GetAsync("GetAssets?branch=" + branch_id + "&UserRole="+ userrole);

                    var responseJson = response.Content.ReadAsStringAsync().Result;

                    AssetDataResponse stocktake = new AssetDataResponse();

                    if (response.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        stocktake = JsonConvert.DeserializeObject<AssetDataResponse>(responseJson);
                        if (stocktake.Status.Equals("true"))
                        {
                          //  AssetList=null;
                           // SEARCHOBJECT=null;
                            AssetList = stocktake.Assets;
                            SEARCHOBJECT = stocktake.Assets;
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

                            CategoryList = apiresponse.CategoryList ?? cat;
                            SubCategoryList = apiresponse.SubCategoryList ?? sub;
                            LocationList = apiresponse.LocationList ?? loc;
                            BranchList = apiresponse.BranchList ?? br;

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

        public async Task GetEmployeeList()
        {
            if (CrossConnectivity.Current.IsConnected)
            {


                try
                {
                    IsBusy = true;
                    IsEnable = true;
                    IsVisible = true;

                    var client = new System.Net.Http.HttpClient();
                    client.BaseAddress = new Uri(ProjectConstants.GETEMPLOYEELIST_API);
                    try
                    {

                        string branch = Preferences.Get(Pref.BRANCH, "");
                        int userrole = Preferences.Get(Pref.User_Role, 2);

                        // var response = await client.GetAsync("GetEmployeeList?Branch=" + branch);
                        var response = await client.GetAsync("GetEmployeeList?Branch=" + branch + "&userrole=" + userrole);
                        var docketJson = response.Content.ReadAsStringAsync().Result;


                      

                        EmployeeMasterResp docketobject = new EmployeeMasterResp();
                        if (response.StatusCode == System.Net.HttpStatusCode.OK)
                        {
                            if (docketJson != "")
                            {
                                docketobject = JsonConvert.DeserializeObject<EmployeeMasterResp>(docketJson);
                                if (docketobject.Status.Equals("true"))
                                {
                                    // dockets= JsonConvert.DeserializeObject<DeliveryDoket>(docketJson);

                                    EmployeeList = docketobject.Employee ?? emp;

                                }
                                else
                                {
                                    await App.Current.MainPage.DisplayAlert("Exception", docketobject.Msg, "Ok");
                                }

                            }
                        }
                        else
                        {
                            await App.Current.MainPage.DisplayAlert("Exception", response.ReasonPhrase, "Ok");
                        }
                        IsBusy = false;
                        IsEnable = false;
                        IsVisible = false;

                    }
                    catch (Exception excp)
                    {
                        await App.Current.MainPage.DisplayAlert("Exception", excp.Message, "Ok");
                        Crashes.TrackError(excp);
                        IsBusy = false;
                        IsEnable = false;
                        IsVisible = false;
                    }

                }
                catch (Exception excp)
                {
                    // Common.SaveLogs(excp.StackTrace);
                    IsBusy = false;
                    IsEnable = false;
                    IsVisible = false;

                    // BranchList = null;
                    //await App.Current.MainPage.DisplayAlert("Exception", "Request could n, please try again later", "Ok");

                    Crashes.TrackError(excp);
                }

            }
            else
            {

                await App.Current.MainPage.DisplayAlert("Alert", "No internet connection, please check and try again later.", "OK");
            }
        }

        public async Task GetBranches(string location)
        {
            if (CrossConnectivity.Current.IsConnected)
            {

                // string location = Preferences.Get(Pref.LOCATION, "");
                int userrole = Preferences.Get(Pref.User_Role, 0);
                try
                {
                    IsBusy = true;
                    IsEnable = true;
                    IsVisible = true;

                    /* string ctype = "Unloading";*/
                    // string ctype = Preferences.Get(ProjectConstants.CTYPE, "");

                    var client = new System.Net.Http.HttpClient();
                    //  client.BaseAddress = new Uri("http://114.143.156.30/");
                    client.BaseAddress = new Uri(ProjectConstants.GETBRANCHES_API1);



                    var response = await client.GetAsync("Get?Location=" + location + "&userrole=" + userrole);
                    var responseJson = response.Content.ReadAsStringAsync().Result;



                    /* var client = new RestClient(ProjectConstants.GETBRANCHES_API);
                     var request = new RestRequest(Method.GET);
                     // request.AddHeader("postman-token", "e3fa53b1-0f94-c75d-d04a-e97018565406");
                     request.AddHeader("cache-control", "no-cache");
                     request.AddHeader("content-type", "application/x-www-form-urlencoded");
                     //  request.AddParameter("application/x-www-form-urlencoded", "Truck_No=GJ01DX8008&CType=Loading&Branch_Id=130", ParameterType.RequestBody);
                     IRestResponse response = client.Execute(request);*/

                    // Extracting output data from received response
                    // string strresponse = responseJson.Content;

                    BranchMasterResp stocktake = new BranchMasterResp();

                    // List<BranchWiseAssets> mystocklist = new List<BranchWiseAssets>();
                    if (response.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        stocktake = JsonConvert.DeserializeObject<BranchMasterResp>(responseJson);
                        if (stocktake.Status.Equals("true"))
                        {

                            BranchList = stocktake.Branches ?? br;

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

        public async Task GetSubCategory(string category)
        {
            if (CrossConnectivity.Current.IsConnected)
            {

                // string location = Preferences.Get(Pref.LOCATION, "");
                int userrole = Preferences.Get(Pref.User_Role, 0);
                try
                {
                    IsBusy = true;
                    IsEnable = true;
                    IsVisible = true;

                    /* string ctype = "Unloading";*/
                    // string ctype = Preferences.Get(ProjectConstants.CTYPE, "");

                    var client = new System.Net.Http.HttpClient();
                    //  client.BaseAddress = new Uri("http://114.143.156.30/");
                    client.BaseAddress = new Uri(ProjectConstants.GETSUBCATEGORYCOUNT_API);



                    var response = await client.GetAsync("GetSub_Category?Category=" + category);
                    var responseJson = response.Content.ReadAsStringAsync().Result;

                    Sub_CategoryResp stocktake = new Sub_CategoryResp();

                    // List<BranchWiseAssets> mystocklist = new List<BranchWiseAssets>();
                    if (response.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        stocktake = JsonConvert.DeserializeObject<Sub_CategoryResp>(responseJson);
                        if (stocktake.Status.Equals("true"))
                        {

                            SubCategoryList = stocktake.SubCategoryList ?? sub;

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

                    SubCategoryList = null;
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
