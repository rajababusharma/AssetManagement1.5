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

namespace AssetManagement.ViewModel
{
    public class FoundMissingReportViewModel: BaseViewModel
    {
        public FoundMissingReportViewModel()
        {
            IsBusy = false;
            IsEnable = false;
            IsVisible = false;
            CallPaymentStatusAPI();
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

        List<STockTallyDetails> _AllReports;

        public List<STockTallyDetails> AllReports
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

        List<STockTallyDetails> _FoundReports;

        public List<STockTallyDetails> FoundReports
        {
            get { return _FoundReports; }

            set
            {
                if (_FoundReports != value)
                {
                    _FoundReports = value;
                    // OnPropertyChanged("ObjPaymentList");
                    NotifyPropertyChanged("FoundReports");
                }
            }
        }

        List<STockTallyDetails> _MissingReports;

        public List<STockTallyDetails> MissingReports
        {
            get { return _MissingReports; }

            set
            {
                if (_MissingReports != value)
                {
                    _MissingReports = value;
                    // OnPropertyChanged("ObjPaymentList");
                    NotifyPropertyChanged("MissingReports");
                }
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
                client.BaseAddress = new Uri(ProjectConstants.GETSCANNINGREPORTS_API);
                try
                {
                    string branch = Preferences.Get(Pref.BRANCH, "");
                    int userrole = Preferences.Get(Pref.User_Role, 2);
                    // string vaid = "2961";

                    //var response = await client.GetAsync("AuthenticateUser?UserName=TNAX5004&Password=TNAX5004");
                    // var response = await client.GetAsync("GetStocksFoundMissingData?Branch=" + branch);
                    var response = await client.GetAsync("GetStocksFoundMissingData?Branch=" + branch + "&userrole=" + userrole);
                    var docketJson = response.Content.ReadAsStringAsync().Result;


                    StockTallyResponse docketobject = new StockTallyResponse();
                    if (response.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        if (docketJson != "")
                        {
                            docketobject = JsonConvert.DeserializeObject<StockTallyResponse>(docketJson);
                            if (docketobject.Status.Equals("true"))
                            {
                                // dockets= JsonConvert.DeserializeObject<DeliveryDoket>(docketJson);

                                AllReports = docketobject.STockTallyDetails;




                                if (AllReports.Count < 1)
                                {
                                    await App.Current.MainPage.DisplayAlert("Exception", "No Data Found", "Ok");

                                }
                                else
                                {
                                    string status1 = "Found";
                                    string status2 = "Missing";

                                    var Found = from STockTallyDetails mydata in AllReports where mydata.Status == status1 select mydata;
                                    var Missing = from STockTallyDetails mydata in AllReports where mydata.Status == status2 select mydata;
                                    List<STockTallyDetails> f = new List<STockTallyDetails>();
                                    List<STockTallyDetails> m = new List<STockTallyDetails>();
                                    if (Found.Count<STockTallyDetails>() > 0)
                                    {
                                        foreach (STockTallyDetails strf in Found)
                                        {
                                            f.Add(strf);
                                        }
                                    }
                                    if (Missing.Count<STockTallyDetails>() > 0)
                                    {
                                        foreach (STockTallyDetails strm in Missing)
                                        {
                                            m.Add(strm);
                                        }
                                    }
                                    FoundReports = f;
                                    MissingReports = m;
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
    }
}
