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
    public class DisposeViewModel:BaseViewModel
    {
        STockTallyDetails _details;
        public DisposeViewModel(STockTallyDetails details)
        {
            _details = new STockTallyDetails();
            _details = details;
            ASSETID = details.Asset_id;
            ASSETNAME = details.Asset_name;
           
            List<string> modelist = new List<string>(new string[] { "Sell", "Destroyed/Scrap" });
            List<string> reasonlist = new List<string>(new string[] { "out dated", "no useability" });
            DISPOSALMODES = modelist;
            DISPOSALREASONS = reasonlist;

            
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

        List<String> _disposalmodes;

        public List<String> DISPOSALMODES
        {
            get { return _disposalmodes; }

            set
            {
                if (_disposalmodes != value)
                {
                    _disposalmodes = value;
                    // OnPropertyChanged("ObjPaymentList");
                    NotifyPropertyChanged("DISPOSALMODES");
                }
            }
        }
        List<String> _disposalreasons;

        public List<String> DISPOSALREASONS
        {
            get { return _disposalreasons; }

            set
            {
                if (_disposalreasons != value)
                {
                    _disposalreasons = value;
                    // OnPropertyChanged("ObjPaymentList");
                    NotifyPropertyChanged("DISPOSALREASONS");
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
        private Xamarin.Forms.ImageSource image2 = (FileImageSource)ImageSource.FromFile("camera.png");
        public Xamarin.Forms.ImageSource Image2
        {
            get { return image2; }
            set
            {
                image2 = value;
                NotifyPropertyChanged("Image2");
            }
        }
        private Xamarin.Forms.ImageSource image3 = (FileImageSource)ImageSource.FromFile("camera.png");
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

        private string _residualvalue;
        public string RESIDUALVALUE
        {
            get
            {
                return _residualvalue;
            }
            set
            {
                _residualvalue = value;
                NotifyPropertyChanged("RESIDUALVALUE");

            }
        }

        private string _disposalmode;
        public string DISPOSALMODE
        {
            get
            {
                return _disposalmode;
            }
            set
            {
                _disposalmode = value;
                NotifyPropertyChanged("DISPOSALMODE");

            }
        }

        private string _reason;
        public string REASON
        {
            get
            {
                return _reason;
            }
            set
            {
                _reason = value;
                NotifyPropertyChanged("REASON");

            }
        }


        public Command SUBMIT
        {
            get
            {
                return new Command(DisposeAsset);
            }
        }

        private async void DisposeAsset(object obj)
        {
            try
            {
                IsBusy = true;
                IsEnable = true;
                IsVisible = true;
                BTNSUBMITSTATUS = false;

                //.....................................................
               string authorisedby= Preferences.Get(Pref.Emp_Name, "");

                PostDisposeAssetRequest dispose = new PostDisposeAssetRequest();
                dispose.AssetDispose_Date = DateTime.Now.Date.ToString();
                dispose.Company_Id = _details.Company_Id;
                dispose.Asset_id = _details.Asset_id;
                dispose.Asset_name = _details.Asset_name;
                dispose.FinancialYear = _details.sDate;
                dispose.Description = _details.Description;
                dispose.Location = _details.Location;
                dispose.Branch = _details.Branch;
                dispose.ReasonFor_Dispose = REASON;
                dispose.ModeOf_Disposal = DISPOSALMODE;
                dispose.Authorized_By = authorisedby;
                dispose.Residual_Value = RESIDUALVALUE;
                dispose.Remarks = Remarks;
                dispose.Img1 = IMAGE1_BASE64;
                dispose.Img2 = IMAGE2_BASE64;
                dispose.Img3 = IMAGE3_BASE64;
                //...........................
                var client = new System.Net.Http.HttpClient();
                // client.BaseAddress = new Uri("http://114.143.156.30/");
                client.BaseAddress = new Uri(ProjectConstants.BASE_URL);
                var json = JsonConvert.SerializeObject(dispose);
                if (CrossConnectivity.Current.IsConnected)  // checking internet connection
                {

                    StringContent str = new StringContent(json, Encoding.UTF8, "application/json");
                    var response = await client.PostAsync(new Uri(ProjectConstants.POST_ASSETDISPOSE_API), str);
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
