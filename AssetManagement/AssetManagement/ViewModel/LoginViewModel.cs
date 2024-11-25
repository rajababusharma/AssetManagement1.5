using Android.Content.Res;
using AssetManagement.Constants;
using AssetManagement.Interface;
using AssetManagement.Model;
using AssetManagement.View;
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
    public class LoginViewModel:BaseViewModel
    {
        public LoginViewModel()
        {
            IsRemember = false;
           // IsCompanyExist();
        }
       

             private string emp_code;
        public string EmpCode
        {
            get { return emp_code; }
            set
            {
                emp_code = value;

                NotifyPropertyChanged("EmpCode");
            }
        }

        private string password;
        public string Password
        {
            get { return password; }
            set
            {
                password = value;
                NotifyPropertyChanged("Password");
            }
        }
        public Command LoginCommand
        {
            get
            {
                return new Command(Login);
            }
        }

        public Command REGISTER
        {
            get
            {
                return new Command(GoToRegistration);
            }
        }

        private async void GoToRegistration(object obj)
        {
           await App.Current.MainPage.Navigation.PushModalAsync(new CompanyRegistration());
        }

        private async void Login()
        {
            if (CrossConnectivity.Current.IsConnected)
            {
                // SYear = Preferences.Get(ProjectConstants.FYEAR, "");
                //null or empty field validation, check weather email and password is null or empty  
               
                if (string.IsNullOrEmpty(EmpCode) || string.IsNullOrEmpty(Password))

                    await App.Current.MainPage.DisplayAlert("Empty Values", "Please enter Emp code and Password", "OK");
                else
                {

                    await LoginCall(EmpCode, Password);

                   // Application.Current.MainPage = new NavigationPage(new MasterDetailPage1());

                }
            }
            else
            {
                
                await App.Current.MainPage.DisplayAlert("Alert", "No internet connection, please check and try again later.", "OK");
            }

        }


        private bool _IsEnableRegister = false;
        public bool IsEnableRegister
        {
            get { return _IsEnableRegister; }
            set
            {
                _IsEnableRegister = value;
                NotifyPropertyChanged("IsEnableRegister");
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

        private bool isbtnEnable = true;
        public bool IsBTNEnable
        {
            get { return isbtnEnable; }
            set
            {
                isbtnEnable = value;
                NotifyPropertyChanged("IsBTNEnable");
            }
        }

        private bool isRemember = true;
        public bool IsRemember
        {
            get { return isRemember; }
            set
            {
                isRemember = value;
                NotifyPropertyChanged("IsRemember");
            }
        }

        public async Task LoginCall(string emp, string pass)
        {

            IsBusy = true;
            IsEnable = true;
            IsVisible = true;
            IsBTNEnable = false;
            var client = new System.Net.Http.HttpClient();
            client.BaseAddress = new Uri(ProjectConstants.LOGIN_API);
            try
            {
                var response = await client.GetAsync("Login?username=" + emp.Trim() + "&password=" + pass.Trim());
                // var response = await client.GetAsync;


                var loginJson = response.Content.ReadAsStringAsync().Result;
                LoginResponse loginobject = new LoginResponse();
                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    if (loginJson != "")
                    {
                        loginobject = JsonConvert.DeserializeObject<LoginResponse>(loginJson);
                        // await DisplayAlert("Access Token", loginobject.Status.ToString(), "Ok");
                        if (loginobject.Status.Equals("true"))
                        {
                            Preferences.Set(Pref.USERNAME, loginobject.UserDetails.User_Name);
                            Preferences.Set(Pref.PIC, loginobject.UserDetails.Pic);

                            Preferences.Set(Pref.Company_Id, loginobject.UserDetails.Company_Id);
                            Preferences.Set(Pref.Emp_Name, loginobject.UserDetails.Emp_Name);
                            Preferences.Set(Pref.User_Code, loginobject.UserDetails.User_Code);
                            Preferences.Set(Pref.BRANCH, loginobject.UserDetails.Branch);
                            Preferences.Set(Pref.USERNAME, loginobject.UserDetails.User_Name);

                            Preferences.Set(Pref.Manage_Assets, loginobject.UserDetails.Manage_Assets);
                            Preferences.Set(Pref.Move_Asset, loginobject.UserDetails.Move_Asset);
                            Preferences.Set(Pref.Transfer_Asset, loginobject.UserDetails.Transfer_Asset);
                            Preferences.Set(Pref.Dispose_Asset, loginobject.UserDetails.Dispose_Asset);
                            Preferences.Set(Pref.Repair_Asset, loginobject.UserDetails.Repair_Asset);
                            Preferences.Set(Pref.Rights_Managment, loginobject.UserDetails.Rights_Managment);
                            Preferences.Set(Pref.User_Role, loginobject.UserDetails.User_Role);
                            if (IsRemember)
                            {
                                Preferences.Set(Pref.LOGINSTATUS, true);
                            }
                            else
                            {
                                Preferences.Set(Pref.LOGINSTATUS, false);
                            }
                           
                             Application.Current.MainPage = new NavigationPage(new MasterDetailPage1());
                           await CommonClass.GetMoveNotification();
                            IsBusy = false;
                            IsEnable = false;
                            IsVisible = false;
                            IsBTNEnable = true;
                           
                        }
                        else
                        {
                            await App.Current.MainPage.DisplayAlert("Exception", loginobject.Msg.ToString(), "Ok");
                            IsBusy = false;
                            IsEnable = false;
                            IsVisible = false;
                            IsBTNEnable = true;
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
                IsBTNEnable = true;
            }
            catch (Exception excp)
            {
                
                // Common.SaveLogs(excp.StackTrace);
                await App.Current.MainPage.DisplayAlert("Exception", excp.Message, "Ok");
                Crashes.TrackError(excp);
                IsBusy = false;
                IsEnable = false;
                IsVisible = false;
                IsBTNEnable = true;

            }

            // return placeobject.places;
        }
        public async Task IsCompanyExist()
        {
           
            try
            {

                var client = new System.Net.Http.HttpClient();
                //  client.BaseAddress = new Uri("http://114.143.156.30/");
                client.BaseAddress = new Uri(ProjectConstants.ISCOMPANYEXIST_API);
                var response = await client.GetAsync("IsCompanyExist");
                var responseJson = response.Content.ReadAsStringAsync().Result;

                PostResponseForAll apiresponse = new PostResponseForAll();


                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    apiresponse = JsonConvert.DeserializeObject<PostResponseForAll>(responseJson);
                    if (apiresponse.Status.Equals("true"))
                    {
                       
                       IsEnableRegister = false;
                    }
                    else
                    {
                        IsEnableRegister = true;
                    }
                }
                else
                {
                    IsEnableRegister = false;
                }   
            }
            catch (Exception excp)
            {
                IsEnableRegister = false;
                await App.Current.MainPage.DisplayAlert("Exception", excp.Message, "Ok");
                Crashes.TrackError(excp);
            }
        }
    }
}
