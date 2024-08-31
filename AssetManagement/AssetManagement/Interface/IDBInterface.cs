

using SQLite;

namespace AssetManagement.Interface
{
    public interface IDBInterface
    {
        SQLiteConnection GetConnection();
    }
}
