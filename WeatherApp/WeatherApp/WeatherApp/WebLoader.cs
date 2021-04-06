using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using WeatherApp.Models;
using WeatherApp.Models.Current;
using WeatherApp.Models.Week;

namespace WeatherApp
{
    public static class WebLoader
    {
        private static string apiKey = "42f0f4a8667336e35af2c9a00f044618";

        public static Task<string> GetResponseAsync(string url)
        {
            return Task.Factory.StartNew( () =>
            {
                var temp = new HttpClient();
                var t = temp.GetAsync(url);
                Task.WaitAll(t);
                var res = t.Result.Content.ReadAsStringAsync();
                Task.WaitAll(res);
                return res.Result;
            });
        }

        public static Task<CurrentForecast> GetCurrentForecastAsync(string cityName)
        {
            return Task.Factory.StartNew(() =>
            {
                var res = GetResponseAsync($"https://api.openweathermap.org/data/2.5/weather?q={cityName}&units=metric&appid={apiKey}");
                Task.WaitAll(res);
                return Newtonsoft.Json.JsonConvert.DeserializeObject<CurrentForecast>(res.Result);
            });
        }

        public static Task<WeekForecast> GetWeekForecastAsync(string cityName)
        {
            return Task.Factory.StartNew(() =>
            {
                var res = GetResponseAsync($"https://api.openweathermap.org/data/2.5/forecast?q={cityName}&units=metric&appid={apiKey}");
                Task.WaitAll(res);
                return Newtonsoft.Json.JsonConvert.DeserializeObject<WeekForecast>(res.Result);
            });
        }
    }
}
