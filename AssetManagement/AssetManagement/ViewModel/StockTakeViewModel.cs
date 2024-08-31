using AssetManagement.Constants;
using AssetManagement.DB;
using AssetManagement.Model;
using Microsoft.AppCenter.Crashes;
using Newtonsoft.Json;
using Plugin.Connectivity;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace AssetManagement.ViewModel
{
    public class StockTakeViewModel : BaseViewModel
    {
        DatabaseController database;
        public StockTakeViewModel()
        {
            IsBusy = false;
            IsEnable = false;
            IsVisible = false;
            database = new DatabaseController();
            GetStockTakeData();
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

        List<STockTallyDetails> _objstockList;

        public List<STockTallyDetails> ObjStockList
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

        private int scanned_stock;
        public int SCANNED_STOCK
        {
            get
            {
                return scanned_stock;
            }
            set
            {
                scanned_stock = value;
                NotifyPropertyChanged("SCANNED_STOCK");

                //put here your code  
                // SYear = selected_year.Year;
            }
        }

        private int total_stock;
        public int TOTAL_STOCK
        {
            get
            {
                return total_stock;
            }
            set
            {
                total_stock = value;
                NotifyPropertyChanged("TOTAL_STOCK");

                //put here your code  
                // SYear = selected_year.Year;
            }
        }

        private string assetid = "";

        

        public string ASSETID
        {
            get
            { return assetid; }
            set
            {
                assetid = value;
                NotifyPropertyChanged("ASSETID");
            }
        }
        public Command Submit_StockTake
        {
            get
            {

                return new Command(Submit);
            }
        }

        private async void Submit(object obj)
        {
            string branch = Preferences.Get(Pref.BRANCH, "");
          
            try
            {
                IsBusy = true;
                IsEnable = true;
                IsVisible = true;
                BTNSUBMITSTATUS = false;

                //.....................................................

               
                List<STockTallyDetails> stockLists = new List<STockTallyDetails>();
                database.Update_Username();
                stockLists =database.GetStockList(branch);
                //...........................
                var client = new System.Net.Http.HttpClient();
                // client.BaseAddress = new Uri("http://114.143.156.30/");
                client.BaseAddress = new Uri(ProjectConstants.BASE_URL);
                var json = JsonConvert.SerializeObject(stockLists);
                if (CrossConnectivity.Current.IsConnected)  // checking internet connection
                {

                    StringContent str = new StringContent(json, Encoding.UTF8, "application/json");
                    var response = await client.PostAsync(new Uri(ProjectConstants.POSTSTOCKTALLYDATA_API), str);
                    var responseJson = response.Content.ReadAsStringAsync().Result;
                    PostResponseForAll stocktakeresponse = new PostResponseForAll();

                    if (response.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        stocktakeresponse = JsonConvert.DeserializeObject<PostResponseForAll>(responseJson);

                        if (stocktakeresponse.Status.Equals("true"))
                        {
                            await App.Current.MainPage.DisplayAlert("Alert", "Data submitted successfully.", "Ok");
                            database.DeleteAll_from_Stocks(branch);
                            ObjStockList = database.GetStockList(branch);

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

        public async void GetStockTakeData()
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
            try
            {
                IsBusy = true;
                IsEnable = true;
                IsVisible = true;

                /* string ctype = "Unloading";*/
                // string ctype = Preferences.Get(ProjectConstants.CTYPE, "");


                var client = new System.Net.Http.HttpClient();
                //  client.BaseAddress = new Uri("http://114.143.156.30/");
                client.BaseAddress = new Uri(ProjectConstants.GETSTOCKTALLYDATA_API);



                var response = await client.GetAsync("GetStockData?Branch=" + branch_id);
                var responseJson = response.Content.ReadAsStringAsync().Result;

                StockTallyResponse stocktake = new StockTallyResponse();

                List<STockTallyDetails> mystocklist = new List<STockTallyDetails>();
                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    stocktake = JsonConvert.DeserializeObject<StockTallyResponse>(responseJson);
                    if (stocktake.Status.Equals("true"))
                    {

                        List<STockTallyDetails> existingStockList = database.GetStockList(branch_id);

                        // List<DocketDetList_Actualloading> NewDocket = loadingList1.Except(existingDocketList).ToList<DocketDetList_Actualloading>();
                        List<STockTallyDetails> NewStock = stocktake.STockTallyDetails.Where(item => existingStockList.All(f => !item.Asset_id.Equals(f.Asset_id))).ToList();
                        // database.Insert_DocketDetList_Actualloading_RefreshDocket(NewDocket);
                        database.Insert_StockData(NewStock);
                        await App.Current.MainPage.DisplayAlert("Alert", "Data synced, now you can start scanning assets.", "Ok");
                        ObjStockList = database.GetStockList(branch_id);
                        // TOTAL_ARTICLE = database.GetAll_StockTakeList().Count.ToString();
                        int totalArticle = ObjStockList.Count;
                        int scanned_item = 0;
                        foreach (var articlecount in ObjStockList)
                        {
                            if (articlecount.Status == "True")
                            {
                                scanned_item = scanned_item + 1;
                            }
                           
                            // totalScanned_Article = totalScanned_Article + count.Scanned_Article;
                        }
                        TOTAL_STOCK = totalArticle;
                        SCANNED_STOCK = scanned_item;
                       
                    }
                    else
                    {
                        //DependencyService.Get<IAudio>().PlayAudioFile(ProjectConstants.audio_alert_fail);
                        await App.Current.MainPage.DisplayAlert("Exception", stocktake.Msg.ToString(), "Ok");
                        ObjStockList = database.GetStockList(branch_id);
                    }

                }
                else
                {
                    // DependencyService.Get<IAudio>().PlayAudioFile(ProjectConstants.audio_alert_fail);
                    await App.Current.MainPage.DisplayAlert("Exception", response.ReasonPhrase, "Ok");
                    ObjStockList = database.GetStockList(branch_id);
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

                ObjStockList = database.GetStockList(branch_id);
                //await App.Current.MainPage.DisplayAlert("Exception", "Request could n, please try again later", "Ok");

                Crashes.TrackError(excp);
            }
        }
    }
}
