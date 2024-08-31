using AssetManagement.Constants;
using AssetManagement.Model;
using Microsoft.AppCenter.Crashes;
using Newtonsoft.Json;
using Plugin.Connectivity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace AssetManagement.ViewModel
{
    public class AssetRepairViewModel : BaseViewModel
    {
        public AssetRepairViewModel()
        {
            IsBusy = false;
            IsEnable = false;
            IsVisible = false;
            ShowMain = true;
            ShowMain1 = true;
            CallPaymentStatusAPI();
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

        private bool _ShowMain1 = false;
        public bool ShowMain1
        {
            get { return _ShowMain1; }
            set
            {
                _ShowMain1 = value;
                NotifyPropertyChanged("ShowMain1");
            }
        }


        private bool _ShowMain = false;
        public bool ShowMain
        {
            get { return _ShowMain; }
            set
            {
                _ShowMain = value;
                NotifyPropertyChanged("ShowMain");
            }
        }

        private bool _Show_popuplayout1 = false;
        public bool Show_popuplayout1
        {
            get { return _Show_popuplayout1; }
            set
            {
                _Show_popuplayout1 = value;
                NotifyPropertyChanged("Show_popuplayout1");
            }
        }


        private bool _Show_popuplayout = false;
        public bool Show_popuplayout
        {
            get { return _Show_popuplayout; }
            set
            {
                _Show_popuplayout = value;
                NotifyPropertyChanged("Show_popuplayout");
            }
        }

        private bool _IsReturned = false;
        public bool IsReturned
        {
            get { return _IsReturned; }
            set
            {
                _IsReturned = value;
                NotifyPropertyChanged("IsReturned");
            }
        }


        List<AssetRepairList> _AllReports;

        public List<AssetRepairList> AllReports
        {
            get { return _AllReports; }

            set
            {
                if (_AllReports != value)
                {
                    _AllReports = value;
                    // OnPropertyChanged("ObjPaymentList");
                    NotifyPropertyChanged("AllReports");
                }
            }
        }

        List<AssetRepairList> _IsReturnReports;

        public List<AssetRepairList> IsReturnReports
        {
            get { return _IsReturnReports; }

            set
            {
                if (_IsReturnReports != value)
                {
                    _IsReturnReports = value;
                    // OnPropertyChanged("ObjPaymentList");
                    NotifyPropertyChanged("IsReturnReports");
                }
            }
        }

        List<AssetRepairList> _IsNotReturnReports;

        public List<AssetRepairList> IsNotReturnReports
        {
            get { return _IsNotReturnReports; }

            set
            {
                if (_IsNotReturnReports != value)
                {
                    _IsNotReturnReports = value;
                    // OnPropertyChanged("ObjPaymentList");
                    NotifyPropertyChanged("IsNotReturnReports");
                }
            }
        }


        List<AssetRepairList> _searchObject;

        public List<AssetRepairList> SEARCHOBJECT
        {
            get { return _searchObject; }

            set
            {
                if (_searchObject != value)
                {
                    _searchObject = value;
                    // OnPropertyChanged("ObjPaymentList");
                    NotifyPropertyChanged("SEARCHOBJECT");
                }
            }
        }

        List<AssetRepairList> _searchObject1;

        public List<AssetRepairList> SEARCHOBJECT1
        {
            get { return _searchObject1; }

            set
            {
                if (_searchObject1 != value)
                {
                    _searchObject1 = value;
                    // OnPropertyChanged("ObjPaymentList");
                    NotifyPropertyChanged("SEARCHOBJECT1");
                }
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
        private string _ASSETID1;
        public string ASSETID1
        {
            get
            {
                return _ASSETID1;
            }
            set
            {
                _ASSETID1 = value;
                NotifyPropertyChanged("ASSETID1");

                //put here your code  
                // SYear = selected_year.Year;
            }
        }

        public async Task CallPaymentStatusAPI()
        {
            if (CrossConnectivity.Current.IsConnected == true)
            {
                IsBusy = true;
                IsEnable = true;
                IsVisible = true;


                var client = new System.Net.Http.HttpClient();
                client.BaseAddress = new Uri(ProjectConstants.GETREPAIRDATA_API);
                try
                {
                    string branch = Preferences.Get(Pref.BRANCH, "");
                    // string vaid = "2961";

                    //var response = await client.GetAsync("AuthenticateUser?UserName=TNAX5004&Password=TNAX5004");
                    var response = await client.GetAsync("GetAssetRepairData");
                    var docketJson = response.Content.ReadAsStringAsync().Result;


                    AssetRepairResponse docketobject = new AssetRepairResponse();
                    if (response.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        if (docketJson != "")
                        {
                            docketobject = JsonConvert.DeserializeObject<AssetRepairResponse>(docketJson);
                            if (docketobject.Status.Equals("true"))
                            {
                                // dockets= JsonConvert.DeserializeObject<DeliveryDoket>(docketJson);

                                AllReports = docketobject.AssetRepairList;




                                if (AllReports.Count < 1)
                                {
                                    await App.Current.MainPage.DisplayAlert("Exception", "No Data Found", "Ok");

                                }
                                else
                                {
                                    int isReturn = 1;
                                    int isNotReturn = 0;

                                    var returned = from AssetRepairList mydata in AllReports where mydata.IsReturned == isReturn select mydata;
                                    var notReturned = from AssetRepairList mydata in AllReports where mydata.IsReturned == isNotReturn select mydata;
                                    List<AssetRepairList> f = new List<AssetRepairList>();
                                    List<AssetRepairList> m = new List<AssetRepairList>();
                                    if (returned.Count<AssetRepairList>() > 0)
                                    {
                                        foreach (AssetRepairList strf in returned)
                                        {
                                            f.Add(strf);
                                        }
                                    }
                                    if (notReturned.Count<AssetRepairList>() > 0)
                                    {
                                        foreach (AssetRepairList strm in notReturned)
                                        {
                                            m.Add(strm);
                                        }
                                    }
                                    IsReturnReports = f;
                                    SEARCHOBJECT1 = f;
                                    IsNotReturnReports = m;
                                    SEARCHOBJECT = m;
                                }
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

                // return placeobject.places;
            }
            else
            {
                await App.Current.MainPage.DisplayAlert("Alert", "No internet, please check internet connectivity.", "Ok");
            }
        }
        public async void SearchAsset()
        {
            bool status = false;
            List<AssetRepairList> dkt = new List<AssetRepairList>();
            if (string.IsNullOrEmpty(ASSETID))
            {
                // await App.Current.MainPage..DisplayAlert("Alert", "Please enter docket number first.", "Ok");
                await App.Current.MainPage.DisplayAlert("Alert", "Please Scan Asset Id first.", "Ok");
            }
            else
            {
                foreach (AssetRepairList docketforpayment in SEARCHOBJECT)
                {
                    if (ASSETID.Trim().Equals(docketforpayment.Asset_id))
                    {
                        dkt.Add(docketforpayment);
                        AllReports = dkt;
                        status = true;
                        break;
                    }

                }
                if (!status)
                {
                    AllReports = null;
                    await App.Current.MainPage.DisplayAlert("Alert", "Asset not found", "Ok");
                }

            }
        }
        public async void SearchAsset1()
        {
            bool status = false;
            List<AssetRepairList> dkt = new List<AssetRepairList>();
            if (string.IsNullOrEmpty(ASSETID))
            {
                // await App.Current.MainPage..DisplayAlert("Alert", "Please enter docket number first.", "Ok");
                await App.Current.MainPage.DisplayAlert("Alert", "Please Scan Asset Id first.", "Ok");
            }
            else
            {
                foreach (AssetRepairList docketforpayment in SEARCHOBJECT1)
                {
                    if (ASSETID.Trim().Equals(docketforpayment.Asset_id))
                    {
                        dkt.Add(docketforpayment);
                        AllReports = dkt;
                        status = true;
                        break;
                    }

                }
                if (!status)
                {
                    AllReports = null;
                    await App.Current.MainPage.DisplayAlert("Alert", "Asset not found", "Ok");
                }

            }
        }

        private string asset_id = "";
        public string ASSET_ID
        {
            get { return asset_id; }
            set
            {
                asset_id = value;
                NotifyPropertyChanged("ASSET_ID");
            }
        }

        private string asset_name = "";
        public string ASSET_NAME
        {
            get { return asset_name; }
            set
            {
                asset_name = value;
                NotifyPropertyChanged("ASSET_NAME");
            }
        }

        private string reason = "";
        public string REASON
        {
            get { return reason; }
            set
            {
                reason = value;
                NotifyPropertyChanged("REASON");
            }
        }

        private string returndate = "";
        public string RETURNDATE
        {
            get { return returndate; }
            set
            {
                returndate = value;
                NotifyPropertyChanged("RETURNDATE");
            }
        }

        private string expectedreturndate = "";
        public string EXPECTEDRETURNDATE
        {
            get { return expectedreturndate; }
            set
            {
                expectedreturndate = value;
                NotifyPropertyChanged("EXPECTEDRETURNDATE");
            }
        }

        private string vendor_name = "";
        public string VENDOR_NAME
        {
            get { return vendor_name; }
            set
            {
                vendor_name = value;
                NotifyPropertyChanged("VENDOR_NAME");
            }
        }

        private string repair_charge = "";
        public string REPAIR_CHARGE
        {
            get { return repair_charge; }
            set
            {
                repair_charge = value;
                NotifyPropertyChanged("REPAIR_CHARGE");
            }
        }

        private string delay_reason = "";
        public string DELAY_REASON
        {
            get { return delay_reason; }
            set
            {
                delay_reason = value;
                NotifyPropertyChanged("DELAY_REASON");
            }
        }

        private string cdate = "";
        public string CDATE
        {
            get { return cdate; }
            set
            {
                cdate = value;
                NotifyPropertyChanged("CDATE");
            }
        }

       public Command SAVE_ISRETURN
        {
            get
            {
               return new Command(SUBMIT_ISRETURN);
            }
        }

        private async void SUBMIT_ISRETURN(object obj)
        {
            if (CrossConnectivity.Current.IsConnected)  // checking internet connection
            {
               await Update_IsReturn();
            }
            else
            {
                // DependencyService.Get<IAudio>().PlayAudioFile(ProjectConstants.audio_alert_fail);
                await App.Current.MainPage.DisplayAlert("Alert", "No internet connection, Please try again after connecting device to internet.", "OK");
                // Preferences.Set(ProjectConstants.PRELOADING_OFFLINEDATA, json);
                // Deleting Data--- un comment late

                // ObjDocketList = database.GetPreLoading_UnloadingData();
            }
        }

        public async Task Update_IsReturn()
        {
            // int count = 0;
           // string branch_id = Preferences.Get(Pref.BRANCH, "");
            string assetid = ASSET_ID;
            // string branch_id = "10th Floor";
            try
            {
                IsBusy = true;
                IsEnable = true;
                IsVisible = true;

                /* string ctype = "Unloading";*/
                // string ctype = Preferences.Get(ProjectConstants.CTYPE, "");


                var client = new System.Net.Http.HttpClient();
                //  client.BaseAddress = new Uri("http://114.143.156.30/");
                client.BaseAddress = new Uri(ProjectConstants.GETREPAIRDATA_API);



                var response = await client.GetAsync("IsAssetReturned?AssetId=" + assetid);
                var responseJson = response.Content.ReadAsStringAsync().Result;

                PostResponseForAll stocktake = new PostResponseForAll();


                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    stocktake = JsonConvert.DeserializeObject<PostResponseForAll>(responseJson);
                    if (stocktake.Status.Equals("true"))
                    {
                        await App.Current.MainPage.DisplayAlert("Alert", "Data submitted successfully.", "Ok");
                        ShowMain = true;
                        Show_popuplayout = false;
                       await CallPaymentStatusAPI();
                        IsBusy = false;
                        IsEnable = false;
                        IsVisible = false;
                    }
                    else
                    {
                        IsBusy = false;
                        IsEnable = false;
                        IsVisible = false;
                        //DependencyService.Get<IAudio>().PlayAudioFile(ProjectConstants.audio_alert_fail);
                        await App.Current.MainPage.DisplayAlert("Exception", stocktake.Msg.ToString(), "Ok");
                       // Objmovenotification = null;
                    }

                }
                else
                {
                    IsBusy = false;
                    IsEnable = false;
                    IsVisible = false;
                    // DependencyService.Get<IAudio>().PlayAudioFile(ProjectConstants.audio_alert_fail);
                    await App.Current.MainPage.DisplayAlert("Exception", response.ReasonPhrase, "Ok");
                   // Objmovenotification = null;
                }



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
    }
}
