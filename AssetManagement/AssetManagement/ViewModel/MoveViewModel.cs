using AssetManagement.Constants;
using AssetManagement.Model;
using Microsoft.AppCenter.Crashes;
using Newtonsoft.Json;
using Plugin.Connectivity;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using static AssetManagement.View.MasterDetailPage1Master;

namespace AssetManagement.ViewModel
{
    public class MoveViewModel : BaseViewModel
    {
        STockTallyDetails _details;
        public MoveViewModel(STockTallyDetails details)
        {
            _details = new STockTallyDetails();
            _details = details;
            ASSETID = details.Asset_id;
            ASSETNAME = details.Asset_name;
            FROMLOCATION = details.Location;
            FROMBRANCH = details.Branch;
           // FROMEMPLOYEE = details.Employee;
           

            GetLocations();
            GetBranches();
           // GetVendorList();
           // GetEmployeeList();
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


        private bool btnSubmitStatus = true;
        public bool BTNSUBMITSTATUS
        {
            get { return btnSubmitStatus; }
            set
            {
                btnSubmitStatus = value;
                NotifyPropertyChanged("BTNSUBMITSTATUS");
            }
        }

      

        List<String> _branchList=new List<string>();

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

        List<String> _LocationList=new List<string>();

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
        private string Image2_base64 = "";
        public string IMAGE2_BASE64
        {
            get { return Image2_base64; }
            set
            {
                Image2_base64 = value;
                NotifyPropertyChanged("IMAGE2_BASE64");
            }
        }

        private string Image3_base64 = "";
        public string IMAGE3_BASE64
        {
            get { return Image3_base64; }
            set
            {
                Image3_base64 = value;
                NotifyPropertyChanged("IMAGE3_BASE64");
            }
        }

        private Xamarin.Forms.ImageSource image1= (FileImageSource)ImageSource.FromFile("camera.png");
        public Xamarin.Forms.ImageSource Image1
        {
            get { return image1; }
            set
            {
                image1 = value;
                NotifyPropertyChanged("Image1");
            }
        }
        private Xamarin.Forms.ImageSource image2=(FileImageSource)ImageSource.FromFile("camera.png");
        public Xamarin.Forms.ImageSource Image2
        {
            get { return image2; }
            set
            {
                image2 = value;
                NotifyPropertyChanged("Image2");
            }
        }
        private Xamarin.Forms.ImageSource image3= (FileImageSource)ImageSource.FromFile("camera.png");
        public Xamarin.Forms.ImageSource Image3
        {
            get { return image3; }
            set
            {
                image3 = value;
                NotifyPropertyChanged("Image3");
            }
        }

        private string _CompanyID;
        public string COMPANYID
        {
            get
            {
                return _CompanyID;
            }
            set
            {
                _CompanyID = value;
                NotifyPropertyChanged("COMPANYID");

                //put here your code  
                // SYear = selected_year.Year;
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

        private string _ASSETNAME;
        public string ASSETNAME
        {
            get
            {
                return _ASSETNAME;
            }
            set
            {
                _ASSETNAME = value;
                NotifyPropertyChanged("ASSETNAME");

                //put here your code  
                // SYear = selected_year.Year;
            }
        }

       

        private string _FROMLOCATION;
        public string FROMLOCATION
        {
            get
            {
                return _FROMLOCATION;
            }
            set
            {
                _FROMLOCATION = value;
                NotifyPropertyChanged("FROMLOCATION");

                //put here your code  
                // SYear = selected_year.Year;
            }
        }

        private string _TOLOCATION;
        public string TOLOCATION
        {
            get
            {
                return _TOLOCATION;
            }
            set
            {
                _TOLOCATION = value;
                NotifyPropertyChanged("TOLOCATION");

                //put here your code  
                // SYear = selected_year.Year;
            }
        }


        private string _FROMBRANCH;
        public string FROMBRANCH
        {
            get
            {
                return _FROMBRANCH;
            }
            set
            {
                _FROMBRANCH = value;
                NotifyPropertyChanged("FROMBRANCH");

                //put here your code  
                // SYear = selected_year.Year;
            }
        }

        private string _TOBRANCH;
        public string TOBRANCH
        {
            get
            {
                return _TOBRANCH;
            }
            set
            {
                _TOBRANCH = value;
                NotifyPropertyChanged("TOBRANCH");

                //put here your code  
                // SYear = selected_year.Year;
            }
        }

        private string _Remarks;
        public string Remarks
        {
            get
            {
                return _Remarks;
            }
            set
            {
                _Remarks = value;
                NotifyPropertyChanged("Remarks");

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
        public async Task GetLocations()
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


                    var client = new RestClient(ProjectConstants.GETLOCATIONS_API);
                    var request = new RestRequest(Method.GET);
                    // request.AddHeader("postman-token", "e3fa53b1-0f94-c75d-d04a-e97018565406");
                    request.AddHeader("cache-control", "no-cache");
                    request.AddHeader("content-type", "application/x-www-form-urlencoded");
                    //  request.AddParameter("application/x-www-form-urlencoded", "Truck_No=GJ01DX8008&CType=Loading&Branch_Id=130", ParameterType.RequestBody);
                    IRestResponse response = client.Execute(request);

                    // Extracting output data from received response
                    string strresponse = response.Content;

                    LocationMasterResp stocktake = new LocationMasterResp();

                    // List<BranchWiseAssets> mystocklist = new List<BranchWiseAssets>();
                    if (response.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        stocktake = JsonConvert.DeserializeObject<LocationMasterResp>(strresponse);
                        if (stocktake.Status.Equals("true"))
                        {

                            LocationList = stocktake.Locations;


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

       public Command SUBMIT
        {
            get
            {
                return new Command(MoveAsset);
            }
        }

        private async void MoveAsset(object obj)
        {
           // string branch = Preferences.Get(Pref.BRANCH, "");

            try
            {
                IsBusy = true;
                IsEnable = true;
                IsVisible = true;
                BTNSUBMITSTATUS = false;

                //.....................................................


                POST_MoveAssetDataRequest pOST_Move = new POST_MoveAssetDataRequest();
                pOST_Move.AssetMove_Date = DateTime.Now.Date.ToString();
                pOST_Move.Company_Id = _details.Company_Id;
                pOST_Move.Asset_id = _details.Asset_id;
                pOST_Move.Asset_name = _details.Asset_name;
                pOST_Move.Employee = _details.Employee;
                pOST_Move.From_Floor = _details.Branch;
                pOST_Move.From_Location_Description = _details.Location;
                pOST_Move.To_Floor = TOBRANCH;
                pOST_Move.To_Location_Description = TOLOCATION;
                pOST_Move.Status = false;
                pOST_Move.Remarks = Remarks;
                pOST_Move.Img1 = IMAGE1_BASE64;
                pOST_Move.Img2 = IMAGE2_BASE64;
                pOST_Move.Img3 = IMAGE3_BASE64;
                //...........................
                var client = new System.Net.Http.HttpClient();
                // client.BaseAddress = new Uri("http://114.143.156.30/");
                client.BaseAddress = new Uri(ProjectConstants.BASE_URL);
                var json = JsonConvert.SerializeObject(pOST_Move);
                if (CrossConnectivity.Current.IsConnected)  // checking internet connection
                {

                    StringContent str = new StringContent(json, Encoding.UTF8, "application/json");
                    var response = await client.PostAsync(new Uri(ProjectConstants.POST_ASSETMOVEDATA_API), str);
                    var responseJson = response.Content.ReadAsStringAsync().Result;
                    PostResponseForAll stocktakeresponse = new PostResponseForAll();

                    if (response.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        stocktakeresponse = JsonConvert.DeserializeObject<PostResponseForAll>(responseJson);

                        if (stocktakeresponse.Status.Equals("true"))
                        {
                            await App.Current.MainPage.DisplayAlert("Alert", "Data submitted successfully.", "Ok");
                           
                            await App.Current.MainPage.Navigation.PopAsync();
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
                Crashes.TrackError(excp);

            }
            IsBusy = false;
            IsEnable = false;
            IsVisible = false;
            BTNSUBMITSTATUS = true;
        }
    }
}
