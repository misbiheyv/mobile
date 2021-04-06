using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using WeatherApp.ViewModels;

namespace WeatherApp
{
    public static class GlobalManager
    {
        private static string _aCity;

        public static string ActiveCity
        {
            get => _aCity;
            set
            {
                _aCity = value;
                File.WriteAllText(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "/city.txt", value);
                WVM?.Update();
                CVM?.Update();
            }
        }
        public static WeekViewModel WVM;
        public static CurrentViewModel CVM;
        public static CityViewModel CityVM;
    }
}
