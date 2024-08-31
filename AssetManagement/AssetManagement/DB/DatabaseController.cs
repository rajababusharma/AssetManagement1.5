
using AssetManagement.Constants;
using AssetManagement.Interface;
using AssetManagement.Model;
using Microsoft.AppCenter.Crashes;
using SQLite;
using System.Collections.Generic;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace AssetManagement.DB
{
    public class DatabaseController
    {
        private SQLiteConnection database;
        private static object collisionLock = new object();
        public DatabaseController()
        {
            database = DependencyService.Get<IDBInterface>().GetConnection();

            database.CreateTable<STockTallyDetails>();
           
        }

        public List<STockTallyDetails> GetStockList(string branch)
        {


            List<STockTallyDetails> Mylist = null;

            try
            {
                using (var dbConn = DependencyService.Get<IDBInterface>().GetConnection())
                {
                    lock (collisionLock)
                    {
                        Mylist = new List<STockTallyDetails>();


                        Mylist = dbConn.Query<STockTallyDetails>("select * from STockTallyDetails where Branch='" + branch + "' order by Status DESC");

                        dbConn.Dispose();
                        dbConn.Close();
                    }
                }

                return Mylist;
            }
            catch (SQLiteException excp)
            {
               
                return Mylist;
                Crashes.TrackError(excp);
            }

        }

        public int Insert_StockData(List<STockTallyDetails> Mylist)
        {
            int status = 0;
            try
            {
                using (var dbConn = DependencyService.Get<IDBInterface>().GetConnection())
                {
                    lock (collisionLock)
                    {
                        // DeleteAll_from_DocketDetList();
                        status = dbConn.InsertAll(Mylist);
                        dbConn.Commit();
                        dbConn.Dispose();
                        dbConn.Close();
                    }
                }

                return status;
            }

            catch (SQLiteException excp)
            {
               
                return status;
                Crashes.TrackError(excp);
            }
        }
        public void DeleteAll_from_Stocks(string branch)
        {
            using (var dbConn = DependencyService.Get<IDBInterface>().GetConnection())
            {
                lock (collisionLock)
                {
                    //dbConn.RunInTransaction(() =>   
                    //   {   
                    // dbConn.DropTable<DocketArticleDetList_Unloading>();
                    // dbConn.CreateTable<DocketArticleDetList_Unloading>();
                    dbConn.Execute("Delete from STockTallyDetails where Branch='" + branch + "'");
                    dbConn.Dispose();
                    dbConn.Close();
                }
                //});   
            }

        }
        public bool CheckDuplicateStockTally(string stockid)
        {
            string status = "Found";
            bool flag = false;
            List<STockTallyDetails> Mylist = null;
            try
            {
                using (var dbConn = DependencyService.Get<IDBInterface>().GetConnection())
                {
                    lock (collisionLock)
                    {
                        // Mylist = new List<AUSAddShortAccessEnitity>();
                        // Mylist = database.Query<DocketDetList>("Select Docket_No,Articles,Scanned_status From DocketDetList");
                        //var existingItem = dbConn.Get<DocketDetList>(dock);
                        // Mylist = dbConn.Query<ArticlesLoading>("Select * From ArticlesLoading where ARTICLE_ID=" + "'" + article + "'");
                        Mylist = dbConn.Query<STockTallyDetails>("Select * From STockTallyDetails where Asset_id=" + "'" + stockid + "' and Status='"+status+"'");
                        if (Mylist.Count > 0)
                        {
                            flag = true;
                        }
                        dbConn.Dispose();
                        dbConn.Close();
                    }
                }
                return flag;
            }
            catch (SQLiteException excp)
            {
                
                return flag;
                Crashes.TrackError(excp);
            }

        }

        public int Update_Stock(string stockid)
        {
          
            int status = 0;
            string scanstatus = "Found";
           
            try
            {
                using (var dbConn = DependencyService.Get<IDBInterface>().GetConnection())
                {
                    lock (collisionLock)
                    {
                        // DeleteAll_from_DocketDetList();
                        //  status = dbConn.Execute("Update DocketDetList SET Scanned_status='" + setstatus + "'  where Docket_No='" + docket + "'");
                        // status = dbConn.Execute("Update DocketDetList_Actualloading SET Scanned_Article=" + "'" + docketQty + "'  where Docket_No=" + "'" + docket + "'");

                        status = dbConn.Execute("Update STockTallyDetails SET Status=" + "'" + scanstatus + "' where Asset_id=" + "'" + stockid + "'");
                        dbConn.Commit();
                        dbConn.Dispose();
                        dbConn.Close();
                    }
                }

                return status;
            }
            catch (SQLiteException excp)
            {
               
                return status;
                Crashes.TrackError(excp);
            }
        }

        public int Update_Username()
        {
            string username = Preferences.Get(Pref.USERNAME, "");

            int status = 0;
            string scanstatus = "Found";

            try
            {
                using (var dbConn = DependencyService.Get<IDBInterface>().GetConnection())
                {
                    lock (collisionLock)
                    {
                        // DeleteAll_from_DocketDetList();
                        //  status = dbConn.Execute("Update DocketDetList SET Scanned_status='" + setstatus + "'  where Docket_No='" + docket + "'");
                        // status = dbConn.Execute("Update DocketDetList_Actualloading SET Scanned_Article=" + "'" + docketQty + "'  where Docket_No=" + "'" + docket + "'");

                        status = dbConn.Execute("Update STockTallyDetails SET User_Name=" + "'" + username + "'");
                        dbConn.Commit();
                        dbConn.Dispose();
                        dbConn.Close();
                    }
                }

                return status;
            }
            catch (SQLiteException excp)
            {

                return status;
                Crashes.TrackError(excp);
            }
        }
    }
}
