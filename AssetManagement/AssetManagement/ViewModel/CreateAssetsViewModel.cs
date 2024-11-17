using AssetManagement.Constants;
using AssetManagement.Model;
using Microsoft.AppCenter.Crashes;
using Newtonsoft.Json;
using Plugin.Connectivity;
using RestSharp;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Shapes;
using static Android.App.Assist.AssistStructure;
using static Android.Renderscripts.Sampler;

namespace AssetManagement.ViewModel
{
    public class CreateAssetsViewModel : BaseViewModel
    {
        
        List<string> dept = new List<string>(new string[] { "No Department" });
        List<string> loc = new List<string>(new string[] { "No Location" });
        List<string> br = new List<string>(new string[] { "No Branch" });
        List<string> emp = new List<string>(new string[] { "No Employee" });
        List<string> vend = new List<string>(new string[] { "No Vendor" });
        List<string> cat = new List<string>(new string[] { "No Category" });
        List<string> sub = new List<string>(new string[] { "No Subcategory" });
        public CreateAssetsViewModel(string asset_id)
        {
            ASSETID = asset_id;
            IsBusy = false;
            IsEnable = false;
            IsVisible = false;

            CANCHANGE = ASSETID!=""? false : true;
            List<string> lifelist = new List<string>(new string[] { "Select Asset Life","1", "2", "3", "4", "5", "6", "7", "8", "9", "10", "11", "12", "13", "14", "15", "16", "17", "18", "19", "20" });
            List<string> warrantylist = new List<string>(new string[] { "Select Asset Warranty","1", "2", "3", "4", "5", "6", "7", "8", "9", "10", "11", "12", "13", "14", "15", "16", "17", "18", "19", "20" });
           
            asset_lifeList = lifelist;
            asset_warrantyList = warrantylist;
           
            DateTime dt = DateTime.Now.Date;
            Purchase_date = dt.ToString();
            Install_date = dt.ToString();
            Mfd_date = dt.ToString();

            Device.BeginInvokeOnMainThread(async() =>
            {
               await GetEmployeeList();
            });
            Device.BeginInvokeOnMainThread(async() =>
            {
               await GetCat_Sub_Dept_Vendor();
            });

            Device.BeginInvokeOnMainThread(async() =>
            {
               await GetData(ASSETID);
            });

          /*  GetEmployeeList();
            Thread.Sleep(500);
            GetCat_Sub_Dept_Vendor();
            Thread.Sleep(500);
            GetData(ASSETID);
            Thread.Sleep(500);*/

        }

