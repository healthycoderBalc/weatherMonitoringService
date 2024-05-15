using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using weatherMonitoringService.SubjectObserverPattern;
using weatherMonitoringService.WeatherBots;

namespace weatherMonitoringService.WeatherStations
{
    public class WeatherStation : IWeatherStation
    {
        private List<IObserver> _weatherBots;

        private float _temperature;
        private float _humidity;
        private string _location;
        public float Temperature
        {
            get { return _temperature; }
            set
            {
                _temperature = value;
                Notify(nameof(Temperature));
            }
        }

        public float Humidity
        {
            get { return _humidity; }
            set
            {
                _humidity = value;
                Notify(nameof(Humidity));
            }
        }

        public string Location { get; set; }


        public WeatherStation()
        {
            _weatherBots = new List<IObserver>();
            _location = string.Empty;
        }

        public void Attach(IObserver observer)
        {
            _weatherBots.Add(observer);
        }
        public void Notify(string propertyName)
        {
            foreach (IObserver observer in _weatherBots)
            {
                observer.Update(this, propertyName);
            }
        }
    }
}
