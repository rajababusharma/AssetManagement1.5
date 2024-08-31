using AssetManagement.Constants;
using AssetManagement.Model;
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
    public class CreateInsuranceViewModel:BaseViewModel
    {
        public CreateInsuranceViewModel()
        {
            IsBusy = false;
            IsEnable = false;
            IsVisible = false;


            List<string> paymentModeList = new List<string>(new string[] { "Select Payment Mode", "Cash", "Credit Card", "Online" });

            PAYMENTMODELIST = paymentModeList;

            DateTime dt = DateTime.Now.Date;
            START_DATE = dt;
            ALERT_DATE = dt;
            DUE_DATE = dt;
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

        private int selectedmode_index = 0;
        public int SELECTEDMODE_INDEX
        {
            get { return selectedmode_index; }
            set
            {
                selectedmode_index = value;
                NotifyPropertyChanged("SELECTEDMODE_INDEX");
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

        List<String> __paymentmodelist = new List<string>();

        public List<String> PAYMENTMODELIST
        {
            get { return __paymentmodelist; }

            set
            {
                if (__paymentmodelist != value)
                {
                    __paymentmodelist = value;
                    // OnPropertyChanged("ObjPaymentList");
                    NotifyPropertyChanged("PAYMENTMODELIST");
                }
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

        private string _ASSETID;
        public string ASSETID
        {
            get { return _ASSETID; }
            set
            {
                _ASSETID = value;
                NotifyPropertyChanged("ASSETID");
            }
        }



        private string _Asset_name;
        public string ASSETNAME
        {
            get { return _Asset_name; }
            set
            {
                _Asset_name = value;
                NotifyPropertyChanged("ASSETNAME");
            }
        }
        private string _paymentmode;
        public string PAYMENTMODE
        {
            get { return _paymentmode; }
            set
            {
                _paymentmode = value;
                NotifyPropertyChanged("PAYMENTMODE");
            }
        }
        private DateTime _start_date;
        public DateTime START_DATE
        {
            get { return _start_date; }
            set
            {
                _start_date = value;
                NotifyPropertyChanged("START_DATE");
            }
        }
        private DateTime _alert_date;
        public DateTime ALERT_DATE
        {
            get { return _alert_date; }
            set
            {
                _alert_date = value;
                NotifyPropertyChanged("ALERT_DATE");
            }
        }

        private DateTime _due_date;
        public DateTime DUE_DATE
        {
            get { return _due_date; }
            set
            {
                _due_date = value;
                NotifyPropertyChanged("DUE_DATE");
            }
        }

        private string _companyname;
        public string INSURANCE_COMPANYNAME
        {
            get { return _companyname; }
            set
            {
                _companyname = value;
                NotifyPropertyChanged("INSURANCE_COMPANYNAME");
            }
        }

        private string _policynumber;
        public string POLICYNUMBER
        {
            get { return _policynumber; }
            set
            {
                _policynumber = value;
                NotifyPropertyChanged("POLICYNUMBER");
            }
        }

        private string _policyname;
        public string POLICYNAME
        {
            get { return _policyname; }
            set
            {
                _policyname = value;
                NotifyPropertyChanged("POLICYNAME");
            }
        }

        private string _premium;
        public string PREMIUM
        {
            get { return _premium; }
            set
            {
                _premium = value;
                NotifyPropertyChanged("PREMIUM");
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
                string branch_id = Preferences.Get(Pref.BRANCH, "");

                InsuranceList inc = new InsuranceList();

                inc.Company_Id = company_id;
                inc.Asset_id = ASSETID;
                inc.Asset_Name = ASSETNAME;
                inc.Branch = branch_id;
                inc.Policy_Date = START_DATE;
                inc.InsuranceCompany_Name = INSURANCE_COMPANYNAME;
                inc.Policy_No = POLICYNUMBER;
                inc.Alert_Date = ALERT_DATE;
                inc.Policy_Name = POLICYNAME;
                inc.Premium = PREMIUM;
                inc.Due_Date = DUE_DATE;
                inc.ModeOFPayment = PAYMENTMODE;
                inc.Image1 = IMAGE1_BASE64;

                //...........................
                var client = new System.Net.Http.HttpClient();
                // client.BaseAddress = new Uri("http://114.143.156.30/");
                client.BaseAddress = new Uri(ProjectConstants.BASE_URL);
                var json = JsonConvert.SerializeObject(inc);
                if (CrossConnectivity.Current.IsConnected)  // checking internet connection
                {

                    StringContent str = new StringContent(json, Encoding.UTF8, "application/json");
                    var response = await client.PostAsync(new Uri(ProjectConstants.CREATEINSURANCE_API), str);
                    var responseJson = response.Content.ReadAsStringAsync().Result;
                    PostResponseForAll stocktakeresponse = new PostResponseForAll();

                    if (response.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        stocktakeresponse = JsonConvert.DeserializeObject<PostResponseForAll>(responseJson);

                        if (stocktakeresponse.Status.Equals("true"))
                        {
                            await App.Current.MainPage.DisplayAlert("Alert", "Insurance details submitted successfully.", "Ok");

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


            }
            IsBusy = false;
            IsEnable = false;
            IsVisible = false;
            BTNSUBMITSTATUS = true;
        }

        public async void GetAssetsInfo()
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
            string assetid = ASSETID;
            try
            {
                IsBusy = true;
                IsEnable = true;
                IsVisible = true;

                /* string ctype = "Unloading";*/
                // string ctype = Preferences.Get(ProjectConstants.CTYPE, "");


                var client = new System.Net.Http.HttpClient();
                //  client.BaseAddress = new Uri("http://114.143.156.30/");
                client.BaseAddress = new Uri(ProjectConstants.GETASSETINFO_API);



                var response = await client.GetAsync("GetAssets?assetid=" + assetid + "&branch=" + branch_id);

                var responseJson = response.Content.ReadAsStringAsync().Result;

                StockTallyResponse stocktake = new StockTallyResponse();

                List<STockTallyDetails> mystocklist = new List<STockTallyDetails>();
                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    stocktake = JsonConvert.DeserializeObject<StockTallyResponse>(responseJson);
                    if (stocktake.Status.Equals("true"))
                    {
                        ASSETNAME = stocktake.STockTallyDetails[0].Asset_name;

                        // int totalArticle = ObjStockList.Count;

                        // TOTAL_STOCK = totalArticle;


                    }
                    else
                    {
                        //DependencyService.Get<IAudio>().PlayAudioFile(ProjectConstants.audio_alert_fail);
                        await App.Current.MainPage.DisplayAlert("Exception", stocktake.Msg.ToString(), "Ok");
                        // ObjStockList = null;
                    }

                }
                else
                {
                    // DependencyService.Get<IAudio>().PlayAudioFile(ProjectConstants.audio_alert_fail);
                    await App.Current.MainPage.DisplayAlert("Exception", response.ReasonPhrase, "Ok");
                    // ObjStockList = null;
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

                // Crashes.TrackError(excp);
            }
        }
    }
}
