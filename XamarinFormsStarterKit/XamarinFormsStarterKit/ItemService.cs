using SQLite;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Linq;
using Xamarin.Forms;

namespace XamarinFormsStarterKit
{
    public class ItemService
    {
        public static string FeedUri = "https://channel9.msdn.com/Blogs/MVP-VisualStudio-Dev/RSS";

        private async Task<IEnumerable<Item>> QueryRssAsync(CancellationToken token)
        {
            try
            {
                //Access via HTTP to the feed and download data as a string
                var client = new HttpClient();
                var data = await client.GetAsync(new Uri(FeedUri), token);

                var actualData = await data.Content.ReadAsStringAsync();

                //Execute a LINQ to XML query against the feed
                var doc = XDocument.Parse(actualData);
                var dc = XNamespace.Get("http://purl.org/dc/elements/1.1/");
                var media = XNamespace.Get("http://search.yahoo.com/mrss/");

                var query = (from entry in doc.Descendants("item")
                             select new Item
                             {
                                 Title = entry.Element("title").Value,
                                 Link = entry.Element("link").Value,
                                 Author = entry.Element(dc + "creator").Value,
                                 PubDate = DateTime.Parse(entry.Element("pubDate").Value,
                             System.Globalization.CultureInfo.InvariantCulture)
                             });

                return query;
            }
            catch (OperationCanceledException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        private SQLiteConnection database;
        private static object collisionLock = new object();

        public ObservableCollection<Item> Items { get; set; }

        public ItemService()
        {
            this.token = new CancellationToken();
            database = DependencyService.Get<IDatabaseConnection>().DbConnection();
            database.CreateTable<Item>();
        }

        private IEnumerable<Item> OfflineQuery()
        {
            lock (collisionLock)
            {
                return database.Table<Item>().AsEnumerable();
            }
        }

        private CancellationToken token;

        public async Task PopulateDataAsync(bool refresh)
        {
            if(refresh==true && App.IsConnected)
            {
                try
                {
                    this.Items = new ObservableCollection<Item>(await QueryRssAsync(token));
                    database.DropTable<Item>();
                    database.CreateTable<Item>();
                    SaveAllItems();
                    return;
                }
                catch
                {
                    return;
                }
            }

            if (database.Table<Item>().Any())
            {
                this.Items =
                  new ObservableCollection<Item>(OfflineQuery());
                return;
            }
            else
            {
                if (!App.IsConnected) throw new InvalidOperationException();
                this.Items = new ObservableCollection<Item>(await QueryRssAsync(token));
                SaveAllItems();
                return;
            }
        }


        public void SaveAllItems()
        {
            lock (collisionLock)
            {
                foreach (var feedItem in this.Items)
                {
                    if (feedItem.Id != 0)
                    {
                        database.Update(feedItem);
                    }
                    else
                    {
                        database.Insert(feedItem);
                    }
                }
            }
        }
    }
}
