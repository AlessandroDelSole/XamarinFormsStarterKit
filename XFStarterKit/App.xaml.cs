
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xamarin.Forms;

namespace XamarinFormsStarterKit
{
    public partial class App : Application
    {
        internal static bool IsConnected;

        public App()
        {
            InitializeComponent();
            Xamarin.Essentials.Connectivity.ConnectivityChanged += (sender, e) => { IsConnected = GetIsConnected(); };

            MainPage = new NavigationPage(new MainPage());
        }



        protected override void OnStart()
        {
            // Handle when your app starts
            App.IsConnected = GetIsConnected();
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
            App.IsConnected = GetIsConnected();
        }

        private bool GetIsConnected()
        {
            return Xamarin.Essentials.Connectivity.NetworkAccess == Xamarin.Essentials.NetworkAccess.Internet;
        }
    }
}
