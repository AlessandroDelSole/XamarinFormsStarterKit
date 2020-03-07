using System;
using System.IO;
using SQLite;
using XamarinFormsStarterKit.Droid;

[assembly: Xamarin.Forms.Dependency(typeof(DatabaseConnection))]
namespace XamarinFormsStarterKit.Droid
{
    public class DatabaseConnection : IDatabaseConnection
    {
        public SQLiteConnection DbConnection()
        {
            var dbName = "StarterKit.db3";
            var path = Path.Combine(System.Environment.
              GetFolderPath(System.Environment.
              SpecialFolder.Personal), dbName);
            return new SQLiteConnection(path);
        }
    }
}
