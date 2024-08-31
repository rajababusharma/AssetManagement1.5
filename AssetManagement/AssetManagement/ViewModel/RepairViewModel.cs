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
    public class RepairViewModel:BaseViewModel
    {
        STockTallyDetails _details;
        public RepairViewModel(STockTallyDetails details)
        {
           
            _details = new STockTallyDetails();
            _details = details;
            ASSETID = details.Asset_id;
            ASSETNAME = details.Asset_name;
            SELECTEDDTAE = DateTime.Now.ToShortDateString();

            GetVendorList();

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

        

        List<String> _VendorList=new List<string>();

        public List<String> VendorList
        {
            get { return _VendorList; }

            set
            {
                if (_VendorList != value)
                {
                    _VendorList = value;
                    // OnPropertyChanged("ObjPaymentList");
                    NotifyPropertyChanged("VendorList");
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

        private string _selectedvendor;
        public string SELECTEDVENDOR
        {
            get
            {
                return _selectedvendor;
            }
            set
            {
                _selectedvendor = value;
                NotifyPropertyChanged("SELECTEDVENDOR");

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

        private string _selecteddate;
        public string SELECTEDDTAE
        {
            get
            {
                return _selecteddate;
            }
            set
            {
                _selecteddate = value;
                NotifyPropertyChanged("SELECTEDDTAE");

            }
        }


        public async Task GetVendorList()
        {
            if (CrossConnectivity.Current.IsConnected)
            {


                try
                {
                    IsBusy = true;
                    IsEnable = true;
                    IsVisible = true;

                    var client = new System.Net.Http.HttpClient();
                    client.BaseAddress = new Uri(ProjectConstants.GETVENDORLIST_API);
                    try
                    {
                        string branch = Preferences.Get(Pref.BRANCH, "");
                        // string vaid = "2961";

                        //var response = await client.GetAsync("AuthenticateUser?UserName=TNAX5004&Password=TNAX5004");
                        var response = await client.GetAsync("GetVendorList");
                        var docketJson = response.Content.ReadAsStringAsync().Result;


                        VendorMasterResp docketobject = new VendorMasterResp();
                        if (response.StatusCode == System.Net.HttpStatusCode.OK)
                        {
                            if (docketJson != "")
                            {
                                docketobject = JsonConvert.DeserializeObject<VendorMasterResp>(docketJson);
                                if (docketobject.Status.Equals("true"))
                                {
                                    // dockets= JsonConvert.DeserializeObject<DeliveryDoket>(docketJson);

                                    VendorList = docketobject.Vendor;





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
                return new Command(RepairAsset);
            }
        }

        private async void RepairAsset(object obj)
        {
            try
            {
                IsBusy = true;
                IsEnable = true;
                IsVisible = true;
                BTNSUBMITSTATUS = false;

                //.....................................................
                string authorisedby = Preferences.Get(Pref.Emp_Name, "");

                PostAssetRepairRequest dispose = new PostAssetRepairRequest();
                dispose.Cdate = DateTime.Now.ToShortDateString();
                dispose.Company_Id = _details.Company_Id;
                dispose.Asset_id = _details.Asset_id;
                dispose.Asset_name = _details.Asset_name;
                dispose.Description = _details.Description;
                dispose.Location_Code = _details.Location;
                dispose.ReasonFor_Repair = Remarks;
                dispose.Authorized_By = authorisedby;
                dispose.Vendor_Name = SELECTEDVENDOR;
                dispose.ExpectedReturn_Date = SELECTEDDTAE;
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
                    var response = await client.PostAsync(new Uri(ProjectConstants.POST_ASSETREPAIR_API), str);
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
