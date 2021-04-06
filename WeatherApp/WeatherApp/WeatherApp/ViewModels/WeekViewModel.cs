using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using WeatherApp.Annotations;
using WeatherApp.Models.Week;

namespace WeatherApp.ViewModels
{
    public class WeekViewModel: INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private WeekForecast _forecast;

        public WeekForecast Forecast
        {
            get => _forecast;
            set
            {
                _forecast = value;
                OnPropertyChanged(nameof(Forecasts));
                OnPropertyChanged(nameof(CityName));
            }
        }

        public List<List> Forecasts => Forecast.List.ToList();
        public string CityName => Forecast.City.Name;

        public WeekViewModel()
        {
            var temp = WebLoader.GetWeekForecastAsync(GlobalManager.ActiveCity);
            Task.WaitAll(temp);
            Forecast = temp.Result;

            StartUpdation();
        }

        public async void Update()
        {
            Forecast = await WebLoader.GetWeekForecastAsync(GlobalManager.ActiveCity);
        }

        public async void StartUpdation()
        {
            while (true)
            {
                await Task.Delay(1000 * 60 * 5);
                Update();
            }
        }

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
