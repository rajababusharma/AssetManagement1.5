using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using Xamarin.Essentials;

namespace AssetManagement.Constants
{
    public static class ProjectConstants
    {
        public static string BEEP = "Beep.mp3";
        // public static string BASE_URL = "http://192.168.16.147:5555/api"; // VXpress
        // public static string BASE_URL = "http://192.168.0.102:5555/api"; //home
        // public static string BASE_URL = "http://103.134.4.131/api";  // Aniche server
        public static string BASE_URL =   "http://anichedigital.in/api";  // Aniche server cloud
        public static string LOGIN_API = BASE_URL+"/home/"; // https://localhost:44392/api/home/Login? username = admin123 & password = admin

        public static string GETSTOCKTALLYDATA_API = BASE_URL + "/Values/"; // https://localhost:44392/api/StockTally/GetStockTallyData? Branch = xyz

        public static string POSTSTOCKTALLYDATA_API = BASE_URL + "/Values/PostStockTallyData";

        public static string GETMISSINGFOUNTASSETS_API = BASE_URL + "/Dashboard/";
        public static string GETBRANCHWISEASSETCOUNT_API = BASE_URL + "/Dashboard/";
        public static string GETAMCDETAILS_API = BASE_URL + "/AMC/";
        public static string GETSCANNINGREPORTS_API = BASE_URL + "/Reports/";
        public static string GETBRANCHES_API = BASE_URL + "/Branch";
        public static string GETBRANCHES_API1 = BASE_URL + "/Branch/";
        public static string GETDISPOSEDDATA_API = BASE_URL + "/Disposed/";
        public static string GETAMCDATA_API = BASE_URL + "/AMC/";
        public static string GETINSURANCEDATA_API = BASE_URL + "/Insurance/";
        public static string GETREPAIRDATA_API = BASE_URL + "/Repair/";
        public static string GETMOVEDATA_API = BASE_URL + "/Move/";
        public static string GETASSETCATEGORYCOUNT_API = BASE_URL + "/AssetCategory/";
        public static string GETLOCATIONS_API = BASE_URL + "/Location/GetLocationList";
        public static string GETSUBCATEGORYCOUNT_API = BASE_URL + "/SubCategory/";
        public static string GETVENDORLIST_API = BASE_URL + "/Vendor/";
        public static string GETEMPLOYEELIST_API = BASE_URL + "/Employee/";

        public static string POST_ASSETMOVEDATA_API = BASE_URL + "/Move/POST_MoveAsset";
        public static string POST_ASSETDISPOSE_API = BASE_URL + "/Disposed/InsertDisposed";
        public static string POST_ASSETREPAIR_API = BASE_URL + "/Repair/InsertIntoRepair";
        public static string POST_ASSETTRANSFER_API = BASE_URL + "/Transfer/InsertIntoDisposed";

        public static string GETUSERSLIST_API = BASE_URL + "/User/GetUserList";
        public static string GETUSERSRIGHT_API = BASE_URL + "/User/GetUsersRight";
        public static string POST_USERSRIGHT_API = BASE_URL + "/User/InsertIntoUserRights";

        public static string CREATEUSERS_API = BASE_URL + "/CreateUsers/CreateUsers";
        public static string CREATEASSETS_API = BASE_URL + "/CreateAsset/CreateAssets";

        public static string GETCAT_SUB_DEPT_VENDOR_API = BASE_URL + "/GetCat_Sub_Dept_Vendor/GetCat_Sub_Dept_Vendor";

        public static string REGISTER_COMPANY_API = BASE_URL + "/CreateCompany/CreateCompany";
        public static string ISCOMPANYEXIST_API = BASE_URL + "/CreateCompany/";
        public static string CREATELOCATION_API = BASE_URL + "/Location/InsertLocation";
        public static string CREATEBRANCH_API = BASE_URL + "/Branch/PostBranch";
        public static string CREATECATEGORY_API = BASE_URL + "/GetCat_Sub_Dept_Vendor/InsertCategory";
        public static string CREATESUBCATEGORY_API = BASE_URL + "/SubCategory/InsertSubCategory";
        public static string CREATEVENDOR_API = BASE_URL + "/Vendor/PostVendor";
        public static string CREATEEMPLOYEE_API = BASE_URL + "/Employee/PostEmployee";
        public static string CREATEDEPARTMENT_API = BASE_URL + "/Department/CreateDepartment";
        public static string RECOVERPASSWORD_API = BASE_URL + "/RecoverPassword/";
        public static string CREATEAMC_API = BASE_URL + "/AMC/CreateAMC";
        public static string CREATEINSURANCE_API = BASE_URL + "/Insurance/CreateInsurance";
        public static string GETASSETINFO_API = BASE_URL + "/CreateAsset/";
        public static string SendMail_API = BASE_URL + "/SendMail/";
        public static string GETASSETIMAGES_API = BASE_URL + "/GetImages/";

        public async static void SpeakNow(string textTospeak)
        {
            var locales = await TextToSpeech.GetLocalesAsync();

            // Grab the first locale
            var locale = locales.FirstOrDefault();
            var settings = new SpeechOptions()
            {
                Volume = 1.0f,
                Pitch = 1.0f,
                
                Locale = locale
            };

            await TextToSpeech.SpeakAsync(textTospeak, settings);
        }
    }
}
