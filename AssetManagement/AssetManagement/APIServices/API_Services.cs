using AssetManagement.Constants;
using AssetManagement.Model;
using Microsoft.AppCenter.Crashes;
using Newtonsoft.Json;
using Plugin.Connectivity;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using static Android.Net.Wifi.WifiEnterpriseConfig;
using System.Threading.Tasks;
using Xamarin.Essentials;
using RestSharp;

namespace AssetManagement.APIServices
{
    public class API_Services
    {
       
       private string branch = Preferences.Get(Pref.BRANCH, "");
       private int userrole = Preferences.Get(Pref.User_Role, 0);
       private string location = Preferences.Get(Pref.LOCATION, "");
        public API_Services()
        {
            
        }

        public async Task<List<string>> GetEmployeeList()
        {
            List<string> employee = null;
            if (CrossConnectivity.Current.IsConnected)
            {
                 HttpClient client = new HttpClient();
                client.BaseAddress = new Uri(ProjectConstants.GETEMPLOYEELIST_API);
                    try
                    {
                       
                        // string vaid = "2961";

                        //var response = await client.GetAsync("AuthenticateUser?UserName=TNAX5004&Password=TNAX5004");
                        var response = await client.GetAsync("GetEmployeeList?Branch=" + branch + "&userrole=" + userrole);
                        var docketJson = response.Content.ReadAsStringAsync().Result;

                        employee = new List<string>();
                        EmployeeMasterResp docketobject = new EmployeeMasterResp();
                        if (response.StatusCode == System.Net.HttpStatusCode.OK)
                        {
                            if (docketJson != "")
                            {
                                docketobject = JsonConvert.DeserializeObject<EmployeeMasterResp>(docketJson);
                                if (docketobject.Status.Equals("true"))
                                {
                                // dockets= JsonConvert.DeserializeObject<DeliveryDoket>(docketJson);
                                employee= docketobject.Employee;
                                return employee;

                                }
                                else
                                {
                                    await App.Current.MainPage.DisplayAlert("Exception", docketobject.Msg, "Ok"); return employee;
                            }

                            }
                        }
                        else
                        {
                            await App.Current.MainPage.DisplayAlert("Exception", response.ReasonPhrase, "Ok");
                        return employee;
                    }
                       

                    }
                    catch (Exception excp)
                    {
                        await App.Current.MainPage.DisplayAlert("Exception", excp.Message, "Ok");
                    Console.WriteLine($"Error: {excp.Message}");
                    return employee;
                }
            }
            else
            {

                await App.Current.MainPage.DisplayAlert("Alert", "No internet connection, please check and try again later.", "OK"); return employee;
            }
            return employee;
        }

        public async Task<Cate_Sub_Dept_VendorResp> GetCat_Sub_Dept_Vendor()
        {
            Cate_Sub_Dept_VendorResp apiresponse =null;
            if (CrossConnectivity.Current.IsConnected)
            {
                try
                {
                    var client = new RestClient(ProjectConstants.GETCAT_SUB_DEPT_VENDOR_API);
                    var request = new RestRequest(Method.GET);
                    // request.AddHeader("postman-token", "e3fa53b1-0f94-c75d-d04a-e97018565406");
                    request.AddHeader("cache-control", "no-cache");
                    request.AddHeader("content-type", "application/x-www-form-urlencoded");
                    //  request.AddParameter("application/x-www-form-urlencoded", "Truck_No=GJ01DX8008&CType=Loading&Branch_Id=130", ParameterType.RequestBody);
                    IRestResponse response = client.Execute(request);
                    // Extracting output data from received response
                    string strresponse = response.Content;


                    apiresponse = new Cate_Sub_Dept_VendorResp();
                    // List<BranchWiseAssets> mystocklist = new List<BranchWiseAssets>();
                    if (response.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        apiresponse = JsonConvert.DeserializeObject<Cate_Sub_Dept_VendorResp>(strresponse);
                        if (apiresponse.Status.Equals("true"))
                        {

                            /*DepartmentList = apiresponse.DepartmentList ?? dept;
                            CategoryList = apiresponse.CategoryList ?? cat;
                            VendorList = apiresponse.VendorList ?? vend;
                            LocationList = apiresponse.LocationList ?? loc;*/
                            return apiresponse;
                        }
                        else
                        {
                            return apiresponse;
                        }


                    }
                    else
                    {
                        return apiresponse;
                    }


                }
                catch (Exception excp)
                {
                    return apiresponse;
                }

            }
            else
            {
                await App.Current.MainPage.DisplayAlert("Alert", "No internet connection, please check and try again later.", "OK");
                return apiresponse;
            }
        }

