using AssetManagement.Constants;
using AssetManagement.Model;
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

namespace AssetManagement.ViewModel
{
    public class CreateAMCViewModel:BaseViewModel
    {
        public CreateAMCViewModel()
        {
            IsBusy = false;
            IsEnable = false;
            IsVisible = false;

          
            List<string> amctypelist = new List<string>(new string[] { "Select AMC Types", "Comprehensive", "Non-Comprehensive"});
            
            AMCTYPELIST = amctypelist;
            GetCat_Sub_Dept_Vendor();

            DateTime dt = DateTime.Now.Date;
           START_DATE= dt;
            END_DATE= dt;
           ALERT_DATE= dt;
        }


        private int _selectedvendor_index = 0;
        public int SELECTEDVENDOR_INDEX
        {
            get { return _selectedvendor_index; }
            set
            {
                _selectedvendor_index = value;
                NotifyPropertyChanged("SELECTEDVENDOR_INDEX");
            }
        }

        private string Image1_base64 = "";
        public string IMAGE1_BASE64
        {
            get { return Image1_base64; }
            set
            {
                Image1_base64 = value;
                NotifyPropertyChanged("IMAGE1_BASE64");
            }
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

        List<String> _VendorList = new List<string>();

        public List<String> VendorList
        {
            get { return _VendorList; }

            set
            {
                if (_VendorList != value)
                {
                    _VendorList = value;
                    // OnPropertyChanged("ObjPaymentList");
                    NotifyPropertyChanged("VendorList");
                }
            }
        }

        List<String> _asset_amctype;

        public List<String> AMCTYPELIST
        {
            get { return _asset_amctype; }

            set
            {
                if (_asset_amctype != value)
                {
                    _asset_amctype = value;
                    // OnPropertyChanged("ObjPaymentList");
                    NotifyPropertyChanged("AMCTYPELIST");
                }
            }
        }
        private bool _BTNSUBMITSTATUS = false;
        public bool BTNSUBMITSTATUS
        {
            get { return _BTNSUBMITSTATUS; }
            set
            {
                _BTNSUBMITSTATUS = value;
                NotifyPropertyChanged("BTNSUBMITSTATUS");
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
        private bool isVisible = false;
        public bool IsVisible
        {
            get { return isVisible; }
            set
            {
                isVisible = value;
                NotifyPropertyChanged("IsVisible");
            }
        }

        private string _ASSETID;
        public string ASSETID
        {
            get { return _ASSETID; }
            set
            {
                _ASSETID = value;
                NotifyPropertyChanged("ASSETID");
            }
        }



        private string _Asset_name;
        public string Asset_name
        {
            get { return _Asset_name; }
            set
            {
                _Asset_name = value;
                NotifyPropertyChanged("Asset_name");
            }
        }

        private string _amctype;
        public string AMCTYPE
        {
            get { return _amctype; }
            set
            {
                _amctype = value;
                NotifyPropertyChanged("AMCTYPE");
            }
        }

        private int _amctype_index=0;
        public int AMCTYPE_INDEX
        {
            get { return _amctype_index; }
            set
            {
                _amctype_index = value;
                NotifyPropertyChanged("AMCTYPE_INDEX");
            }
        }


        private string _Vendor;
        public string Vendor
        {
            get { return _Vendor; }
            set
            {
                _Vendor = value;
                NotifyPropertyChanged("Vendor");
            }
        }

        private string _amcValue;
        public string AMCVALUE
        {
            get { return _amcValue; }
            set
            {
                _amcValue = value;
                NotifyPropertyChanged("AMCVALUE");
            }
        }

        private DateTime _start_date;
        public DateTime START_DATE
        {
            get { return _start_date; }
            set
            {
                _start_date = value;
                NotifyPropertyChanged("START_DATE");
            }
        }
        private DateTime _end_date;
        public DateTime END_DATE
        {
            get { return _end_date; }
            set
            {
                _end_date = value;
                NotifyPropertyChanged("END_DATE");
            }
        }

        private DateTime _alert_date;
        public DateTime ALERT_DATE
        {
            get { return _alert_date; }
            set
            {
                _alert_date = value;
                NotifyPropertyChanged("ALERT_DATE");
            }
        }

        private string _description;
        public string DESCRIPTION
        {
            get { return _description; }
            set
            {
                _description = value;
                NotifyPropertyChanged("DESCRIPTION");
            }
        }


        private async Task GetCat_Sub_Dept_Vendor()
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

                          
                            VendorList = apiresponse.VendorList;
                           
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

                    //BranchList = null;
                    //await App.Current.MainPage.DisplayAlert("Exception", "Request could n, please try again later", "Ok");
                   //Crashes.TrackError(excp);

                }

            }
            else
            {

                await App.Current.MainPage.DisplayAlert("Alert", "No internet connection, please check and try again later.", "OK");
            }
        }

        public Command SUBMIT
        {
            get
            {
                return new Command(Submit);
            }
        }

