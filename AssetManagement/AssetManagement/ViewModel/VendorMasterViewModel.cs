using AssetManagement.Constants;
using AssetManagement.Model;
using Microsoft.AppCenter.Crashes;
using Newtonsoft.Json;
using Plugin.Connectivity;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace AssetManagement.ViewModel
{
    public class VendorMasterViewModel:BaseViewModel
    {
        public VendorMasterViewModel()
        {
            IsBusy = false;
            IsEnable = false;
            IsVisible = false;
        }

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

        private string _VENDOR_CODE;
        public string VENDOR_CODE
        {
            get { return _VENDOR_CODE; }
            set
            {
                _VENDOR_CODE = value;
                NotifyPropertyChanged("VENDOR_CODE");
            }
        }

        private string _VENDOR_NAME;
        public string VENDOR_NAME
        {
            get { return _VENDOR_NAME; }
            set
            {
                _VENDOR_NAME = value;
                NotifyPropertyChanged("VENDOR_NAME");
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

        private string _CITY;
        public string CITY
        {
            get { return _CITY; }
            set
            {
                _CITY = value;
                NotifyPropertyChanged("CITY");
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

                CreateVendorRequest vendor = new CreateVendorRequest();

                vendor.Company_Id = company_id;
                vendor.Vendor_code = VENDOR_CODE;
                vendor.Vendor_Name = VENDOR_NAME;
                vendor.Address1 = ADDRESS1;
                vendor.Address2 = ADDRESS2;
                vendor.City = CITY;
                vendor.ZipCode = ZIPCODE;
                vendor.Contact = CONTACT;
                vendor.EmailId = EMAIL;
                vendor.gstno = GSTNO;

                //...........................
                var client = new System.Net.Http.HttpClient();
                // client.BaseAddress = new Uri("http://114.143.156.30/");
                client.BaseAddress = new Uri(ProjectConstants.BASE_URL);
                var json = JsonConvert.SerializeObject(vendor);
                if (CrossConnectivity.Current.IsConnected)  // checking internet connection
                {

                    StringContent str = new StringContent(json, Encoding.UTF8, "application/json");
                    var response = await client.PostAsync(new Uri(ProjectConstants.CREATEVENDOR_API), str);
                    var responseJson = response.Content.ReadAsStringAsync().Result;
                    PostResponseForAll stocktakeresponse = new PostResponseForAll();

                    if (response.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        stocktakeresponse = JsonConvert.DeserializeObject<PostResponseForAll>(responseJson);

                        if (stocktakeresponse.Status.Equals("true"))
                        {
                            await App.Current.MainPage.DisplayAlert("Alert", "Vendor created successfully.", "Ok");

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
