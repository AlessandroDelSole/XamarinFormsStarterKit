using Plugin.Connectivity;
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
            CrossConnectivity.Current.ConnectivityChanged += (sender, e) => { IsConnected = e.IsConnected; };

            MainPage = new NavigationPage(new MainPage());
        }



        protected override void OnStart()
        {
            // Handle when your app starts
            App.IsConnected = CrossConnectivity.Current.IsConnected;
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
            App.IsConnected = CrossConnectivity.Current.IsConnected;
        }
    }
}
