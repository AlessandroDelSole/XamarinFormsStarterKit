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
                // Show a modal message
                await DisplayAlert("Error", "Check your network connection", "OK");

                // Navigate back
                await Navigation.PopAsync();
            }

            // Assign WebView.Source with the html content to show
            this.WebView1.Source = new Uri(link, UriKind.Absolute);
        }
    }
}
