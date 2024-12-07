using AssetManagement.Constants;
using AssetManagement.Model;
using Microsoft.AppCenter.Crashes;
using Newtonsoft.Json;
using Plugin.Connectivity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using static AssetManagement.View.MasterDetailPage1Master;

namespace AssetManagement.ViewModel
{
    public class AssetMoveViewModel : BaseViewModel
    {
        public AssetMoveViewModel()
        {
            IsBusy = false;
            IsEnable = false;
            IsVisible = false;
            ShowMain = true;

            GetAllAssetsData();
            GetMoveNotification();
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


        private bool _isstatus = false;
        public bool ISSTATUS
        {
            get { return _isstatus; }
            set
            {
                _isstatus = value;
                NotifyPropertyChanged("ISSTATUS");
            }
        }

        private bool _issave = false;
        public bool IsSAVE
        {
            get { return _issave; }
            set
            {
                _issave = value;
                NotifyPropertyChanged("IsSAVE");
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

        List<AssetMoveList> _objmovenotification;

        public List<AssetMoveList> Objmovenotification
        {
            get { return _objmovenotification; }

            set
            {
                if (_objmovenotification != value)
                {
                    _objmovenotification = value;
                    // OnPropertyChanged("ObjPaymentList");
                    NotifyPropertyChanged("Objmovenotification");
                }
            }
        }

        List<AssetMoveList> _objstockList;

        public List<AssetMoveList> ObjStockList
        {
            get { return _objstockList; }

            set
            {
                if (_objstockList != value)
                {
                    _objstockList = value;
                    // OnPropertyChanged("ObjPaymentList");
                    NotifyPropertyChanged("ObjStockList");
                }
            }
        }

        List<AssetMoveList> _searchObject;

        public List<AssetMoveList> SEARCHOBJECT
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

        List<AssetMoveList> _searchMoveObject;

        public List<AssetMoveList> SEARCHMOVEOBJECT
        {
            get { return _searchMoveObject; }

            set
            {
                if (_searchMoveObject != value)
                {
                    _searchMoveObject = value;
                    // OnPropertyChanged("ObjPaymentList");
                    NotifyPropertyChanged("SEARCHMOVEOBJECT");
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

        private string _FROMLOCATION;
        public string FROMLOCATION
        {
            get
            {
                return _FROMLOCATION;
            }
            set
            {
                _FROMLOCATION = value;
                NotifyPropertyChanged("FROMLOCATION");

                //put here your code  
                // SYear = selected_year.Year;
            }
        }

        private string _TOLOCATION;
        public string TOLOCATION
        {
            get
            {
                return _TOLOCATION;
            }
            set
            {
                _TOLOCATION = value;
                NotifyPropertyChanged("TOLOCATION");

                //put here your code  
                // SYear = selected_year.Year;
            }
        }


        private string _FROMBRANCH;
        public string FROMBRANCH
        {
            get
            {
                return _FROMBRANCH;
            }
            set
            {
                _FROMBRANCH = value;
                NotifyPropertyChanged("FROMBRANCH");

                //put here your code  
                // SYear = selected_year.Year;
            }
        }

        private string _TOBRANCH;
        public string TOBRANCH
        {
            get
            {
                return _TOBRANCH;
            }
            set
            {
                _TOBRANCH = value;
                NotifyPropertyChanged("TOBRANCH");

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

        private string _movedate;
        public string MOVEDATE
        {
            get
            {
                return _movedate;
            }
            set
            {
                _movedate = value;
                NotifyPropertyChanged("MOVEDATE");

            }
        }

        private bool _movestatus;
        public bool STATUS
        {
            get
            {
                return _movestatus;
            }
            set
            {
                _movestatus = value;
                NotifyPropertyChanged("STATUS");

            }
        }

        public async void GetAllAssetsData()
        {
            //null or empty field validation, check weather email and password is null or empty  

            if (CrossConnectivity.Current.IsConnected)
            {

                await GetData();


                // Task.Delay(10000).Wait();

            }
            else
            {
                // DependencyService.Get<IAudio>().PlayAudioFile(ProjectConstants.audio_alert_fail);
                await App.Current.MainPage.DisplayAlert("Alert", "No internet connection, please check and try again later.", "OK");

            }
            // App.Current.MainPage.DisplayAlert("Empty Values", "welcome", "OK");

        }

        public async Task GetData()
        {
            string branch_id = Preferences.Get(Pref.BRANCH, "");
            int userrole = Preferences.Get(Pref.User_Role, 2);
            try
            {
                IsBusy = true;
                IsEnable = true;
                IsVisible = true;

                /* string ctype = "Unloading";*/
                // string ctype = Preferences.Get(ProjectConstants.CTYPE, "");


                var client = new System.Net.Http.HttpClient();
                //  client.BaseAddress = new Uri("http://114.143.156.30/");
                client.BaseAddress = new Uri(ProjectConstants.GETMOVEDATA_API);



               // var response = await client.GetAsync("GetMoveData?Branch=" + branch_id);
                var response = await client.GetAsync("GetMoveData?Branch=" + branch_id + "&userrole=" + userrole);
                var responseJson = response.Content.ReadAsStringAsync().Result;

                AssetMoveDetailResponse stocktake = new AssetMoveDetailResponse();

                List<AssetMoveList> mystocklist = new List<AssetMoveList>();
                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    stocktake = JsonConvert.DeserializeObject<AssetMoveDetailResponse>(responseJson);
                    if (stocktake.Status.Equals("true"))
                    {
                        ObjStockList = stocktake.AssetMoveList;
                        SEARCHOBJECT = stocktake.AssetMoveList;
                        // int totalArticle = ObjStockList.Count;

                        // TOTAL_STOCK = totalArticle;


                    }
                    else
                    {
                        //DependencyService.Get<IAudio>().PlayAudioFile(ProjectConstants.audio_alert_fail);
                        await App.Current.MainPage.DisplayAlert("Exception", stocktake.Msg.ToString(), "Ok");
                        ObjStockList = null;
                    }

                }
                else
                {
                    // DependencyService.Get<IAudio>().PlayAudioFile(ProjectConstants.audio_alert_fail);
                    await App.Current.MainPage.DisplayAlert("Exception", response.ReasonPhrase, "Ok");
                    ObjStockList = null;
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

                // ObjStockList = database.GetStockList(branch_id);
                //await App.Current.MainPage.DisplayAlert("Exception", "Request could n, please try again later", "Ok");
                Crashes.TrackError(excp);

            }
        }
        public async void SearchAsset()
        {
            bool status = false;
            List<AssetMoveList> dkt = new List<AssetMoveList>();
            if (string.IsNullOrEmpty(ASSETID))
            {
                // await App.Current.MainPage..DisplayAlert("Alert", "Please enter docket number first.", "Ok");
                await App.Current.MainPage.DisplayAlert("Alert", "Please Scan Asset Id first.", "Ok");
            }
            else
            {
                foreach (AssetMoveList docketforpayment in SEARCHOBJECT)
                {
                    if (ASSETID.Trim().Equals(docketforpayment.Asset_id))
                    {
                        dkt.Add(docketforpayment);
                        ObjStockList = dkt;
                        status = true;
                        break;
                    }

                }
                if (!status)
                {
                    ObjStockList = null;
                    await App.Current.MainPage.DisplayAlert("Alert", "Asset not found", "Ok");
                }

            }
        }

        public async void SearchMovedAsset()
        {
            bool status = false;
            List<AssetMoveList> dkt = new List<AssetMoveList>();
            if (string.IsNullOrEmpty(ASSETID))
            {
                // await App.Current.MainPage..DisplayAlert("Alert", "Please enter docket number first.", "Ok");
                await App.Current.MainPage.DisplayAlert("Alert", "Please Scan Asset Id first.", "Ok");
            }
            else
            {
                if (SEARCHMOVEOBJECT.Count != 0)
                {
                    foreach (AssetMoveList docketforpayment in SEARCHMOVEOBJECT)
                    {
                        if (ASSETID.Trim().Equals(docketforpayment.Asset_id))
                        {
                            dkt.Add(docketforpayment);
                            ObjStockList = dkt;
                            status = true;
                            break;
                        }

                    }
                    if (!status)
                    {
                        ObjStockList = null;
                        await App.Current.MainPage.DisplayAlert("Alert", "Asset not found", "Ok");
                    }
                }
               

            }
        }

        public async Task GetMoveNotification()
        {
            int count = 0;
             string branch_id = Preferences.Get(Pref.BRANCH, "");
           // string branch_id = "10th Floor";
            try
            {


                /* string ctype = "Unloading";*/
                // string ctype = Preferences.Get(ProjectConstants.CTYPE, "");


                var client = new System.Net.Http.HttpClient();
                //  client.BaseAddress = new Uri("http://114.143.156.30/");
                client.BaseAddress = new Uri(ProjectConstants.GETMOVEDATA_API);



                var response = await client.GetAsync("GetMoveNotifications?ToBranch=" + branch_id);
                var responseJson = response.Content.ReadAsStringAsync().Result;

                AssetMoveDetailResponse stocktake = new AssetMoveDetailResponse();

                List<AssetMoveList> mystocklist = new List<AssetMoveList>();
                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    stocktake = JsonConvert.DeserializeObject<AssetMoveDetailResponse>(responseJson);
                    if (stocktake.Status.Equals("true"))
                    {
                        Objmovenotification = stocktake.AssetMoveList;
                        SEARCHMOVEOBJECT = stocktake.AssetMoveList;
                        // int totalArticle = ObjStockList.Count;

                        // TOTAL_STOCK = totalArticle;


                    }
                    else
                    {
                        //DependencyService.Get<IAudio>().PlayAudioFile(ProjectConstants.audio_alert_fail);
                       // await App.Current.MainPage.DisplayAlert("Exception", stocktake.Msg.ToString(), "Ok");
                        Objmovenotification = null;
                    }

                }
                else
                {
                    // DependencyService.Get<IAudio>().PlayAudioFile(ProjectConstants.audio_alert_fail);
                   // await App.Current.MainPage.DisplayAlert("Exception", response.ReasonPhrase, "Ok");
                    Objmovenotification = null;
                }



            }
            catch (Exception excp)
            {
                // Common.SaveLogs(excp.StackTrace);
                Objmovenotification = null;
                // ObjStockList = database.GetStockList(branch_id);
                //await App.Current.MainPage.DisplayAlert("Exception", "Request could n, please try again later", "Ok");
                Crashes.TrackError(excp);

            }
        }
       
        public async Task UpdateMoveNotification()
        {
           // int count = 0;
            string branch_id = Preferences.Get(Pref.BRANCH, "");
            string assetid = ASSET_ID;
           
          
           string location = TOLOCATION;
          
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
                client.BaseAddress = new Uri(ProjectConstants.GETMOVEDATA_API);



                var response = await client.GetAsync("UpdateMoveAsset?ToBranch=" + branch_id + "&AssetId="+ assetid + "&ToLocation=" + location);
                var responseJson = response.Content.ReadAsStringAsync().Result;

                PostResponseForAll stocktake = new PostResponseForAll();

              
                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    stocktake = JsonConvert.DeserializeObject<PostResponseForAll>(responseJson);
                    if (stocktake.Status.Equals("true"))
                    {
                        await App.Current.MainPage.DisplayAlert("Alert", "Request has been submitted successfully.", "Ok");
                        ShowMain = true;
                        Show_popuplayout = false;
                       await GetMoveNotification();
                      
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
                IsBusy = false;
                IsEnable = false;
                IsVisible = false;
                Crashes.TrackError(excp);
            }
        }

    }
}
