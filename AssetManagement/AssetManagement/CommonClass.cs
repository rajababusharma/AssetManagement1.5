using AssetManagement.Constants;
using AssetManagement.Interface;
using AssetManagement.Model;
using AssetManagement.ViewModel;
using Microsoft.AppCenter.Crashes;
using Newtonsoft.Json;
using Plugin.Connectivity;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using static Android.Util.EventLogTags;

namespace AssetManagement
{
    public static class CommonClass
    {

        public static ImageSource ReturnImageFromBase64(string base64String)
        {
            ImageSource One = null;
            if (!base64String.Equals(""))
            {
                // var basevalue = Convert.FromBase64String(input);
                One = Xamarin.Forms.ImageSource.FromStream(() => new MemoryStream(Convert.FromBase64String(base64String)));
                // One = (FileImageSource)ImageSource.FromFile("ok.png");// new BitmapImage(new Uri("ms-appx:///Assets/nil.png"));
            }
            else
            {
                One = (FileImageSource)ImageSource.FromFile("noimage.png");// new BitmapImage(new Uri("ms-appx:///Assets/nil.png"));
            }
            return One;
        }

        public static async void ShareFile(string filename)
        {
            try
            {
                //  var fn = "Attachment.txt";
                // var file = Path.Combine(FileSystem.CacheDirectory, fn);
                var csvPath = DependencyService.Get<IShareReports>().GetCSVPath(filename);
                //File.WriteAllText(csvPath, "Hello World");

                await Share.RequestAsync(new ShareFileRequest
                {
                    Title = "Share scanned data file",
                    File = new ShareFile(csvPath)
                });
            }
            catch (Exception excp)
            {
                Crashes.TrackError(excp);
            }

        }

        public static async void SubmitDetails(string filename, List<STockTallyDetails> Reports)
        {
            try
            {
                //if (DeviceInfo.Platform.ToString() == Device.Android)
                //{
                //    DependencyService.Get<ICheckFilePermission>().CheckPermission();
                //}

                var csvPath = DependencyService.Get<IShareReports>().GetCSVPath(filename);
                // File.Create(csvPath);


                //// build the data in memory
                //StringBuilder s = new StringBuilder();
                //foreach (var listing in articles)
                //{
                //    s = s.AppendLine(listing.BATCH_NO + "," + listing.ARTICLES + "," + listing.DATETIME);
                //}

                //// write the data all at once
                // File.WriteAllText(csvPath, s.ToString());
                //s.Clear();
                using (var logStream = new FileStream(csvPath, FileMode.OpenOrCreate, FileAccess.Write, FileShare.Write))
                using (var streamWriter = new StreamWriter(logStream))
                {
                    var dt = DateTime.Now.ToLongDateString();
                    streamWriter.WriteLine(filename + ", Date: " + dt);
                    streamWriter.WriteLine();
                    streamWriter.WriteLine("Company Id" + "," + "Asset Id" + "," + "Asset Name" + "," + "Asset Location" + "," + "Asset Branch" + "," + "Employee" + "," + "Description");
                    foreach (var listing in Reports)
                    {
                        streamWriter.WriteLine(listing.Company_Id + "," + listing.Asset_id + "," + listing.Asset_name + "," + listing.Location + "," + listing.Branch + "," + listing.Employee + "," + listing.Description);
                    }

                    streamWriter.Flush();
                    streamWriter.Close();
                    logStream.Close();
                }




                // await App.Current.MainPage.DisplayAlert("Alert", "Data saved in " + csvPath + " location", "Ok");



            }
            catch (Exception excp)
            {
                await App.Current.MainPage.DisplayAlert("Exception", excp.Message, "Ok");
                Crashes.TrackError(excp);
            }
        }

