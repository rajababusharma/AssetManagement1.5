using AssetManagement.Constants;
using AssetManagement.Model;
using Microsoft.AppCenter.Crashes;
using Newtonsoft.Json;
using Plugin.Connectivity;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace AssetManagement.ViewModel
{
    public class RightsManageentViewModel: BaseViewModel
    {
        public RightsManageentViewModel()
        {
            GetUsersList();

           
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


        private bool createassets = false;
        public bool CREATEASSETS
        {
            get { return createassets; }
            set
            {
                createassets = value;
                NotifyPropertyChanged("CREATEASSETS");
            }
        }

        private bool moveassets = false;
        public bool MOVEASSETS
        {
            get { return moveassets; }
            set
            {
                moveassets = value;
                NotifyPropertyChanged("MOVEASSETS");
            }
        }

        private bool transferassets = false;
        public bool TRANSFERASSETS
        {
            get { return transferassets; }
            set
            {
                transferassets = value;
                NotifyPropertyChanged("TRANSFERASSETS");
            }
        }

        private bool repairassets = false;
        public bool REPAIRASSETS
        {
            get { return repairassets; }
            set
            {
                repairassets = value;
                NotifyPropertyChanged("REPAIRASSETS");
            }
        }

        
             private bool rightsmanagement = false;
        public bool RIGHTS_MANAGMENT
        {
            get { return rightsmanagement; }
            set
            {
                rightsmanagement = value;
                NotifyPropertyChanged("RIGHTS_MANAGMENT");
            }
        }

        private bool disposeassets = false;
        public bool DISPOSEASSETS
        {
            get { return disposeassets; }
            set
            {
                disposeassets = value;
                NotifyPropertyChanged("DISPOSEASSETS");
            }
        }

        private string selectesuser = "";
        public string SELECTESUSER
        {
            get { return selectesuser; }
            set
            {
                selectesuser = value;
                NotifyPropertyChanged("SELECTESUSER");
            }
        }



        List<String> _UsersList = new List<string>();

        public List<String> UsersList
        {
            get { return _UsersList; }

            set
            {
                if (_UsersList != value)
                {
                    _UsersList = value;
                    // OnPropertyChanged("ObjPaymentList");
                    NotifyPropertyChanged("UsersList");
                }
            }
        }

        public async Task GetUsersList()
        {
            if (CrossConnectivity.Current.IsConnected)
            {


                try
                {
                    IsBusy = true;
                    IsEnable = true;
                    IsVisible = true;

                    var client = new System.Net.Http.HttpClient();
                    client.BaseAddress = new Uri(ProjectConstants.GETUSERSLIST_API);
                    try
                    {
                        string branch = Preferences.Get(Pref.BRANCH, "");
                        // string vaid = "2961";

                        //var response = await client.GetAsync("AuthenticateUser?UserName=TNAX5004&Password=TNAX5004");
                        var response = await client.GetAsync("GetEmployeeList");
                        var docketJson = response.Content.ReadAsStringAsync().Result;


                        UserMasterResp docketobject = new UserMasterResp();
                        if (response.StatusCode == System.Net.HttpStatusCode.OK)
                        {
                            if (docketJson != "")
                            {
                                docketobject = JsonConvert.DeserializeObject<UserMasterResp>(docketJson);
                                if (docketobject.Status.Equals("true"))
                                {
                                    // dockets= JsonConvert.DeserializeObject<DeliveryDoket>(docketJson);

                                    UsersList = docketobject.Users;





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

        public async Task GetUsersRights(string user)
        {
            if (CrossConnectivity.Current.IsConnected)
            {


                try
                {
                    IsBusy = true;
                    IsEnable = true;
                    IsVisible = true;

                    var client = new System.Net.Http.HttpClient();
                    client.BaseAddress = new Uri(ProjectConstants.GETUSERSRIGHT_API);
                    try
                    {
                        string branch = Preferences.Get(Pref.BRANCH, "");
                        // string vaid = "2961";

                        //var response = await client.GetAsync("AuthenticateUser?UserName=TNAX5004&Password=TNAX5004");
                        var response = await client.GetAsync("GetUsersRight?user="+ user);
                        var docketJson = response.Content.ReadAsStringAsync().Result;


                        UserRightsResp docketobject = new UserRightsResp();
                        if (response.StatusCode == System.Net.HttpStatusCode.OK)
                        {
                            if (docketJson != "")
                            {
                                docketobject = JsonConvert.DeserializeObject<UserRightsResp>(docketJson);
                                if (docketobject.Status.Equals("true"))
                                {
                                    // dockets= JsonConvert.DeserializeObject<DeliveryDoket>(docketJson);

                                    DISPOSEASSETS = docketobject.UsersRights.Dispose_Asset;
                                    CREATEASSETS = docketobject.UsersRights.Manage_Assets;
                                    MOVEASSETS = docketobject.UsersRights.Move_Asset;
                                    TRANSFERASSETS = docketobject.UsersRights.Transfer_Asset;
                                    REPAIRASSETS = docketobject.UsersRights.Repair_Asset;
                                    RIGHTS_MANAGMENT = docketobject.UsersRights.Rights_Managment;
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

       public Command SAVEPERMISSIONS
        {
            get
            {

                return new Command(SAVE_PERMISSIONS);
            }
        }

        private async void SAVE_PERMISSIONS(object obj)
        {

            bool rights_status = Preferences.Get(Pref.Rights_Managment, false);
            if (!rights_status)
            {
               // BTNSUBMITSTATUS = false;
               await App.Current.MainPage.DisplayAlert("Alert", "You don't have permission to change the settings. Please contact to your admin", "Ok");
            }
            else
            {
                try
                {
                    IsBusy = true;
                    IsEnable = true;
                    IsVisible = true;
                    BTNSUBMITSTATUS = false;

                    //.....................................................
                    string company_id = Preferences.Get(Pref.Company_Id, "");

                    PostUsersRightRequest rights = new PostUsersRightRequest();
                    rights.Company_Id = company_id;
                    rights.User = SELECTESUSER;
                    rights.Manage_Assets = CREATEASSETS;
                    rights.Move_Asset = MOVEASSETS;
                    rights.Repair_Asset = REPAIRASSETS;
                    rights.Transfer_Asset = TRANSFERASSETS;
                    rights.Dispose_Asset = DISPOSEASSETS;
                    rights.Rights_Managment = RIGHTS_MANAGMENT;
                    //...........................
                    var client = new System.Net.Http.HttpClient();
                    // client.BaseAddress = new Uri("http://114.143.156.30/");
                    client.BaseAddress = new Uri(ProjectConstants.BASE_URL);
                    var json = JsonConvert.SerializeObject(rights);
                    if (CrossConnectivity.Current.IsConnected)  // checking internet connection
                    {

                        StringContent str = new StringContent(json, Encoding.UTF8, "application/json");
                        var response = await client.PostAsync(new Uri(ProjectConstants.POST_USERSRIGHT_API), str);
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
}
