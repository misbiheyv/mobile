using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace WeatherApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CurrentPage : ContentPage
    {
        public CurrentPage()
        {
            InitializeComponent();

            BindingContext = GlobalManager.CVM;

            im.Source = new UriImageSource
            {
                CachingEnabled = true,
                CacheValidity = new System.TimeSpan(2, 0, 0, 0),
                Uri = new Uri($"https://openweathermap.org/img/wn/{GlobalManager.CVM.Forecast.Weather.First().Icon}@2x.png")
            };

            GlobalManager.CVM.Updation += s =>
            {
                im.Source = new UriImageSource
                {
                    CachingEnabled = true,
                    CacheValidity = new System.TimeSpan(2, 0, 0, 0),
                    Uri = new Uri($"https://openweathermap.org/img/wn/{s}@2x.png")
                };
            };
        }
    }
}