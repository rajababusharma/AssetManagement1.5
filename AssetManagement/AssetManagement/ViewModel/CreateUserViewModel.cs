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

namespace AssetManagement.ViewModel
{
    public class CreateUserViewModel:BaseViewModel
    {
        List<string> dept = new List<string>(new string[] { "No Department" });
        List<string> loc = new List<string>(new string[] { "No Location" });
        List<string> br = new List<string>(new string[] { "No Branch" });
        public CreateUserViewModel()
        {
            IsBusy = false;
            IsEnable = false;
            IsVisible = false;
          //  GetLocations();
           // GetBranches();
            GetDepartment();
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

        private async Task GetDepartment()
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

                            DepartmentList = apiresponse.DepartmentList??dept;
                            LocationList = apiresponse.LocationList??loc;
                            BranchList = apiresponse.BranchList??br;

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

                    DepartmentList = null;
                    //await App.Current.MainPage.DisplayAlert("Exception", "Request could n, please try again later", "Ok");
                    Crashes.TrackError(excp);

                }

            }
            else
            {

                await App.Current.MainPage.DisplayAlert("Alert", "No internet connection, please check and try again later.", "OK");
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

        private string _EMPNAME;
        public string EMPNAME
        {
            get { return _EMPNAME; }
            set
            {
                _EMPNAME = value;
                NotifyPropertyChanged("EMPNAME");
            }
        }

        private string _EMPCODE;
        public string EMPCODE
        {
            get { return _EMPCODE; }
            set
            {
                _EMPCODE = value;
                NotifyPropertyChanged("EMPCODE");
            }
        }

        private string _USERNAME;
        public string USERNAME
        {
            get { return _USERNAME; }
            set
            {
                _USERNAME = value;
                NotifyPropertyChanged("USERNAME");
            }
        }

        private string _PASSWORD;
        public string PASSWORD
        {
            get { return _PASSWORD; }
            set
            {
                _PASSWORD = value;
                NotifyPropertyChanged("PASSWORD");
            }
        }

        private string _CONFIRMPASSWORD;
        public string CONFIRMPASSWORD
        {
            get { return _CONFIRMPASSWORD; }
            set
            {
                _CONFIRMPASSWORD = value;
                NotifyPropertyChanged("CONFIRMPASSWORD");
            }
        }


        private string _DEPARTMENT;
        public string DEPARTMENT
        {
            get { return _DEPARTMENT; }
            set
            {
                _DEPARTMENT = value;
                NotifyPropertyChanged("DEPARTMENT");
            }
        }


        private string _LOCATION;
        public string LOCATION
        {
            get { return _LOCATION; }
            set
            {
                _LOCATION = value;
                NotifyPropertyChanged("LOCATION");
            }
        }

        private string _BRANCH;
        public string BRANCH
        {
            get { return _BRANCH; }
            set
            {
                _BRANCH = value;
                NotifyPropertyChanged("BRANCH");
            }
        }

        private string _CONTACT;
        public string CONTACT
        {
            get { return _CONTACT; }
            set
            {
                _CONTACT = value;
                NotifyPropertyChanged("CONTACT");
            }
        }

        private string _EMAIL;
        public string EMAIL
        {
            get { return _EMAIL; }
            set
            {
                _EMAIL = value;
                NotifyPropertyChanged("EMAIL");
            }
        }

        List<String> _departmentList = new List<string>();

        public List<String> DepartmentList
        {
            get { return _departmentList; }

            set
            {
                if (_departmentList != value)
                {
                    _departmentList = value;
                    // OnPropertyChanged("ObjPaymentList");
                    NotifyPropertyChanged("DepartmentList");
                }
            }
        }

        List<String> _branchList = new List<string>();

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

        List<String> _LocationList = new List<string>();

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



        public Command REGISTER
        {
            get
            {
                return new Command(Register_User);
            }
        }

        private async void Register_User(object obj)
        {
            if (!string.IsNullOrEmpty(EMPNAME) && !string.IsNullOrEmpty(BRANCH) && !string.IsNullOrEmpty(CONTACT) && !string.IsNullOrEmpty(LOCATION) && !string.IsNullOrEmpty(DEPARTMENT) && !string.IsNullOrEmpty(EMAIL) && !string.IsNullOrEmpty(PASSWORD) && !string.IsNullOrEmpty(EMPCODE) && !string.IsNullOrEmpty(USERNAME))
            {
                if (PASSWORD.Trim().Equals(CONFIRMPASSWORD.Trim()))
            {
                try
                {
                    IsBusy = true;
                    IsEnable = true;
                    IsVisible = true;
                    BTNSUBMITSTATUS = false;

                    //.....................................................
                    string company_id = Preferences.Get(Pref.Company_Id, "");

                    CreateUserRequest creaeusers = new CreateUserRequest();

                    creaeusers.Cdate = DateTime.Now.ToShortDateString();
                    creaeusers.Branch = BRANCH;
                    creaeusers.Company_Id = company_id;
                    creaeusers.Contact = CONTACT;
                    creaeusers.Department = DEPARTMENT;
                    creaeusers.EmailId = EMAIL.Trim();
                    creaeusers.Emp_Name = EMPNAME;
                    creaeusers.IsActive = "True";
                    creaeusers.Location = LOCATION;
                    creaeusers.Password = PASSWORD.Trim();
                    creaeusers.pic = "";
                    creaeusers.User_Code = EMPCODE.Trim();
                    creaeusers.User_Name = USERNAME.Trim();
                        creaeusers.pic = IMAGE1_BASE64;

                    //...........................
                    var client = new System.Net.Http.HttpClient();
                    // client.BaseAddress = new Uri("http://114.143.156.30/");
                    client.BaseAddress = new Uri(ProjectConstants.BASE_URL);
                    var json = JsonConvert.SerializeObject(creaeusers);
                    if (CrossConnectivity.Current.IsConnected)  // checking internet connection
                    {

                        StringContent str = new StringContent(json, Encoding.UTF8, "application/json");
                        var response = await client.PostAsync(new Uri(ProjectConstants.CREATEUSERS_API), str);
                        var responseJson = response.Content.ReadAsStringAsync().Result;
                        PostResponseForAll stocktakeresponse = new PostResponseForAll();

                        if (response.StatusCode == System.Net.HttpStatusCode.OK)
                        {
                            stocktakeresponse = JsonConvert.DeserializeObject<PostResponseForAll>(responseJson);

                            if (stocktakeresponse.Status.Equals("true"))
                            {
                                await App.Current.MainPage.DisplayAlert("Alert", "User created successfully.", "Ok");

                                // await App.Current.MainPage.Navigation.PopAsync();
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
            else
            {
                await App.Current.MainPage.DisplayAlert("Alert", "Password do not match.", "Ok");
            }
            }
            else
            {
                await App.Current.MainPage.DisplayAlert("Alert", "Please fill all the details.", "Ok");
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
    }
}
