using AssetManagement.Constants;
using AssetManagement.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace AssetManagement.ViewModel
{
    public class DownloadImagesViewModel : BaseViewModel
    {
        public DownloadImagesViewModel()
        {
            IsBusy = false;
            IsEnable = false;
            IsVisible = false;
           
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

        internal async void GetAssetImage()
        {
           // string branch_id = Preferences.Get(Pref.BRANCH, "");
            try
            {
                IsBusy = true;
                IsEnable = true;
                IsVisible = true;

                /* string ctype = "Unloading";*/
                // string ctype = Preferences.Get(ProjectConstants.CTYPE, "");


                var client = new System.Net.Http.HttpClient();
                //  client.BaseAddress = new Uri("http://114.143.156.30/");
                client.BaseAddress = new Uri(ProjectConstants.GETASSETIMAGES_API);



                var response = await client.GetAsync("GetImages?AssetId="+ASSETID);
                var responseJson = response.Content.ReadAsStringAsync().Result;

                AssetsImagesResponse imagelistresponse = new AssetsImagesResponse();

                List<assetimages> Imagelist = new List<assetimages>();
                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    imagelistresponse = JsonConvert.DeserializeObject<AssetsImagesResponse>(responseJson);
                    if (imagelistresponse.Status.Equals("true"))
                    {
                        foreach(assetimages assetimages in imagelistresponse.Images)
                        {
                            assetimages assetimages1 = new assetimages();
                            assetimages1.ImageType = assetimages.ImageType;
                            assetimages1.Images = assetimages.Images;
                            Imagelist.Add(assetimages1);
                        }
                        ObjImageList = Imagelist;




                    }
                    else
                    {
                        //DependencyService.Get<IAudio>().PlayAudioFile(ProjectConstants.audio_alert_fail);
                        await App.Current.MainPage.DisplayAlert("Exception", imagelistresponse.Msg.ToString(), "Ok");
                        ObjImageList = null;
                    }

                }
                else
                {
                    // DependencyService.Get<IAudio>().PlayAudioFile(ProjectConstants.audio_alert_fail);
                    await App.Current.MainPage.DisplayAlert("Exception", response.ReasonPhrase, "Ok");
                    ObjImageList = null;
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

        private string assetid;
        public string ASSETID
        {
            get { return assetid; }
            set
            {
                assetid = value;
                NotifyPropertyChanged("ASSETID");
            }
        }

        private List<assetimages> imagelist=null;
        public List<assetimages> ObjImageList
        {
            get { return imagelist; }
            set
            {
                imagelist = value;
                NotifyPropertyChanged("ObjImageList");
            }
        }
    }
}