        public async Task<List<string>> GetBranches()
        {
            List<string> branch = null;
            if (CrossConnectivity.Current.IsConnected)
            {
                try
                {
                    HttpClient client = new HttpClient();
                    //  client.BaseAddress = new Uri("http://114.143.156.30/");
                    client.BaseAddress = new Uri(ProjectConstants.GETBRANCHES_API);



                    var response = await client.GetAsync("Get?Location=" + location + "&userrole=" + userrole);
                    var responseJson = response.Content.ReadAsStringAsync().Result;


                    BranchMasterResp stocktake = new BranchMasterResp();

                     branch = new List<string>();
                    if (response.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        stocktake = JsonConvert.DeserializeObject<BranchMasterResp>(responseJson);
                        if (stocktake.Status.Equals("true"))
                        {

                            branch = stocktake.Branches;

                            return branch;
                        }
                        else
                        {
                            //DependencyService.Get<IAudio>().PlayAudioFile(ProjectConstants.audio_alert_fail);
                            // await App.Current.MainPage.DisplayAlert("Exception", stocktake.Msg.ToString(), "Ok");
                            //BranchwiseList = stocktake.BranchWiseAssets;
                            return branch;
                        }

                    }
                    else
                    {
                        // DependencyService.Get<IAudio>().PlayAudioFile(ProjectConstants.audio_alert_fail);
                        // await App.Current.MainPage.DisplayAlert("Exception", response.ReasonPhrase, "Ok");
                        //BranchwiseList = stocktake.BranchWiseAssets;
                        return branch;
                    }
                   

                }
                catch (Exception excp)
                {
                    Console.WriteLine("Exception on GetBranches method " + excp.Message);
                    return branch;
                    //await App.Current.MainPage.DisplayAlert("Exception", "Request could n, please try again later", "Ok");
                    // Crashes.TrackError(excp);
                   
                }

            }
            else
            {

                await App.Current.MainPage.DisplayAlert("Alert", "No internet connection, please check and try again later.", "OK"); return branch;
            }
        }

        public async Task<List<GetAssetTypeCount>> GetAMCDetails(string branch_id)
        {

            if (CrossConnectivity.Current.IsConnected)
            {

                try
                {
                    HttpClient client = new HttpClient();
                    //  client.BaseAddress = new Uri("http://114.143.156.30/");
                    client.BaseAddress = new Uri(ProjectConstants.GETAMCDETAILS_API);
                    var response = await client.GetAsync("GetAMCRepair?Branch=" + branch_id);
                    var responseJson = response.Content.ReadAsStringAsync().Result;

                    GetAMC_RepairCount stocktake = new GetAMC_RepairCount();
                    // GetFountMissingCountOnBranch stocktake = new GetFountMissingCountOnBranch();

                    // List<BranchWiseAssets> mystocklist = new List<BranchWiseAssets>();
                    if (response.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        stocktake = JsonConvert.DeserializeObject<GetAMC_RepairCount>(responseJson);
                        if (stocktake.Status.Equals("true"))
                        {



                            return stocktake.GetAssetTypeCount;



                        }
                        else
                        {
                            return null;
                        }

                    }
                    else
                    {
                        return null;
                    }
  

                }
                catch (Exception excp)
                {
                    Console.WriteLine("Exception on GetAMCDetails method " + excp.Message);
                    return null;
                }

            }
            else
            {

                await App.Current.MainPage.DisplayAlert("Alert", "No internet connection, please check and try again later.", "OK"); return null;
            }
        }
        public async Task<List<BranchWiseAssets>> GetStockDetailsBranchWise()
        {
            if (CrossConnectivity.Current.IsConnected)
            {

              
                try
                {
                    HttpClient client = new HttpClient();
                    //  client.BaseAddress = new Uri("http://114.143.156.30/");
                    client.BaseAddress = new Uri(ProjectConstants.GETBRANCHWISEASSETCOUNT_API);



                    var response = await client.GetAsync("GetBranchWiseAssetCount");
                    var responseJson = response.Content.ReadAsStringAsync().Result;

                    BranchWiseAssetCount stocktake = new BranchWiseAssetCount();

                   
                    if (response.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        stocktake = JsonConvert.DeserializeObject<BranchWiseAssetCount>(responseJson);
                        if (stocktake.Status.Equals("true"))
                        {


                            return stocktake.BranchWiseAssets;



                        }
                        else
                        {
                            return null;
                        }

                    }
                    else
                    {
                        return null;
                    }
                   

                }
                catch (Exception excp)
                {
                    Console.WriteLine("Exception on GetStockDetailsBranchWise method " + excp.Message);
                    return null;
                }

            }
            else
            {

                await App.Current.MainPage.DisplayAlert("Alert", "No internet connection, please check and try again later.", "OK"); return null;
            }
        }
    }
}
