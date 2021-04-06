using System;
using System.IO;
using WeatherApp.ViewModels;
using WeatherApp.Views;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace WeatherApp
{
    public partial class App : Application
    {
        public App(bool hasInternet)
        {
            InitializeComponent();

            if (hasInternet)
            {
                if (File.Exists(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) +
                                "/city.txt"))
                {
                    GlobalManager.ActiveCity =
                        File.ReadAllText(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) +
                                         "/city.txt");
                }
                else
                {
                    GlobalManager.ActiveCity = "Moscow";
                }

                GlobalManager.CVM = new CurrentViewModel();
                GlobalManager.WVM = new WeekViewModel();

                MainPage = new AppShell();
            }
            else
            {
                MainPage = new ErrorPage();
            }
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
