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
using static Android.Graphics.Paint;
using static Android.Net.Wifi.WifiEnterpriseConfig;

namespace AssetManagement.ViewModel
{
    public class EmployeeMasterViewModel:BaseViewModel
    {
        List<string> dept = new List<string>(new string[] { "No Department" });
        List<string> loc = new List<string>(new string[] { "No Location" });
        List<string> br = new List<string>(new string[] { "No Branch" });
        public EmployeeMasterViewModel()
        {
            IsBusy = false;
            IsEnable = false;
            IsVisible = false;
            //  GetLocations();
            // GetBranches();
            GetDepartment();
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

                            DepartmentList = apiresponse.DepartmentList ?? dept;
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

        List<String> _EmployeeList = new List<string>(100);


        private bool _BTNSUBMITSTATUS = true;
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

                CreateEmployeeRequest creaeusers = new CreateEmployeeRequest();

                creaeusers.Company_Id = company_id;
                creaeusers.Emp_code = EMPCODE.Trim();
                creaeusers.Emp_Name = EMPNAME.Trim();
                creaeusers.Department = DEPARTMENT;
                creaeusers.Location = LOCATION;
                creaeusers.Branch = BRANCH;
                creaeusers.Contact = CONTACT;
                creaeusers.EmailId = EMAIL.Trim();
               

                //...........................
                var client = new System.Net.Http.HttpClient();
                // client.BaseAddress = new Uri("http://114.143.156.30/");
                client.BaseAddress = new Uri(ProjectConstants.BASE_URL);
                var json = JsonConvert.SerializeObject(creaeusers);
                if (CrossConnectivity.Current.IsConnected)  // checking internet connection
                {

                    StringContent str = new StringContent(json, Encoding.UTF8, "application/json");
                    var response = await client.PostAsync(new Uri(ProjectConstants.CREATEEMPLOYEE_API), str);
                    var responseJson = response.Content.ReadAsStringAsync().Result;
                    PostResponseForAll stocktakeresponse = new PostResponseForAll();

                    if (response.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        stocktakeresponse = JsonConvert.DeserializeObject<PostResponseForAll>(responseJson);

                        if (stocktakeresponse.Status.Equals("true"))
                        {
                            await App.Current.MainPage.DisplayAlert("Alert", "Employee created successfully.", "Ok");

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
    }
}
