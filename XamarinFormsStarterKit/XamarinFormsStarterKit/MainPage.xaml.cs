using Plugin.Connectivity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace XamarinFormsStarterKit
{
    public partial class MainPage : ContentPage
    {
        private ItemService itemsService;

        public MainPage()
        {
            InitializeComponent();
            this.itemsService = new ItemService();
        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();
            this.BusyIndicator.IsVisible = true;
            this.BusyIndicator.IsRunning = true;
            try
            {
                await this.itemsService.PopulateDataAsync(false);
                this.BindingContext = this.itemsService.Items;
            }
            catch(InvalidOperationException ex)
            {
                await DisplayAlert("Error", "Check your network connection.", "OK");
                return;
            }
            catch(Exception ex)
            {
                await DisplayAlert("Error", ex.Message, "OK");
                return;
            }
            finally
            {
                this.BusyIndicator.IsVisible = false;
                this.BusyIndicator.IsRunning = false;
            }
        }

        private async void RefreshButton_Clicked(object sender, EventArgs e)
        {
            this.BusyIndicator.IsVisible = true;
            this.BusyIndicator.IsRunning = true;
            try
            {
                await this.itemsService.PopulateDataAsync(true);
                this.BindingContext = this.itemsService.Items;
            }
            catch (InvalidOperationException ex)
            {
                await DisplayAlert("Error", "Check your network connection.", "OK");
                return;
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", ex.Message, "OK");
                return;
            }
            finally
            {
                this.BusyIndicator.IsVisible = false;
                this.BusyIndicator.IsRunning = false;
            }
        }

        private async void RssView_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            var selected = e.Item as Item;

            if (selected != null)
                await Navigation.PushAsync(new WebContentPage(selected.Link));
        }
    }
}
