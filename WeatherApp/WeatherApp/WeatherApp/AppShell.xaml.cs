using System;
using System.Collections.Generic;
using WeatherApp.ViewModels;
using WeatherApp.Views;
using Xamarin.Forms;

namespace WeatherApp
{
    public partial class AppShell : Xamarin.Forms.Shell
    {
        public AppShell()
        {
            InitializeComponent();

            Routing.RegisterRoute(nameof(CityPage), typeof(CityPage));
            Routing.RegisterRoute(nameof(CurrentPage), typeof(CurrentPage));
            Routing.RegisterRoute(nameof(ForecastPage), typeof(ForecastPage));
        }
    }
}
