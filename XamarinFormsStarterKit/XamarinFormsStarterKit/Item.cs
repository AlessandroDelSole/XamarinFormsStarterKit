using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using SQLite;

namespace XamarinFormsStarterKit
{
    [Table("FeedItems")]
    public class Item: INotifyPropertyChanged
    {
        private int _id;
        [PrimaryKey, AutoIncrement]
        public int Id
        {
            get
            {
                return _id;
            }
            set
            {
                this._id = value;
                OnPropertyChanged(nameof(Id));
            }
        }

        private string title;
        public string Title
        {
            get
            {
                return title;
            }

            set
            {
                title=value;
                OnPropertyChanged(nameof(Title));
            }
        }

        private string link;
        public string Link
        {
            get
            {
                return link;
            }

            set
            {
                link = value;
                OnPropertyChanged(nameof(Link));
            }
        }

        private string author;
        public string Author
        {
            get
            {
                return author;
            }

            set
            {
                author = value;
                OnPropertyChanged(nameof(Author));
            }
        }

        private DateTime pubDate;
        public DateTime PubDate
        {
            get
            {
                return pubDate;
            }

            set
            {
                pubDate = value;
                OnPropertyChanged(nameof(PubDate));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string propertyName)
        {
            this.PropertyChanged?.Invoke(this,
              new PropertyChangedEventArgs(propertyName));
        }
    }
}
