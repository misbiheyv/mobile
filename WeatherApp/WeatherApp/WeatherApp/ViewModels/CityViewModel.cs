using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Windows.Input;
using WeatherApp.Annotations;
using Xamarin.Forms;

namespace WeatherApp.ViewModels
{
    public class CityViewModel: INotifyPropertyChanged
    {
        public string[] Cities;

        private string _current;
        public string Current
        {
            get => _current;
            set
            {
                _current = value;
                AvCities = !String.IsNullOrEmpty(value) ? Cities.Where(x => x.StartsWith(value)).Take(50).ToList() : new List<string>();
                OnPropertyChanged(nameof(AvCities));
                OnPropertyChanged(nameof(Current));
            }
        }

        private string _sCity;

        public string SCity
        {
            get => _sCity;
            set
            {
                _sCity = value;
                Current = value;
                OnPropertyChanged(nameof(SCity));
            }
        }

        public List<string> AvCities { get; set; }

        public ICommand PressCommand { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        public CityViewModel()
        {
            PressCommand = new Command(() =>
            {
                if (Cities.Contains(Current))
                    GlobalManager.ActiveCity = Current;
                Current = "";
            });

            _current = GlobalManager.ActiveCity;
        }

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