        public static async void SubmitDisposalDetails(string filename, List<DisposalReportList> Reports)
        {
            try
            {
                //if (DeviceInfo.Platform.ToString() == Device.Android)
                //{
                //    DependencyService.Get<ICheckFilePermission>().CheckPermission();
                //}

                var csvPath = DependencyService.Get<IShareReports>().GetCSVPath(filename);
                // File.Create(csvPath);


                //// build the data in memory
                //StringBuilder s = new StringBuilder();
                //foreach (var listing in articles)
                //{
                //    s = s.AppendLine(listing.BATCH_NO + "," + listing.ARTICLES + "," + listing.DATETIME);
                //}

                //// write the data all at once
                // File.WriteAllText(csvPath, s.ToString());
                //s.Clear();
                using (var logStream = new FileStream(csvPath, FileMode.OpenOrCreate, FileAccess.Write, FileShare.Write))
                using (var streamWriter = new StreamWriter(logStream))
                {
                    var dt = DateTime.Now.ToLongDateString();
                    streamWriter.WriteLine(filename + ", Date: " + dt);
                    streamWriter.WriteLine();
                    streamWriter.WriteLine("Company Id" + "," + "Asset Id" + "," + "Asset Name" + "," + "Asset Location" + "," + "Asset Branch" + "," + "Disposal Date" + "," + "Authorised by" + "," + "Final location" + "," + "Mode of dispose" + "," + "Reason" + "," + "Residual Value");
                    foreach (var listing in Reports)
                    {
                        streamWriter.WriteLine(listing.Company_Id + "," + listing.Asset_id + "," + listing.Asset_name + "," + listing.Location + "," + listing.Branch + "," + listing.AssetDispose_Date + "," + listing.Authorized_By + "," + listing.FinalLocation + "," + listing.ModeOf_Disposal + "," + listing.ReasonFor_Dispose + "," + listing.Residual_Value);
                    }

                    streamWriter.Flush();
                    streamWriter.Close();
                    logStream.Close();
                }




                // await App.Current.MainPage.DisplayAlert("Alert", "Data saved in " + csvPath + " location", "Ok");



            }
            catch (Exception excp)
            {
                await App.Current.MainPage.DisplayAlert("Exception", excp.Message, "Ok");
                Crashes.TrackError(excp);
            }
        }

        public static async void SubmitAMCDetails(string filename, List<AssetAMCList> objStockList)
        {
            try
            {
                //if (DeviceInfo.Platform.ToString() == Device.Android)
                //{
                //    DependencyService.Get<ICheckFilePermission>().CheckPermission();
                //}

                var csvPath = DependencyService.Get<IShareReports>().GetCSVPath(filename);
                // File.Create(csvPath);


                //// build the data in memory
                //StringBuilder s = new StringBuilder();
                //foreach (var listing in articles)
                //{
                //    s = s.AppendLine(listing.BATCH_NO + "," + listing.ARTICLES + "," + listing.DATETIME);
                //}

                //// write the data all at once
                // File.WriteAllText(csvPath, s.ToString());
                //s.Clear();
                using (var logStream = new FileStream(csvPath, FileMode.OpenOrCreate, FileAccess.Write, FileShare.Write))
                using (var streamWriter = new StreamWriter(logStream))
                {
                    var dt = DateTime.Now.ToLongDateString();
                    streamWriter.WriteLine(filename + ", Date: " + dt);
                    streamWriter.WriteLine();
                    streamWriter.WriteLine("Company Id" + "," + "Asset Id" + "," + "AMC_Description" + "," + "AMC_EndDate" + "," + "AMC_StartDate" + "," + "AMC_Type" + "," + "AMC_Value" + "," + "Vendor_Name" + "," + "Alert Date");
                    foreach (var listing in objStockList)
                    {
                        streamWriter.WriteLine(listing.Company_Id + "," + listing.Asset_id + "," + listing.AMC_Description + "," + listing.AMC_EndDate + "," + listing.AMC_StartDate + "," + listing.AMC_Type + "," + listing.AMC_Value + "," + listing.Vendor_Name + "," + listing.AMC_AlertDate);
                    }

                    streamWriter.Flush();
                    streamWriter.Close();
                    logStream.Close();
                }




                // await App.Current.MainPage.DisplayAlert("Alert", "Data saved in " + csvPath + " location", "Ok");



            }
            catch (Exception excp)
            {
                await App.Current.MainPage.DisplayAlert("Exception", excp.Message, "Ok");
                Crashes.TrackError(excp);
            }
        }

        public static async void SubmitInsuranceDetails(string filename, List<InsuranceDetailList> Reports)
        {
            try
            {
                //if (DeviceInfo.Platform.ToString() == Device.Android)
                //{
                //    DependencyService.Get<ICheckFilePermission>().CheckPermission();
                //}

                var csvPath = DependencyService.Get<IShareReports>().GetCSVPath(filename);
                // File.Create(csvPath);


                //// build the data in memory
                //StringBuilder s = new StringBuilder();
                //foreach (var listing in articles)
                //{
                //    s = s.AppendLine(listing.BATCH_NO + "," + listing.ARTICLES + "," + listing.DATETIME);
                //}

                //// write the data all at once
                // File.WriteAllText(csvPath, s.ToString());
                //s.Clear();
                using (var logStream = new FileStream(csvPath, FileMode.OpenOrCreate, FileAccess.Write, FileShare.Write))
                using (var streamWriter = new StreamWriter(logStream))
                {
                    var dt = DateTime.Now.ToLongDateString();
                    streamWriter.WriteLine(filename + ", Date: " + dt);
                    streamWriter.WriteLine();
                    streamWriter.WriteLine("Company Id" + "," + "Asset Id" + "," + "Insurance Company" + "," + "Insurance_Date" + "," + "Policy_Date" + "," + "Policy_Name" + "," + "Policy_No" + "," + "Premium" + "," + "Mode OF Payment");
                    foreach (var listing in Reports)
                    {
                        streamWriter.WriteLine(listing.Company_Id + "," + listing.Asset_id + "," + listing.InsuranceCompany_Name + "," + listing.Insurance_Date + "," + listing.Policy_Date + "," + listing.Policy_Name + "," + listing.Policy_No + "," + listing.Premium + "," + listing.ModeOFPayment);
                    }

                    streamWriter.Flush();
                    streamWriter.Close();
                    logStream.Close();
                }




                // await App.Current.MainPage.DisplayAlert("Alert", "Data saved in " + csvPath + " location", "Ok");



            }
            catch (Exception excp)
            {
                await App.Current.MainPage.DisplayAlert("Exception", excp.Message, "Ok");
                Crashes.TrackError(excp);
            }
        }

