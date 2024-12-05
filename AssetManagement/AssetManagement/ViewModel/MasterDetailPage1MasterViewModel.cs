using AssetManagement.Constants;
using AssetManagement.Model;
using AssetManagement.View;
using Microsoft.AppCenter.Crashes;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace AssetManagement.ViewModel
{
    public class MasterDetailPage1MasterViewModel:BaseViewModel
    {
        public ObservableCollection<MasterDetailPage1MasterMenuItem> MenuItems { get; set; }
       
      
       
        public MasterDetailPage1MasterViewModel()
        {
            USERNAME = Preferences.Get(Pref.Emp_Name, "");
            BRANCH = Preferences.Get(Pref.BRANCH, "");
            string imagebase64 = Preferences.Get(Pref.PIC, "");
            if (!imagebase64.Equals(""))
            {
                Image1 = Xamarin.Forms.ImageSource.FromStream(() => new MemoryStream(Convert.FromBase64String(imagebase64)));
            }
            
          
            // NOTIFICATIONTEXT = notcount.ToString();
            // ImageSource.FromResource("Images.Assets.Images.NormalProfile.png");
            MenuItems = new ObservableCollection<MasterDetailPage1MasterMenuItem>(new[]
            {
                  
                     new MasterDetailPage1MasterMenuItem { Id = 0, Title = "Create New Users", TargetType=typeof(Create_Users)},
                       new MasterDetailPage1MasterMenuItem { Id = 1, Title = "Rights Management", TargetType=typeof(RightsManagement)},
                        new MasterDetailPage1MasterMenuItem { Id = 2, Title = "Asset Management", TargetType=typeof(ManagementAssets)},
                           new MasterDetailPage1MasterMenuItem { Id = 3, Title = "Stock Inventory",TargetType=typeof(StockTally)},
                    new MasterDetailPage1MasterMenuItem { Id = 4, Title = "Create Masters", TargetType=typeof(Masters)},
                      new MasterDetailPage1MasterMenuItem { Id = 5, Title = "Create Assets", TargetType=typeof(CreateAssets)},
                        new MasterDetailPage1MasterMenuItem { Id = 6, Title = "Search Asset", TargetType=typeof(UpdateAsset)},
                      new MasterDetailPage1MasterMenuItem { Id = 7, Title = "Reports", TargetType=typeof(Reports)},
                       new MasterDetailPage1MasterMenuItem { Id = 8, Title = "Assets Document", TargetType=typeof(DownloadDocuments)},
                     new MasterDetailPage1MasterMenuItem { Id = 9, Title = "About Us", TargetType=typeof(About_Us)},
                });

            GetMoveNotification();
        }

        private Xamarin.Forms.ImageSource image1 = (FileImageSource)ImageSource.FromFile("person.png");
        public Xamarin.Forms.ImageSource Image1
        {
            get { return image1; }
            set
            {
                image1 = value;
                NotifyPropertyChanged("Image1");
            }
        }

        private string _NOTIFICATIONTEXT;
        public string NOTIFICATIONTEXT
        {
            get { return _NOTIFICATIONTEXT; }
            set
            {
                _NOTIFICATIONTEXT = value;
                NotifyPropertyChanged("NOTIFICATIONTEXT");
            }
        }

        private string _amcnotification;
        public string AMCNOTIFICATIONTEXT
        {
            get { return _amcnotification; }
            set
            {
                _amcnotification = value;
                NotifyPropertyChanged("AMCNOTIFICATIONTEXT");
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

        public async Task GetMoveNotification()
        {
            int movecount = 0;
            int amc_count = 0;
            int ins_count = 0;
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
                        if (stocktake.AssetMoveList!=null)
                        {
                            movecount = stocktake.AssetMoveList.Count;
                        }
                        if (stocktake.AMCLists != null)
                        {
                            amc_count = stocktake.AMCLists.Count;
                        }
                        if (stocktake.insuranceLists != null)
                        {
                            ins_count = stocktake.insuranceLists.Count;
                        }
                       
                       
                       
                        if (movecount > 0)
                        {


                            NOTIFICATIONTEXT = movecount.ToString();
                           
                            // for local notification
                            // DependencyService.Get<INotification>().CreateNotification("Welcome to local notification", "You have an asset move notification, please click to view.");
                        }
                        if (amc_count > 0 || ins_count>0)
                        {
                            AMCNOTIFICATIONTEXT = (amc_count +ins_count).ToString();
                        }
                    }
                    else
                    {
                        //DependencyService.Get<IAudio>().PlayAudioFile(ProjectConstants.audio_alert_fail);
                        // await App.Current.MainPage.DisplayAlert("Exception", stocktake.Msg.ToString(), "Ok");
                        // Preferences.Set(Pref.MOVENOTIFICATION, count);
                    }

                }
                else
                {
                    // DependencyService.Get<IAudio>().PlayAudioFile(ProjectConstants.audio_alert_fail);
                    // await App.Current.MainPage.DisplayAlert("Exception", response.ReasonPhrase, "Ok");
                    // Preferences.Set(Pref.MOVENOTIFICATION, count);
                }



            }
            catch (Exception excp)
            {
                // Common.SaveLogs(excp.StackTrace);
                // Preferences.Set(Pref.MOVENOTIFICATION, count);
                // ObjStockList = database.GetStockList(branch_id);
                //await App.Current.MainPage.DisplayAlert("Exception", "Request could n, please try again later", "Ok");
                Crashes.TrackError(excp);

            }
        }
    }
}
