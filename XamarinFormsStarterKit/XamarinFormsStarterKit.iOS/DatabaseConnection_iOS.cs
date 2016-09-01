using LocalDataAccess.iOS;
using SQLite;
using System;
using System.IO;
using XamarinFormsStarterKit;

[assembly: Xamarin.Forms.Dependency(typeof(DatabaseConnection_iOS))]
namespace LocalDataAccess.iOS
{
    public class DatabaseConnection_iOS: IDatabaseConnection
    {
        public SQLiteConnection DbConnection()
        {
            var dbName = "StarterKit.db3";
            string personalFolder =
              System.Environment.
              GetFolderPath(Environment.SpecialFolder.Personal);
            string libraryFolder =
              Path.Combine(personalFolder, "..", "Library");
            var path = Path.Combine(libraryFolder, dbName);
            return new SQLiteConnection(path);
        }
    }
}