        public static async void SubmitRepairDetails(string filename, List<AssetRepairList> Reports)
        {
            try
            {
                //if (DeviceInfo.Platform.ToString() == Device.Android)
                //{
                //    DependencyService.Get<ICheckFilePermission>().CheckPermission();
                //}

                var csvPath = DependencyService.Get<IShareReports>().GetCSVPath(filename);
                // File.Create(csvPath);


                //// build the data in memory
                //StringBuilder s = new StringBuilder();
                //foreach (var listing in articles)
                //{
                //    s = s.AppendLine(listing.BATCH_NO + "," + listing.ARTICLES + "," + listing.DATETIME);
                //}

                //// write the data all at once
                // File.WriteAllText(csvPath, s.ToString());
                //s.Clear();
                using (var logStream = new FileStream(csvPath, FileMode.OpenOrCreate, FileAccess.Write, FileShare.Write))
                using (var streamWriter = new StreamWriter(logStream))
                {
                    var dt = DateTime.Now.ToLongDateString();
                    streamWriter.WriteLine(filename + ", Date: " + dt);
                    streamWriter.WriteLine();
                    streamWriter.WriteLine("Company Id" + "," + "Asset Id" + "," + "Asset_name" + "," + "Description" + "," + "Authorized_By" + "," + "IsReturned" + "," + "ReasonFor_Delay" + "," + "Location_Code" + "," + "ReasonFor_Repair" + "," + "Repair_Charge" + "," + "Returned_On" + "," + "Vendor_Name");
                    foreach (var listing in Reports)
                    {
                        streamWriter.WriteLine(listing.Company_Id + "," + listing.Asset_id + "," + listing.Asset_name + "," + listing.Description + "," + listing.Authorized_By + "," + listing.IsReturned + "," + listing.ReasonFor_Delay + "," + listing.Location_Code + "," + listing.ReasonFor_Repair + "," + listing.Repair_Charge + "," + listing.Returned_On + "," + listing.Vendor_Name);
                    }

                    streamWriter.Flush();
                    streamWriter.Close();
                    logStream.Close();
                }




                // await App.Current.MainPage.DisplayAlert("Alert", "Data saved in " + csvPath + " location", "Ok");



            }
            catch (Exception excp)
            {
                await App.Current.MainPage.DisplayAlert("Exception", excp.Message, "Ok");
                Crashes.TrackError(excp);
            }
        }
        public static async void SubmitMoveDetails(string filename, List<AssetMoveList> Reports)
        {
            try
            {
                //if (DeviceInfo.Platform.ToString() == Device.Android)
                //{
                //    DependencyService.Get<ICheckFilePermission>().CheckPermission();
                //}

                var csvPath = DependencyService.Get<IShareReports>().GetCSVPath(filename);
                // File.Create(csvPath);


                //// build the data in memory
                //StringBuilder s = new StringBuilder();
                //foreach (var listing in articles)
                //{
                //    s = s.AppendLine(listing.BATCH_NO + "," + listing.ARTICLES + "," + listing.DATETIME);
                //}

                //// write the data all at once
                // File.WriteAllText(csvPath, s.ToString());
                //s.Clear();
                using (var logStream = new FileStream(csvPath, FileMode.OpenOrCreate, FileAccess.Write, FileShare.Write))
                using (var streamWriter = new StreamWriter(logStream))
                {
                    var dt = DateTime.Now.ToLongDateString();
                    streamWriter.WriteLine(filename + ", Date: " + dt);
                    streamWriter.WriteLine();
                    streamWriter.WriteLine("Company Id" + "," + "Asset Id" + "," + "Asset_name" + "," + "From_Location" + "," + "To_Location" + "," + "From Branch" + "," + "To Branch" + "," + "AssetMove_Date" + "," + "Status" + "," + "Remarks");
                    foreach (var listing in Reports)
                    {
                        streamWriter.WriteLine(listing.Company_Id + "," + listing.Asset_id + "," + listing.Asset_name + "," + listing.From_Location_Description + "," + listing.To_Location_Description + "," + listing.From_Floor + "," + listing.To_Floor + "," + listing.AssetMove_Date + "," + listing.Status + "," + listing.Remarks);
                    }

                    streamWriter.Flush();
                    streamWriter.Close();
                    logStream.Close();
                }




                // await App.Current.MainPage.DisplayAlert("Alert", "Data saved in " + csvPath + " location", "Ok");



            }
            catch (Exception excp)
            {
                await App.Current.MainPage.DisplayAlert("Exception", excp.Message, "Ok");
                Crashes.TrackError(excp);
            }
        }
        public static async Task GetMoveNotification()
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
                        count = stocktake.AssetMoveList.Count;
                        if (count > 0)
                        {
                            // for local notification
                            DependencyService.Get<INotification>().CreateNotification("Welcome to local notification", "You have an asset move notification, please click to view.");
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

        public static async Task GetMove_AMCNotification()
        {
            MasterDetailPage1MasterViewModel vm = new MasterDetailPage1MasterViewModel();
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
                        if (stocktake.AssetMoveList != null)
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


                            vm.NOTIFICATIONTEXT = movecount.ToString();

                            // for local notification
                            // DependencyService.Get<INotification>().CreateNotification("Welcome to local notification", "You have an asset move notification, please click to view.");
                        }
                        if (amc_count > 0 || ins_count > 0)
                        {
                            vm.AMCNOTIFICATIONTEXT = (amc_count + ins_count).ToString();
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

        public static async Task GetAssetDetails(string filename, List<CreateAssetRequest> Reports)
        {
            try
            {
                
                var csvPath = DependencyService.Get<IShareReports>().GetCSVPath(filename);
               
                using (var logStream = new FileStream(csvPath, FileMode.OpenOrCreate, FileAccess.Write, FileShare.Write))
                using (var streamWriter = new StreamWriter(logStream))
                {
                    var dt = DateTime.Now.ToLongDateString();
                    streamWriter.WriteLine(filename + ", Date: " + dt);
                    streamWriter.WriteLine();
                    streamWriter.WriteLine(
                        "Company Id" + "," + 
                        "Asset Id" + "," + 
                        "Financial Year" + "," + 
                        "Asset Name" + "," + 
                        "Asset Description" + "," + 
                        "Location" + "," + 
                        "Branch" + "," + 
                        "Employee" + "," + 
                        "Category" + "," + 
                        "SubCategory" + "," + 
                        "Asset_type" + "," + 
                        "Asset_value" + "," + 
                        "Asset_life" + "," + 
                        "Vendor" + "," + 
                        "SAP_code" + "," + 
                        "Purchase_date" + "," + 
                        "Install_date" + "," + 
                        "Manufactured By" + "," + 
                        "Mfd_date" + "," + 
                        "Warranty_period" + "," + 
                        "Model_no" + "," + 
                        "Part_no" + "," + 
                        "Serial_no" + "," + 
                        "Residual_value" + "," + 
                        "Depreciation" + "," + 
                        "Invoice_No" + "," + 
                        "Make" + "," + 
                        "Department" + "," + 
                        "Remarks");
                    foreach (var listing in Reports)
                    {
                        streamWriter.WriteLine(
                            listing.Company_Id + "," + 
                            listing.Asset_id + "," +
                            listing.FinancialYear + "," +
                            listing.Asset_name + "," + 
                            listing.Description + "," + 
                            listing.Location + "," + 
                            listing.Branch + "," + 
                            listing.Employee + "," +
                             listing.Category + "," +
                              listing.SubCategory + "," +
                               listing.Asset_type + "," +
                                listing.Asset_value + "," +
                                 listing.Asset_life + "," +
                                  listing.Vendor + "," +
                                   listing.SAP_code + "," +
                                    listing.Purchase_date + "," +
                                     listing.Install_date + "," +
                                      listing.ManufacturedBy + "," +
                                      listing.Mfd_date + "," +
                                     listing.Warranty_period + "," +
                                      listing.Model_no + "," +
                                       listing.Part_no + "," +
                                        listing.Serial_no + "," +
                                         listing.Residual_value + "," +
                                         listing.Depreciation + "," +
                                         listing.Invoice_No + "," +
                                          listing.Make + "," +
                                          listing.Department + "," +
                            listing.Remark);
                    }

                    streamWriter.Flush();
                    streamWriter.Close();
                    logStream.Close();
                }
            }
            catch (Exception excp)
            {
                await App.Current.MainPage.DisplayAlert("Exception", excp.Message, "Ok");
                Crashes.TrackError(excp);
            }
        }
    }
}
