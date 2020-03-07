
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace XamarinFormsStarterKit
{
    public partial class WebContentPage : ContentPage
    {
        Item item;
        public WebContentPage(Item content)
        {
            InitializeComponent();

            this.item = content;
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
            this.WebView1.Source = new Uri(item.Link, UriKind.Absolute);
        }

        private async void ShareButton_Clicked(object sender, EventArgs e)
        {
            await Share.RequestAsync(new ShareTextRequest
            {
                Uri = item.Link,
                Title = item.Title
            });
        }
    }
}
