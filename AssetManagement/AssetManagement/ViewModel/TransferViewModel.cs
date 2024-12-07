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
    public class TransferViewModel : BaseViewModel
    {
        STockTallyDetails _details;
        public TransferViewModel(STockTallyDetails details)
        {
            GetEmployeeList();
            _details = new STockTallyDetails();
            _details = details;
            ASSETID = details.Asset_id;
            ASSETNAME = details.Asset_name;
           // FROMLOCATION = details.Location;
           // FROMBRANCH = details.Branch;
            FROMEMPLOYEE = details.Employee;
           // List<string> modelist = new List<string>(new string[] { "Sell", "Destroyed" });
           // List<string> reasonlist = new List<string>(new string[] { "Damaged", "Destroyed" });
           // DISPOSALMODES = modelist;
          //  DISPOSALREASONS = reasonlist;

            //  GetLocations();
            // GetBranches();
            // GetVendorList();
            

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

       

       

        List<String> _EmployeeList=new List<string>();

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

        private string _FROMEMPLOYEE;
        public string FROMEMPLOYEE
        {
            get
            {
                return _FROMEMPLOYEE;
            }
            set
            {
                _FROMEMPLOYEE = value;
                NotifyPropertyChanged("FROMEMPLOYEE");

                //put here your code  
                // SYear = selected_year.Year;
            }
        }


        private string _TOEMPLOYEE;
        public string TOEMPLOYEE
        {
            get
            {
                return _TOEMPLOYEE;
            }
            set
            {
                _TOEMPLOYEE = value;
                NotifyPropertyChanged("TOEMPLOYEE");

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
                        // string vaid = "2961";

                        //var response = await client.GetAsync("AuthenticateUser?UserName=TNAX5004&Password=TNAX5004");
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

                                    EmployeeList = docketobject.Employee;





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



  

        public Command SUBMIT
        {
            get
            {
                return new Command(TransferAsset);
            }
        }

        private async void TransferAsset(object obj)
        {
            try
            {
                IsBusy = true;
                IsEnable = true;
                IsVisible = true;
                BTNSUBMITSTATUS = false;

                //.....................................................
                string authorisedby = Preferences.Get(Pref.Emp_Name, "");

                PostAssetTransferRequest dispose = new PostAssetTransferRequest();
                dispose.AssetTransfer_Date = DateTime.Now.Date.ToString();
                dispose.Company_Id = _details.Company_Id;
                dispose.Asset_id = _details.Asset_id;
                dispose.Asset_name = _details.Asset_name;
                dispose.FinancialYear = _details.sDate;
                dispose.Description = _details.Description;
                dispose.From_Employee = _details.Employee;
                dispose.To_Employee = TOEMPLOYEE;
               
                //...........................
                var client = new System.Net.Http.HttpClient();
                // client.BaseAddress = new Uri("http://114.143.156.30/");
                client.BaseAddress = new Uri(ProjectConstants.BASE_URL);
                var json = JsonConvert.SerializeObject(dispose);
                if (CrossConnectivity.Current.IsConnected)  // checking internet connection
                {

                    StringContent str = new StringContent(json, Encoding.UTF8, "application/json");
                    var response = await client.PostAsync(new Uri(ProjectConstants.POST_ASSETTRANSFER_API), str);
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
