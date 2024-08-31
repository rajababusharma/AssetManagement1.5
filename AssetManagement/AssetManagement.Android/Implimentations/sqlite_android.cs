using AssetManagement.Droid.Implimentations;
using AssetManagement.Interface;
using SQLite;
using System.IO;
using Xamarin.Forms;

[assembly: Dependency(typeof(sqlite_android))]
namespace AssetManagement.Droid.Implimentations
{
    public class sqlite_android : IDBInterface
    {
        public SQLiteConnection GetConnection()
        {
            var sqliteFilename = "ASSETMANAGE.db";
            string documentPath = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
            var path = Path.Combine(documentPath, sqliteFilename);
            var conn = new SQLite.SQLiteConnection(path);
            return conn;
        }

       
    }
}