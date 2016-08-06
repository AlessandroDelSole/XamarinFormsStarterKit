using SQLite;
using Xamarin.Forms;
using LocalDataAccess.UWP;
using Windows.Storage;
using System.IO;
using XamarinFormsStarterKit;

[assembly: Dependency(typeof(DatabaseConnection_UWP))]
namespace LocalDataAccess.UWP
{
    public class DatabaseConnection_UWP : IDatabaseConnection
    {
        public SQLiteConnection DbConnection()
        {
            var dbName = "StarterKit.db3";
            var path = Path.Combine(ApplicationData.
              Current.LocalFolder.Path, dbName);
            return new SQLiteConnection(path);
        }
    }
}
