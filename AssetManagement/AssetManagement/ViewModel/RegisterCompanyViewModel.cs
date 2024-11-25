using AssetManagement.Constants;
using AssetManagement.Model;
using Microsoft.AppCenter.Crashes;
using Newtonsoft.Json;
using Plugin.Connectivity;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using Xamarin.Forms;

namespace AssetManagement.ViewModel
{
    public class RegisterCompanyViewModel : BaseViewModel
    {
        public RegisterCompanyViewModel()
        {
            IsBusy = false;
            IsEnable = false;
            IsVisible = false;
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

        private Xamarin.Forms.ImageSource image1 = (FileImageSource)ImageSource.FromFile("users.png");
        public Xamarin.Forms.ImageSource Image1
        {
            get { return image1; }
            set
            {
                image1 = value;
                NotifyPropertyChanged("Image1");
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

        private string _companyid;
        public string COMPANYID
        {
            get { return _companyid; }
            set
            {
                _companyid = value;
                NotifyPropertyChanged("COMPANYID");
            }
        }
        private string _COMPANYNAME;
        public string COMPANYNAME
        {
            get { return _COMPANYNAME; }
            set
            {
                _COMPANYNAME = value;
                NotifyPropertyChanged("COMPANYNAME");
            }
        }

        private string _ADDRESS1;
        public string ADDRESS1
        {
            get { return _ADDRESS1; }
            set
            {
                _ADDRESS1 = value;
                NotifyPropertyChanged("ADDRESS1");
            }
        }
        private string _ADDRESS2;
        public string ADDRESS2
        {
            get { return _ADDRESS2; }
            set
            {
                _ADDRESS2 = value;
                NotifyPropertyChanged("ADDRESS2");
            }
        }

        private string _ZIPCODE;
        public string ZIPCODE
        {
            get { return _ZIPCODE; }
            set
            {
                _ZIPCODE = value;
                NotifyPropertyChanged("ZIPCODE");
            }
        }

        private string _gstno;
        public string GSTNO
        {
            get { return _gstno; }
            set
            {
                _gstno = value;
                NotifyPropertyChanged("GSTNO");
            }
        }

        public List<string> User_Roles
        {
            get
            {
                return new List<string> { "Select User Role", "Administrator", "User", "Supervisor", "Editor" };
            }
        }

        private int _selectedUser_role;
        public int SELECTEDUSER_ROLE
        {
            get
            {
                return _selectedUser_role;
            }
            set
            {
                _selectedUser_role = value;
                NotifyPropertyChanged("SELECTEDUSER_ROLE");
            }
        }

        public Command REGISTER
        {
            get
            {
                return new Command(Register_Company);
            }
        }

        private async void Register_Company(object obj)
        {
            if (!string.IsNullOrEmpty(EMPNAME) && !string.IsNullOrEmpty(BRANCH) && !string.IsNullOrEmpty(CONTACT) && !string.IsNullOrEmpty(LOCATION) && !string.IsNullOrEmpty(DEPARTMENT) && !string.IsNullOrEmpty(EMAIL) && !string.IsNullOrEmpty(PASSWORD) && !string.IsNullOrEmpty(EMPCODE) && !string.IsNullOrEmpty(USERNAME) && !string.IsNullOrEmpty(COMPANYID) && !string.IsNullOrEmpty(COMPANYNAME))
            {
                if (PASSWORD.Equals(CONFIRMPASSWORD))
                {
                    try
                    {
                        IsBusy = true;
                        IsEnable = true;
                        IsVisible = true;
                        BTNSUBMITSTATUS = false;

                        //.....................................................
                        // string company_id = Preferences.Get(Pref.Company_Id, "");

                        CreateCompanyRequest creaeusers = new CreateCompanyRequest();

                        creaeusers.Cdate = DateTime.Now.ToShortDateString();
                        creaeusers.Branch = BRANCH.Trim();
                        creaeusers.Company_Id = COMPANYID.Trim();
                        creaeusers.Company_Name = COMPANYNAME.Trim();
                        creaeusers.Address1 = ADDRESS1.Trim();
                        creaeusers.Address2 = ADDRESS2.Trim();
                        creaeusers.ZipCode = ZIPCODE;
                        creaeusers.Contact = CONTACT.Trim();
                        creaeusers.Department = DEPARTMENT.Trim();
                        creaeusers.EmailId = EMAIL.Trim();
                        creaeusers.Emp_Name = EMPNAME;
                        creaeusers.IsActive = "True";
                        creaeusers.Location = LOCATION.Trim();
                        creaeusers.Password = PASSWORD.Trim();
                        creaeusers.pic = IMAGE1_BASE64;
                        creaeusers.User_Code = EMPCODE.Trim();
                        creaeusers.User_Name = USERNAME.Trim();
                        creaeusers.gstno = GSTNO;
                        creaeusers.User_Role = SELECTEDUSER_ROLE;
                        //...........................
                        var client = new System.Net.Http.HttpClient();
                        // client.BaseAddress = new Uri("http://114.143.156.30/");
                        client.BaseAddress = new Uri(ProjectConstants.BASE_URL);
                        var json = JsonConvert.SerializeObject(creaeusers);
                        if (CrossConnectivity.Current.IsConnected)  // checking internet connection
                        {

                            StringContent str = new StringContent(json, Encoding.UTF8, "application/json");
                            var response = await client.PostAsync(new Uri(ProjectConstants.REGISTER_COMPANY_API), str);
                            var responseJson = response.Content.ReadAsStringAsync().Result;
                            PostResponseForAll stocktakeresponse = new PostResponseForAll();

                            if (response.StatusCode == System.Net.HttpStatusCode.OK)
                            {
                                stocktakeresponse = JsonConvert.DeserializeObject<PostResponseForAll>(responseJson);

                                if (stocktakeresponse.Status.Equals("true"))
                                {
                                    await App.Current.MainPage.DisplayAlert("Alert", "Company Registered successfully.", "Ok");

                                    // await App.Current.MainPage.Navigation.PopAsync();
                                    Application.Current.MainPage = new NavigationPage(new MainPage());
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
    }
}
