using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace XamarinFormsStarterKit
{
    public partial class WebContentPage : ContentPage
    {
        string link;
        public WebContentPage(string link)
        {
            InitializeComponent();

            this.link = link;
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            if (!App.IsConnected)
            {
                await DisplayAlert("Error", "Check your network connection", "OK");
                await Navigation.PopAsync();
            }

            this.WebView1.Source = new Uri(link, UriKind.Absolute);
        }
    }
}
