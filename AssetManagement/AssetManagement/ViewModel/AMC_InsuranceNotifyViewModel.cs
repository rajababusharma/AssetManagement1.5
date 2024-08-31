using AssetManagement.Constants;
using AssetManagement.Model;
using AssetManagement.View;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace AssetManagement.ViewModel
{
    public class AMC_InsuranceNotifyViewModel : BaseViewModel
    {
        public AMC_InsuranceNotifyViewModel()
        {
            IsBusy = false;
            IsEnable = false;
            IsVisible = false;
            ShowMain1 = true;
            ShowMain2 = true;
            GetAMCNotification();
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

        private bool _ShowMain2 = false;
        public bool ShowMain2
        {
            get { return _ShowMain2; }
            set
            {
                _ShowMain2 = value;
                NotifyPropertyChanged("ShowMain2");
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

        private bool _Show_popuplayout2 = false;
        public bool Show_popuplayout2
        {
            get { return _Show_popuplayout2; }
            set
            {
                _Show_popuplayout2 = value;
                NotifyPropertyChanged("Show_popuplayout2");
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

        List<AMCList> _amcList;

        public List<AMCList> AMCList
        {
            get { return _amcList; }

            set
            {
                if (_amcList != value)
                {
                    _amcList = value;
                    // OnPropertyChanged("ObjPaymentList");
                    NotifyPropertyChanged("AMCList");
                }
            }
        }

        List<AMCList> _amcListSearch;

        public List<AMCList> AMCListSearch
        {
            get { return _amcListSearch; }

            set
            {
                if (_amcListSearch != value)
                {
                    _amcListSearch = value;
                    // OnPropertyChanged("ObjPaymentList");
                    NotifyPropertyChanged("AMCListSearch");
                }
            }
        }

        List<InsuranceList> _insList;

        public List<InsuranceList> InsuranceList
        {
            get { return _insList; }

            set
            {
                if (_insList != value)
                {
                    _insList = value;
                    // OnPropertyChanged("ObjPaymentList");
                    NotifyPropertyChanged("InsuranceList");
                }
            }
        }

        List<InsuranceList> _insListSearch;

        public List<InsuranceList> InsuranceListSearch
        {
            get { return _insListSearch; }

            set
            {
                if (_insListSearch != value)
                {
                    _insListSearch = value;
                    // OnPropertyChanged("ObjPaymentList");
                    NotifyPropertyChanged("InsuranceListSearch");
                }
            }
        }


        public async Task GetAMCNotification()
        {
           // int count = 0;
            string branch_id = Preferences.Get(Pref.BRANCH, "");
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



                var response = await client.GetAsync("GetMoveNotifications?ToBranch=" + branch_id);
                var responseJson = response.Content.ReadAsStringAsync().Result;

                AssetMoveDetailResponse stocktake = new AssetMoveDetailResponse();

                List<AssetMoveList> mystocklist = new List<AssetMoveList>();
                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    stocktake = JsonConvert.DeserializeObject<AssetMoveDetailResponse>(responseJson);
                    if (stocktake.Status.Equals("true"))
                    {
                        AMCList = stocktake.AMCLists;
                        InsuranceList = stocktake.insuranceLists;
                        AMCListSearch = stocktake.AMCLists;
                        InsuranceListSearch = stocktake.insuranceLists;


                    }
                    else
                    {
                        //DependencyService.Get<IAudio>().PlayAudioFile(ProjectConstants.audio_alert_fail);
                        // await App.Current.MainPage.DisplayAlert("Exception", stocktake.Msg.ToString(), "Ok");
                       // Objmovenotification = null;
                    }

                }
                else
                {
                    // DependencyService.Get<IAudio>().PlayAudioFile(ProjectConstants.audio_alert_fail);
                    // await App.Current.MainPage.DisplayAlert("Exception", response.ReasonPhrase, "Ok");
                   // Objmovenotification = null;
                }

                IsBusy = false;
                IsEnable = false;
                IsVisible = false;

            }
            catch (Exception excp)
            {
                IsBusy = false;
                IsEnable = false;
                IsVisible = false;
                // Common.SaveLogs(excp.StackTrace);
                //  Objmovenotification = null;
                // ObjStockList = database.GetStockList(branch_id);
                //await App.Current.MainPage.DisplayAlert("Exception", "Request could n, please try again later", "Ok");
                // Crashes.TrackError(excp);

            }
        }

        private string _ASSET_ID;
        public string ASSET_ID
        {
            get { return _ASSET_ID; }
            set
            {
                _ASSET_ID = value;
                NotifyPropertyChanged("ASSET_ID");
            }
        }

        private string _ASSET_NAME;
        public string ASSET_NAME
        {
            get { return _ASSET_NAME; }
            set
            {
                _ASSET_NAME = value;
                NotifyPropertyChanged("ASSET_NAME");
            }
        }

        private string _VENDOR;
        public string VENDOR
        {
            get { return _VENDOR; }
            set
            {
                _VENDOR = value;
                NotifyPropertyChanged("VENDOR");
            }
        }

        private string _AMC_TYPE;
        public string AMC_TYPE
        {
            get { return _AMC_TYPE; }
            set
            {
                _AMC_TYPE = value;
                NotifyPropertyChanged("AMC_TYPE");
            }
        }

        private string _STARTDATE;
        public string STARTDATE
        {
            get { return _STARTDATE; }
            set
            {
                _STARTDATE = value;
                NotifyPropertyChanged("STARTDATE");
            }
        }

        private string _ENDDATE;
        public string ENDDATE
        {
            get { return _ENDDATE; }
            set
            {
                _ENDDATE = value;
                NotifyPropertyChanged("ENDDATE");
            }
        }

        private string _ALERTDATE;
        public string ALERTDATE
        {
            get { return _ALERTDATE; }
            set
            {
                _ALERTDATE = value;
                NotifyPropertyChanged("ALERTDATE");
            }
        }


        private string _DESCRIPTION;
        public string DESCRIPTION
        {
            get { return _DESCRIPTION; }
            set
            {
                _DESCRIPTION = value;
                NotifyPropertyChanged("DESCRIPTION");
            }
        }

        private string _AMC_VALUE;
        public string AMC_VALUE
        {
            get { return _AMC_VALUE; }
            set
            {
                _AMC_VALUE = value;
                NotifyPropertyChanged("AMC_VALUE");
            }
        }
        //................................................
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

        private string _duedate;
        public string Due_Date
        {
            get
            {
                return _duedate;
            }
            set
            {
                _duedate = value;
                NotifyPropertyChanged("Due_Date");
            }
        }

        private string _Premium;
        public string Premium
        {
            get
            {
                return _Premium;
            }
            set
            {
                _Premium = value;
                NotifyPropertyChanged("Premium");
            }
        }

        private string _Policy_Name;
        public string Policy_Name
        {
            get
            {
                return _Policy_Name;
            }
            set
            {
                _Policy_Name = value;
                NotifyPropertyChanged("Policy_Name");
            }
        }

        private string _Alert_Date;
        public string Alert_Date
        {
            get
            {
                return _Alert_Date;
            }
            set
            {
                _Alert_Date = value;
                NotifyPropertyChanged("Alert_Date");
            }
        }


        private string _Policy_No;
        public string Policy_No
        {
            get
            {
                return _Policy_No;
            }
            set
            {
                _Policy_No = value;
                NotifyPropertyChanged("Policy_No");
            }
        }

        private string _InsuranceCompany;
        public string InsuranceCompany
        {
            get
            {
                return _InsuranceCompany;
            }
            set
            {
                _InsuranceCompany = value;
                NotifyPropertyChanged("InsuranceCompany");
            }
        }

        private string _Policy_Date;
        public string Policy_Date
        {
            get
            {
                return _Policy_Date;
            }
            set
            {
                _Policy_Date = value;
                NotifyPropertyChanged("Policy_Date");
            }
        }

       






        public async void SearchAMC()
        {
            bool status = false;
            List<AMCList> dkt = new List<AMCList>();
            if (string.IsNullOrEmpty(ASSETID))
            {
                // await App.Current.MainPage..DisplayAlert("Alert", "Please enter docket number first.", "Ok");
                await App.Current.MainPage.DisplayAlert("Alert", "Please Scan Asset Id first.", "Ok");
            }
            else
            {
                foreach (AMCList amc in AMCListSearch)
                {
                    if (ASSETID.Trim().Equals(amc.Asset_id))
                    {
                        dkt.Add(amc);
                        AMCList = dkt;
                        status = true;
                        break;
                    }

                }
                if (!status)
                {
                    AMCList = null;
                    await App.Current.MainPage.DisplayAlert("Alert", "Asset not found", "Ok");
                }

            }
        }
        public async void SearchIns()
        {
            bool status = false;
            List<InsuranceList> dkt = new List<InsuranceList>();
            if (string.IsNullOrEmpty(ASSETID))
            {
                // await App.Current.MainPage..DisplayAlert("Alert", "Please enter docket number first.", "Ok");
                await App.Current.MainPage.DisplayAlert("Alert", "Please Scan Asset Id first.", "Ok");
            }
            else
            {
                foreach (InsuranceList ins in InsuranceListSearch)
                {
                    if (ASSETID.Trim().Equals(ins.Asset_id))
                    {
                        dkt.Add(ins);
                        InsuranceList = dkt;
                        status = true;
                        break;
                    }

                }
                if (!status)
                {
                    InsuranceList = null;
                    await App.Current.MainPage.DisplayAlert("Alert", "Asset not found", "Ok");
                }

            }
        }

       public Command RENEW_INSURANCE
        {
            get
            {
                return new Command(Renew_Insurance);
            }
        }

        private async void Renew_Insurance(object obj)
        {
            await App.Current.MainPage.Navigation.PushAsync(new CreateInsurance(ASSET_ID, ASSET_NAME, Policy_Date, InsuranceCompany, Policy_No, Alert_Date, Policy_Name, Premium, Due_Date));
        }

       public Command RENEW
        {
            get
            {
                return new Command(Renew_AMC);
            }
        }

        private async void Renew_AMC(object obj)
        {
            await App.Current.MainPage.Navigation.PushModalAsync(new CreateAMC(ASSET_ID,ASSET_NAME,VENDOR, AMC_TYPE,Convert.ToDateTime(STARTDATE), Convert.ToDateTime(ENDDATE), Convert.ToDateTime(ALERTDATE), DESCRIPTION, AMC_VALUE));
        }
    }
}