        public async Task GetCat_Sub_Dept_Vendor()
        {
            if (CrossConnectivity.Current.IsConnected)
            {


                try
                {
                    IsBusy = true;
                    IsEnable = true;
                    IsVisible = true;

                    /* string ctype = "Unloading";*/
                    // string ctype = Preferences.Get(ProjectConstants.CTYPE, "");


                    var client = new RestClient(ProjectConstants.GETCAT_SUB_DEPT_VENDOR_API);
                    var request = new RestRequest(Method.GET);
                    // request.AddHeader("postman-token", "e3fa53b1-0f94-c75d-d04a-e97018565406");
                    request.AddHeader("cache-control", "no-cache");
                    request.AddHeader("content-type", "application/x-www-form-urlencoded");
                    //  request.AddParameter("application/x-www-form-urlencoded", "Truck_No=GJ01DX8008&CType=Loading&Branch_Id=130", ParameterType.RequestBody);
                    IRestResponse response = client.Execute(request);
                    // Extracting output data from received response
                    string strresponse = response.Content;

                    Cate_Sub_Dept_VendorResp apiresponse = new Cate_Sub_Dept_VendorResp();

                    // List<BranchWiseAssets> mystocklist = new List<BranchWiseAssets>();
                    if (response.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        apiresponse = JsonConvert.DeserializeObject<Cate_Sub_Dept_VendorResp>(strresponse);
                        if (apiresponse.Status.Equals("true"))
                        {

                            DepartmentList = apiresponse.DepartmentList ?? dept;
                            CategoryList = apiresponse.CategoryList??cat;
                            SubCategoryList = apiresponse.SubCategoryList??sub;
                            VendorList = apiresponse.VendorList??vend;
                            LocationList = apiresponse.LocationList??loc;
                            BranchList = apiresponse.BranchList??br;

                           
                               // GetEmployeeList();
                               
                           

                            /*Device.BeginInvokeOnMainThread(() =>
                            {
                                GetData(ASSETID).Wait();
                            });*/
                           /* if (!string.IsNullOrEmpty(ASSETID))
                            {
                               
                                GetData(ASSETID).Wait();
                            }*/
                        }
                        else
                        {

                        }

                    }
                    else
                    {

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

                    BranchList = null;
                    //await App.Current.MainPage.DisplayAlert("Exception", "Request could n, please try again later", "Ok");
                    Crashes.TrackError(excp);

                }

            }
            else
            {

                await App.Current.MainPage.DisplayAlert("Alert", "No internet connection, please check and try again later.", "OK");
            }
        }
        public async Task GetEmployeeList()
        {
            if (CrossConnectivity.Current.IsConnected)
            {


                try
                {
                    IsBusy = true;
                    IsEnable = true;
                    IsVisible = true;

                    var client = new System.Net.Http.HttpClient();
                    client.BaseAddress = new Uri(ProjectConstants.GETEMPLOYEELIST_API);
                    try
                    {
                        string branch = Preferences.Get(Pref.BRANCH, "");
                        // string vaid = "2961";

                        //var response = await client.GetAsync("AuthenticateUser?UserName=TNAX5004&Password=TNAX5004");
                        var response = await client.GetAsync("GetEmployeeList?Branch=" + branch);
                        var docketJson = response.Content.ReadAsStringAsync().Result;


                        EmployeeMasterResp docketobject = new EmployeeMasterResp();
                        if (response.StatusCode == System.Net.HttpStatusCode.OK)
                        {
                            if (docketJson != "")
                            {
                                docketobject = JsonConvert.DeserializeObject<EmployeeMasterResp>(docketJson);
                                if (docketobject.Status.Equals("true"))
                                {
                                    // dockets= JsonConvert.DeserializeObject<DeliveryDoket>(docketJson);

                                    EmployeeList = docketobject.Employee??emp;
                                   
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

                }
                catch (Exception excp)
                {
                    // Common.SaveLogs(excp.StackTrace);
                    IsBusy = false;
                    IsEnable = false;
                    IsVisible = false;

                    // BranchList = null;
                    //await App.Current.MainPage.DisplayAlert("Exception", "Request could n, please try again later", "Ok");

                    Crashes.TrackError(excp);
                }

            }
            else
            {

                await App.Current.MainPage.DisplayAlert("Alert", "No internet connection, please check and try again later.", "OK");
            }
        }

        List<String> _departmentList = null;

        public List<String> DepartmentList
        {
            get { return _departmentList; }

            set
            {
                if (_departmentList != value)
                {
                    _departmentList = value;
                    // OnPropertyChanged("ObjPaymentList");
                    NotifyPropertyChanged("DepartmentList");
                }
            }
        }

        List<String> _EmployeeList = new List<string>(100);

        public List<String> EmployeeList
        {
            get { return _EmployeeList; }

            set
            {
                if (_EmployeeList != value)
                {
                    _EmployeeList = value;
                    // OnPropertyChanged("ObjPaymentList");
                    NotifyPropertyChanged("EmployeeList");
                }
            }
        }


        List<String> _branchList = null;

        public List<String> BranchList
        {
            get { return _branchList; }

            set
            {
                if (_branchList != value)
                {
                    _branchList = value;
                    // OnPropertyChanged("ObjPaymentList");
                    NotifyPropertyChanged("BranchList");
                }
            }
        }

        List<String> _LocationList = null;

        public List<String> LocationList
        {
            get { return _LocationList; }

            set
            {
                if (_LocationList != value)
                {
                    _LocationList = value;
                    // OnPropertyChanged("ObjPaymentList");
                    NotifyPropertyChanged("LocationList");
                }
            }
        }

        List<String> _CategoryList = null;

        public List<String> CategoryList
        {
            get { return _CategoryList; }

            set
            {
                if (_CategoryList != value)
                {
                    _CategoryList = value;
                    // OnPropertyChanged("ObjPaymentList");
                    NotifyPropertyChanged("CategoryList");
                }
            }
        }

        List<String> _SubCategoryList = null;

        public List<String> SubCategoryList
        {
            get { return _SubCategoryList; }

            set
            {
                if (_SubCategoryList != value)
                {
                    _SubCategoryList = value;
                    // OnPropertyChanged("ObjPaymentList");
                    NotifyPropertyChanged("SubCategoryList");
                }
            }
        }

        List<String> _VendorList = null;

        public List<String> VendorList
        {
            get { return _VendorList; }

            set
            {
                if (_VendorList != value)
                {
                    _VendorList = value;
                    // OnPropertyChanged("ObjPaymentList");
                    NotifyPropertyChanged("VendorList");
                }
            }
        }




        List<String> _asset_warranty;

        public List<String> asset_warrantyList
        {
            get { return _asset_warranty; }

            set
            {
                if (_asset_warranty != value)
                {
                    _asset_warranty = value;
                    // OnPropertyChanged("ObjPaymentList");
                    NotifyPropertyChanged("asset_warrantyList");
                }
            }
        }


        List<String> _asset_life;

        public List<String> asset_lifeList
        {
            get { return _asset_life; }

            set
            {
                if (_asset_life != value)
                {
                    _asset_life = value;
                    // OnPropertyChanged("ObjPaymentList");
                    NotifyPropertyChanged("asset_lifeList");
                }
            }
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
        public string Asset_name
        {
            get { return _Asset_name; }
            set
            {
                _Asset_name = value;
                NotifyPropertyChanged("Asset_name");
            }
        }

        private string _Description;
        public string Description
        {
            get { return _Description; }
            set
            {
                _Description = value;
                NotifyPropertyChanged("Description");
            }
        }

        private string _Location;
        public string Location
        {
            get { return _Location; }
            set
            {
                _Location = value;
                NotifyPropertyChanged("Location");
            }
        }

        private string _Branch;
        public string Branch
        {
            get { return _Branch; }
            set
            {
                _Branch = value;
                NotifyPropertyChanged("Branch");
            }
        }

        private string _EMPNAME;
        public string Employee
        {
            get { return _EMPNAME; }
            set
            {
                _EMPNAME = value;
                NotifyPropertyChanged("Employee");
            }
        }


        private string _Category;
        public string Category
        {
            get { return _Category; }
            set
            {
                _Category = value;
                NotifyPropertyChanged("Category");
            }
        }


        private string _SubCategory;
        public string SubCategory
        {
            get { return _SubCategory; }
            set
            {
                _SubCategory = value;
                NotifyPropertyChanged("SubCategory");
            }
        }

        private string _Asset_type="";
        public string Asset_type
        {
            get { return _Asset_type; }
            set
            {
                _Asset_type = value;
                NotifyPropertyChanged("Asset_type");
            }
        }

        private string _Asset_value;
        public string Asset_value
        {
            get { return _Asset_value; }
            set
            {
                _Asset_value = value;
                Calcualte_Depreciation();
                NotifyPropertyChanged("Asset_value");
            }
        }

        private string _Asset_life;
        public string Asset_life
        {
            get { return _Asset_life; }
            set
            {
                _Asset_life = value;
                Calcualte_Depreciation();
                NotifyPropertyChanged("Asset_life");
            }
        }

        private string _warranty;
        public string Warranty
        {
            get { return _warranty; }
            set
            {
                _warranty = value;
                NotifyPropertyChanged("Warranty");
            }
        }


        private string _Vendor;
        public string Vendor
        {
            get { return _Vendor; }
            set
            {
                _Vendor = value;
                NotifyPropertyChanged("Vendor");
            }
        }
        private string _SAP_code;
        public string SAP_code
        {
            get { return _SAP_code; }
            set
            {
                _SAP_code = value;
                NotifyPropertyChanged("SAP_code");
            }
        }
        private string _Purchase_date;
        public string Purchase_date
        {
            get { return _Purchase_date; }
            set
            {
                _Purchase_date = value;
                NotifyPropertyChanged("Purchase_date");
            }
        }
        private string _Install_date;
        public string Install_date
        {
            get { return _Install_date; }
            set
            {
                _Install_date = value;
                NotifyPropertyChanged("Install_date");
            }
        }
        private string _ManufacturedBy;
        public string ManufacturedBy
        {
            get { return _ManufacturedBy; }
            set
            {
                _ManufacturedBy = value;
                NotifyPropertyChanged("ManufacturedBy");
            }
        }
        private string _Mfd_date;
        public string Mfd_date
        {
            get { return _Mfd_date; }
            set
            {
                _Mfd_date = value;
                NotifyPropertyChanged("Mfd_date");
            }
        }
        private string _Warranty_period;
        public string Warranty_period
        {
            get { return _Warranty_period; }
            set
            {
                _Warranty_period = value;
                NotifyPropertyChanged("Warranty_period");
            }
        }
        private string _Model_no;
        public string Model_no
        {
            get { return _Model_no; }
            set
            {
                _Model_no = value;
                NotifyPropertyChanged("Model_no");
            }
        }
        private string _Part_no;
        public string Part_no
        {
            get { return _Part_no; }
            set
            {
                _Part_no = value;
                NotifyPropertyChanged("Part_no");
            }
        }
        private string _Serial_no;
        public string Serial_no
        {
            get { return _Serial_no; }
            set
            {
                _Serial_no = value;
                NotifyPropertyChanged("Serial_no");
            }
        }
        private string _Residual_value;
        public string Residual_value
        {
            get { return _Residual_value; }
            set
            {
                _Residual_value = value;
                Calcualte_Depreciation();
                NotifyPropertyChanged("Residual_value");
            }
        }
        private string _Depreciation;
        public string Depreciation
        {
            get { return _Depreciation; }
            set
            {
                _Depreciation = value;
                NotifyPropertyChanged("Depreciation");
            }
        }
        private string _Invoice_No;
        public string Invoice_No
        {
            get { return _Invoice_No; }
            set
            {
                _Invoice_No = value;
                NotifyPropertyChanged("Invoice_No");
            }
        }


        private string _Make;
        public string Make
        {
            get { return _Make; }
            set
            {
                _Make = value;
                NotifyPropertyChanged("Make");
            }
        }

        private string _Department;
        public string Department
        {
            get { return _Department; }
            set
            {
                _Department = value;
                NotifyPropertyChanged("Department");
            }
        }
        private string _Remark;
        public string Remark
        {
            get { return _Remark; }
            set
            {
                _Remark = value;
                NotifyPropertyChanged("Remark");
            }
        }

        private int _selectedlocation_index=0;
        public int SELECTEDLOCATION_INDEX
        {
            get { return _selectedlocation_index; }
            set
            {
                _selectedlocation_index = value;
                NotifyPropertyChanged("SELECTEDLOCATION_INDEX");
            }
        }
        private int _selectedbranch_index = 0;
        public int SELECTEDBRANCH_INDEX
        {
            get { return _selectedbranch_index; }
            set
            {
                _selectedbranch_index = value;
                NotifyPropertyChanged("SELECTEDBRANCH_INDEX");
            }
        }
        private int _selectedemployee_index = 0;
        public int SELECTEDEMPLOYEE_INDEX
        {
            get { return _selectedemployee_index; }
            set
            {
                _selectedemployee_index = value;
                NotifyPropertyChanged("SELECTEDEMPLOYEE_INDEX");
            }
        }

        private int _selectedcategory_index = 0;
        public int SELECTEDCATEGORY_INDEX
        {
            get { return _selectedcategory_index; }
            set
            {
                _selectedcategory_index = value;
                NotifyPropertyChanged("SELECTEDCATEGORY_INDEX");
            }
        }

        private int _selectedsubcategory_index = 0;
        public int SELECTEDSUBCATEGORY_INDEX
        {
            get { return _selectedsubcategory_index; }
            set
            {
                _selectedsubcategory_index = value;
                NotifyPropertyChanged("SELECTEDSUBCATEGORY_INDEX");
            }
        }


        private int _selectedlife_index = 0;
        public int SELECTEDLIFE_INDEX
        {
            get { return _selectedlife_index; }
            set
            {
                _selectedlife_index = value;
                Calcualte_Depreciation();
                NotifyPropertyChanged("SELECTEDLIFE_INDEX");
            }
        }

        private int _selectedVENDOR_index = 0;
        public int SELECTEDVENDOR_INDEX
        {
            get { return _selectedVENDOR_index; }
            set
            {
                _selectedVENDOR_index = value;
                NotifyPropertyChanged("SELECTEDVENDOR_INDEX");
            }
        }

        private int _selectedwarranty_index = 0;
        public int SELECTEDWARRANT_INDEX
        {
            get { return _selectedwarranty_index; }
            set
            {
                _selectedwarranty_index = value;
                NotifyPropertyChanged("SELECTEDWARRANT_INDEX");
            }
        }

        private int _selecteddepartment_index = 0;
        public int SELECTEDDEPARTMENT_INDEX
        {
            get { return _selecteddepartment_index; }
            set
            {
                _selecteddepartment_index = value;
                NotifyPropertyChanged("SELECTEDDEPARTMENT_INDEX");
            }
        }



        private DateTime _selectedpurchade_date = DateTime.Now.Date;
        public DateTime SELECTEDPURCHASE_DATE
        {
            get { return _selectedpurchade_date; }
            set
            {
                _selectedpurchade_date = value;
                NotifyPropertyChanged("SELECTEDPURCHASE_DATE");
            }
        }

        private DateTime _selectedinstall_date = DateTime.Now.Date;
        public DateTime SELECTEDINSTALL_DATE
        {
            get { return _selectedinstall_date; }
            set
            {
                _selectedinstall_date = value;
                NotifyPropertyChanged("SELECTEDINSTALL_DATE");
            }
        }

        private DateTime _selectedmfd_date = DateTime.Now.Date;
        public DateTime SELECTEDMFD_DATE
        {
            get { return _selectedmfd_date; }
            set
            {
                _selectedmfd_date = value;
                NotifyPropertyChanged("SELECTEDMFD_DATE");
            }
        }

        private bool _canchange;

        public bool CANCHANGE
        {
            get { return _canchange; }
            set { _canchange = value;
                NotifyPropertyChanged("CANCHANGE");
            }
        }



        public Command createAssets
        {
            get
            {
                return new Command(CreateAssets);
            }
        }

        private async void CreateAssets(object obj)
        {

            if (!string.IsNullOrEmpty(ASSETID))
            {
            try
            {
                IsBusy = true;
                IsEnable = true;
                IsVisible = true;
                BTNSUBMITSTATUS = false;

                //.....................................................
                string company_id = Preferences.Get(Pref.Company_Id, "");

                CreateAssetRequest createAssets = new CreateAssetRequest();

                createAssets.Asset_id = ASSETID;
                createAssets.Asset_life = Asset_life.Equals("Select Asset Life") ? 0 : Convert.ToInt32(Asset_life);
                createAssets.Asset_name = Asset_name;
                createAssets.Asset_type = Asset_type;
                createAssets.Asset_value = Asset_value;
                createAssets.Branch = Branch;
                createAssets.Category = Category;
                createAssets.Company_Id = company_id;
                createAssets.Current_Date = DateTime.Now.ToShortDateString();
                createAssets.Department = Department;
                createAssets.Depreciation = Depreciation;
                createAssets.Description = Description;
                createAssets.Employee = Employee;
                createAssets.FileName = IMAGE1_BASE64;
                createAssets.FinancialYear = DateTime.Now.ToShortDateString();
                createAssets.Install_date = Install_date;
                createAssets.Invoice_No = Invoice_No;
                createAssets.Location = Location;
                createAssets.Make = Make;
                createAssets.ManufacturedBy = ManufacturedBy;
                createAssets.Mfd_date = Mfd_date;
                createAssets.Model_no = Model_no;
                createAssets.Part_no = Part_no;
                createAssets.Purchase_date = Purchase_date;
                createAssets.Remark = Remark;
                createAssets.Residual_value = Residual_value;
                createAssets.SAP_code = SAP_code;
                createAssets.Serial_no = Serial_no;
                createAssets.SubCategory = SubCategory;
                createAssets.Vendor = Vendor;
                createAssets.Warranty_period = Warranty_period.Equals("Select Asset Warranty") ? 0 : Convert.ToInt32(Warranty_period);
                ;

                //...........................
                var client = new System.Net.Http.HttpClient();
                // client.BaseAddress = new Uri("http://114.143.156.30/");
                client.BaseAddress = new Uri(ProjectConstants.BASE_URL);
                var json = JsonConvert.SerializeObject(createAssets);
                if (CrossConnectivity.Current.IsConnected)  // checking internet connection
                {

                    StringContent str = new StringContent(json, Encoding.UTF8, "application/json");
                    var response = await client.PostAsync(new Uri(ProjectConstants.CREATEASSETS_API), str);
                    var responseJson = response.Content.ReadAsStringAsync().Result;
                    PostResponseForAll stocktakeresponse = new PostResponseForAll();

                    if (response.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        stocktakeresponse = JsonConvert.DeserializeObject<PostResponseForAll>(responseJson);

                        if (stocktakeresponse.Status.Equals("true"))
                        {
                            await App.Current.MainPage.DisplayAlert("Alert", "Asset created successfully.", "Ok");
                                IMAGE1_BASE64 = "";
                                ASSETID = "";
                                Asset_name = "";
                                Description = "";
                                Asset_value = "";
                                SAP_code = "";
                                Model_no = "";
                                Part_no = "";
                                Serial_no="";
                                Invoice_No = "";
                                Make = "";
                                Remark = "";
                                CANCHANGE = true;

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
                Crashes.TrackError(excp);

            }
            IsBusy = false;
            IsEnable = false;
            IsVisible = false;
            BTNSUBMITSTATUS = true;
            }
            else
            {
                await App.Current.MainPage.DisplayAlert("Alert", "Please scan asset id first.", "OK");
            }
        }


        

        public async Task GetData(string assetid)
        {
           if(string.IsNullOrEmpty(assetid)) return;
            if (CrossConnectivity.Current.IsConnected)
            {
                string branch_id = Preferences.Get(Pref.BRANCH, "");
           // string assetid = ASSETID;
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

                Asset_InfoResponse stocktake = new Asset_InfoResponse();

               
                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    stocktake = JsonConvert.DeserializeObject<Asset_InfoResponse>(responseJson);
                    if (stocktake.Status.Equals("true"))
                    {
                        Asset_name = stocktake.Assets.Asset_name;
                        Description= stocktake.Assets.Description;
                        SELECTEDLOCATION_INDEX=LocationList.IndexOf(stocktake.Assets.Location);
                        Location = stocktake.Assets.Location;
                        SELECTEDBRANCH_INDEX = BranchList.IndexOf(stocktake.Assets.Branch);
                        Branch = stocktake.Assets.Branch;
                        SELECTEDEMPLOYEE_INDEX = EmployeeList.IndexOf(stocktake.Assets.Employee);
                        Employee = stocktake.Assets.Employee;
                        SELECTEDCATEGORY_INDEX = CategoryList.IndexOf(stocktake.Assets.Category);
                        Category = stocktake.Assets.Category;
                        SELECTEDSUBCATEGORY_INDEX = SubCategoryList.IndexOf(stocktake.Assets.SubCategory);
                        SubCategory = stocktake.Assets.SubCategory;
                        SELECTEDLIFE_INDEX = asset_lifeList.IndexOf(stocktake.Assets.Asset_life.ToString());
                        Asset_life = stocktake.Assets.Asset_life.ToString();
                        SELECTEDVENDOR_INDEX = VendorList.IndexOf(stocktake.Assets.Vendor);
                        Vendor = stocktake.Assets.Vendor;
                        SELECTEDDEPARTMENT_INDEX = DepartmentList.IndexOf(stocktake.Assets.Department);
                        Department = stocktake.Assets.Department;
                        SELECTEDWARRANT_INDEX = asset_warrantyList.IndexOf(stocktake.Assets.Warranty_period.ToString());
                        Warranty_period = stocktake.Assets.Warranty_period.ToString();
                            SELECTEDINSTALL_DATE = stocktake.Assets.Install_date != "" ? DateTime.Parse(stocktake.Assets.Install_date) : DateTime.Now.Date;
                            //  SELECTEDINSTALL_DATE = Convert.ToDateTime(stocktake.Assets.Install_date) != null ? Convert.ToDateTime(stocktake.Assets.Install_date) : DateTime.Now.Date;
                            Install_date = stocktake.Assets.Install_date;
                           // SELECTEDMFD_DATE=DateTime.Parse(stocktake.Assets.Mfd_date);
                            SELECTEDMFD_DATE = stocktake.Assets.Mfd_date != "" ? DateTime.Parse(stocktake.Assets.Mfd_date) : DateTime.Now.Date;
                            // SELECTEDMFD_DATE = Convert.ToDateTime(stocktake.Assets.Mfd_date) != null ? Convert.ToDateTime(stocktake.Assets.Mfd_date) : DateTime.Now.Date;
                            Mfd_date = stocktake.Assets.Mfd_date;
                            //  SELECTEDPURCHASE_DATE = DateTime.Parse(stocktake.Assets.Purchase_date);
                            SELECTEDPURCHASE_DATE = stocktake.Assets.Purchase_date != "" ? DateTime.Parse(stocktake.Assets.Purchase_date) : DateTime.Now.Date;
                            //  SELECTEDPURCHASE_DATE = Convert.ToDateTime(stocktake.Assets.Purchase_date)!=null? Convert.ToDateTime(stocktake.Assets.Purchase_date):DateTime.Now.Date;
                            Purchase_date = stocktake.Assets.Purchase_date;
                      //  SELECTEDPURCHASE_DATE = Convert.ToDateTime(stocktake.Assets.Purchase_date)!=null? Convert.ToDateTime(stocktake.Assets.Purchase_date):DateTime.Now.Date;
                        Asset_value = stocktake.Assets.Asset_value;
                        ManufacturedBy = stocktake.Assets.ManufacturedBy;
                        Model_no = stocktake.Assets.Model_no;
                        Part_no = stocktake.Assets.Part_no;
                        Serial_no = stocktake.Assets.Serial_no;
                        Residual_value = stocktake.Assets.Residual_value;
                        Depreciation = stocktake.Assets.Depreciation;
                        Invoice_No = stocktake.Assets.Invoice_No;
                        Make = stocktake.Assets.Make;
                        Remark = stocktake.Assets.Remark;
                        SAP_code = stocktake.Assets.SAP_code;

                        IMAGE1_BASE64 = stocktake.Assets.FileName;
                        // Displaying captured image
                        if (!stocktake.Assets.FileName.Equals(""))
                        {
                            Image1 = Xamarin.Forms.ImageSource.FromStream(() => new MemoryStream(Convert.FromBase64String(stocktake.Assets.FileName)));
                        }



                    }
                    else
                    {
                        await App.Current.MainPage.DisplayAlert("Exception", stocktake.Msg.ToString(), "Ok");
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
                // Common.SaveLogs(excp.StackTrace);
                IsBusy = false;
                IsEnable = false;
                IsVisible = false;

                // ObjStockList = database.GetStockList(branch_id);
                //await App.Current.MainPage.DisplayAlert("Exception", "Request could n, please try again later", "Ok");

                // Crashes.TrackError(excp);
            }
            }
            else
            {
                // DependencyService.Get<IAudio>().PlayAudioFile(ProjectConstants.audio_alert_fail);
                await App.Current.MainPage.DisplayAlert("Alert", "No internet connection, please check and try again later.", "OK");

            }
        }

        private void Calcualte_Depreciation()
        {
            if (!string.IsNullOrEmpty(Asset_value) && !string.IsNullOrEmpty(Residual_value) && SELECTEDLIFE_INDEX != 0)
            {
                var ast = Decimal.Parse(Asset_value);
                var res = Decimal.Parse(Residual_value);
                int life = int.Parse(Asset_life);
                Depreciation = ((ast - res) / life).ToString();
              // Straight line method to calculate Depreciation = (Cost of the asset - Residual Value) /Life of the asset;

            }

        }
    }
}
