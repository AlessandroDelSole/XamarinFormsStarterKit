using Plugin.Share;
using Plugin.Share.Abstractions;
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

        private void ShareButton_Clicked(object sender, EventArgs e)
        {
            ShareMessage msg = new ShareMessage();
            msg.Title = this.item.Title;
            msg.Url = this.item.Link;

            CrossShare.Current.Share(msg);
        }
    }
}
