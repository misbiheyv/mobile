using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using WeatherApp.Annotations;
using WeatherApp.Models.Current;

namespace WeatherApp.ViewModels
{
    public class CurrentViewModel: INotifyPropertyChanged
    {
        public event Action<string> Updation;
        public event PropertyChangedEventHandler PropertyChanged;
        private CurrentForecast _forecast;

        public CurrentForecast Forecast
        {
            get => _forecast;
            set
            {
                _forecast = value;
                OnPropertyChanged(nameof(MinTemp));
                OnPropertyChanged(nameof(MaxTemp));
                OnPropertyChanged(nameof(WindSpeed));
                OnPropertyChanged(nameof(Weather));
                OnPropertyChanged(nameof(CityName));
                OnPropertyChanged(nameof(Humidity));
            }
        }

        public int Humidity => Forecast.Main.Humidity;
        public double MinTemp => Forecast.Main.TempMin;
        public double MaxTemp => Forecast.Main.TempMax;
        public double WindSpeed => Forecast.Wind.Speed;
        public string Weather => Forecast.Weather[0].Main;
        public string CityName => Forecast.Name;
        public string Time => DateTime.Now.ToString("t");

        public CurrentViewModel()
        {
            var temp = WebLoader.GetCurrentForecastAsync(GlobalManager.ActiveCity);
            Task.WaitAll(temp);
            Forecast = temp.Result;

            StartTimer();
            StartUpdation();
        }

        public async void StartTimer()
        {
            while (true)
            {
                await Task.Delay(100);
                OnPropertyChanged(nameof(Time));
            }
        }

        public async void Update()
        {
            Forecast = await WebLoader.GetCurrentForecastAsync(GlobalManager.ActiveCity);
            Updation?.Invoke(Forecast.Weather.First().Icon);
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