        private async void Submit(object obj)
        {
            try
            {
                IsBusy = true;
                IsEnable = true;
                IsVisible = true;
                BTNSUBMITSTATUS = false;

                //.....................................................
                string company_id = Preferences.Get(Pref.Company_Id, "");
                string branch_id = Preferences.Get(Pref.BRANCH, "");

                AMCList AMC = new AMCList();
               
                AMC.Company_Id = company_id;
                AMC.Branch = branch_id;
                AMC.Asset_id = ASSETID;
                AMC.Asset_Name = Asset_name;
                AMC.Vendor_Name = Vendor;
                AMC.AMC_Type = AMCTYPE;
                AMC.AMC_StartDate = START_DATE;
                AMC.AMC_EndDate = END_DATE;
                AMC.AMC_AlertDate = ALERT_DATE;
                AMC.AMC_Description = DESCRIPTION;
                AMC.AMC_Value = AMCVALUE;
                AMC.Image1 = IMAGE1_BASE64;

                //...........................
                var client = new System.Net.Http.HttpClient();
                // client.BaseAddress = new Uri("http://114.143.156.30/");
                client.BaseAddress = new Uri(ProjectConstants.BASE_URL);
                var json = JsonConvert.SerializeObject(AMC);
                if (CrossConnectivity.Current.IsConnected)  // checking internet connection
                {

                    StringContent str = new StringContent(json, Encoding.UTF8, "application/json");
                    var response = await client.PostAsync(new Uri(ProjectConstants.CREATEAMC_API), str);
                    var responseJson = response.Content.ReadAsStringAsync().Result;
                    PostResponseForAll stocktakeresponse = new PostResponseForAll();

                    if (response.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        stocktakeresponse = JsonConvert.DeserializeObject<PostResponseForAll>(responseJson);

                        if (stocktakeresponse.Status.Equals("true"))
                        {
                            await App.Current.MainPage.DisplayAlert("Alert", "AMC created successfully.", "Ok");

                           await CommonClass.GetMove_AMCNotification();
                        }
                        else
                        {
                            // DependencyService.Get<IAudio>().PlayAudioFile(ProjectConstants.audio_alert_fail);
                            await App.Current.MainPage.DisplayAlert("Alert", stocktakeresponse.Msg, "Ok");
                        }
                    }
                    else
                    {
                        // DependencyService.Get<IAudio>().PlayAudioFile(ProjectConstants.audio_alert_fail);
                        await App.Current.MainPage.DisplayAlert("Alert", response.ReasonPhrase, "Ok");
                        // await App.Current.MainPage.DisplayAlert("Alert", pusaddresponse.Msg.ToString(), "Ok");
                    }
                }
                else
                {
                    // DependencyService.Get<IAudio>().PlayAudioFile(ProjectConstants.audio_alert_fail);
                    await App.Current.MainPage.DisplayAlert("Alert", "No internet connection, Please try again after connecting device to internet.", "OK");
                    // Preferences.Set(ProjectConstants.PRELOADING_OFFLINEDATA, json);
                    // Deleting Data--- un comment late

                    // ObjDocketList = database.GetPreLoading_UnloadingData();
                }
                IsBusy = false;
                IsEnable = false;
                IsVisible = false;
                BTNSUBMITSTATUS = true;

            }
            catch (Exception excp)
            {

                IsBusy = false;
                IsEnable = false;
                IsVisible = false;
                BTNSUBMITSTATUS = true;
                await App.Current.MainPage.DisplayAlert("Alert", excp.Message, "Ok");
               

            }
            IsBusy = false;
            IsEnable = false;
            IsVisible = false;
            BTNSUBMITSTATUS = true;
        }
        public async void GetAssetsInfo()
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
            string assetid = ASSETID;
            try
            {
                IsBusy = true;
                IsEnable = true;
                IsVisible = true;

                /* string ctype = "Unloading";*/
                // string ctype = Preferences.Get(ProjectConstants.CTYPE, "");


                var client = new System.Net.Http.HttpClient();
                //  client.BaseAddress = new Uri("http://114.143.156.30/");
                client.BaseAddress = new Uri(ProjectConstants.GETASSETINFO_API);



                var response = await client.GetAsync("GetAssets?assetid=" + assetid + "&branch=" + branch_id);
                
                var responseJson = response.Content.ReadAsStringAsync().Result;

                StockTallyResponse stocktake = new StockTallyResponse();

                List<STockTallyDetails> mystocklist = new List<STockTallyDetails>();
                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    stocktake = JsonConvert.DeserializeObject<StockTallyResponse>(responseJson);
                    if (stocktake.Status.Equals("true"))
                    {
                        Asset_name = stocktake.STockTallyDetails[0].Asset_name;
                       
                        // int totalArticle = ObjStockList.Count;

                        // TOTAL_STOCK = totalArticle;


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
                // Common.SaveLogs(excp.StackTrace);
                IsBusy = false;
                IsEnable = false;
                IsVisible = false;

                // ObjStockList = database.GetStockList(branch_id);
                //await App.Current.MainPage.DisplayAlert("Exception", "Request could n, please try again later", "Ok");

               // Crashes.TrackError(excp);
            }
        }
    }
}
