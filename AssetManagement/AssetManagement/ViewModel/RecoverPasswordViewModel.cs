using AssetManagement.Constants;
using AssetManagement.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace AssetManagement.ViewModel
{
    public class RecoverPasswordViewModel:BaseViewModel
    {
        public RecoverPasswordViewModel()
        {
            IsBusy = false;
            IsEnable = false;
            IsVisible = false;
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

        private bool isbtnEnable = true;
        public bool BTNSUBMITSTATUS
        {
            get { return isbtnEnable; }
            set
            {
                isbtnEnable = value;
                NotifyPropertyChanged("BTNSUBMITSTATUS");
            }
        }

        private string email;
        public string EMAIL
        {
            get { return email; }
            set
            {
                email = value;
                NotifyPropertyChanged("EMAIL");
            }
        }

        private string password;
        public string PASSWORD
        {
            get { return password; }
            set
            {
                password = value;
                NotifyPropertyChanged("PASSWORD");
            }
        }
        private string employee;
        public string EMPLOYEE
        {
            get { return employee; }
            set
            {
                employee = value;
                NotifyPropertyChanged("EMPLOYEE");
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
           await LoginCall(EMAIL);
        }

        public async Task SendEmail(string to,string pwd)
        {
            try
            {
               
                var client = new System.Net.Http.HttpClient();
                client.BaseAddress = new Uri(ProjectConstants.SendMail_API);
                try
                {
                    var response = await client.GetAsync("SendMail?to=" + to +"&pwd="+pwd);
                    // var response = await client.GetAsync;


                    var recoveryresponse = response.Content.ReadAsStringAsync().Result;
                    PostResponseForAll loginobject = new PostResponseForAll();
                    if (response.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        if (recoveryresponse != "")
                        {
                            loginobject = JsonConvert.DeserializeObject<PostResponseForAll>(recoveryresponse);
                            // await DisplayAlert("Access Token", loginobject.Status.ToString(), "Ok");
                            if (loginobject.Status.Equals("true"))
                            {


                                /* List<string> toAddress = new List<string>();
                                 toAddress.Add(EMAIL);
                                 await SendEmail("Password Recovery", "Mr. "+EMPLOYEE+", you have recovered your password successfully. Your password is: "+PASSWORD, toAddress);
                                 await App.Current.MainPage.Navigation.PushModalAsync(new MainPage());*/
                                await App.Current.MainPage.DisplayAlert("Alert", "Password has been sent to your registered email address.", "Ok");
                              

                            }
                            else
                            {
                                await App.Current.MainPage.DisplayAlert("Exception", loginobject.Msg.ToString(), "Ok");
                                
                            }

                        }
                    }
                    else
                    {
                        await App.Current.MainPage.DisplayAlert("Exception", response.ReasonPhrase, "Ok");
                    }
                    
                }
                catch (Exception excp)
                {

                    // Common.SaveLogs(excp.StackTrace);
                    await App.Current.MainPage.DisplayAlert("Exception", excp.Message, "Ok");

                   

                }
                // await App.Current.MainPage.DisplayAlert("Alert", "Password has been sent to your registered email address.", "Ok");
            }
           
            catch (Exception ex)
            {
                // Some other exception occurred  
                await App.Current.MainPage.DisplayAlert("Alert", ex.Message, "Ok");
            }
        }
        public async Task LoginCall(string email)
        {

            IsBusy = true;
            IsEnable = true;
            IsVisible = true;
            BTNSUBMITSTATUS = false;
            var client = new System.Net.Http.HttpClient();
            client.BaseAddress = new Uri(ProjectConstants.RECOVERPASSWORD_API);
            try
            {
                var response = await client.GetAsync("GetPassword?email=" + email.Trim());
                // var response = await client.GetAsync;


                var loginJson = response.Content.ReadAsStringAsync().Result;
                RecoverPasswordResp loginobject = new RecoverPasswordResp();
                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    if (loginJson != "")
                    {
                        loginobject = JsonConvert.DeserializeObject<RecoverPasswordResp>(loginJson);
                        // await DisplayAlert("Access Token", loginobject.Status.ToString(), "Ok");
                        if (loginobject.Status.Equals("true"))
                        {
                            EMPLOYEE = loginobject.recoverPassword.Emp_Name;
                            PASSWORD= loginobject.recoverPassword.Password;

                            /* List<string> toAddress = new List<string>();
                             toAddress.Add(EMAIL);
                             await SendEmail("Password Recovery", "Mr. "+EMPLOYEE+", you have recovered your password successfully. Your password is: "+PASSWORD, toAddress);
                             await App.Current.MainPage.Navigation.PushModalAsync(new MainPage());*/
                           await SendEmail(email.Trim(), PASSWORD);
                            IsBusy = false;
                            IsEnable = false;
                            IsVisible = false;
                            BTNSUBMITSTATUS = true;

                        }
                        else
                        {
                            await App.Current.MainPage.DisplayAlert("Exception", loginobject.Msg.ToString(), "Ok");
                            IsBusy = false;
                            IsEnable = false;
                            IsVisible = false;
                            BTNSUBMITSTATUS = true;
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
                BTNSUBMITSTATUS = true;
            }
            catch (Exception excp)
            {

                // Common.SaveLogs(excp.StackTrace);
                await App.Current.MainPage.DisplayAlert("Exception", excp.Message, "Ok");
                
                IsBusy = false;
                IsEnable = false;
                IsVisible = false;
                BTNSUBMITSTATUS = true;

            }

            // return placeobject.places;
        }
    }
}
