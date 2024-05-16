using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using weatherMonitoringService.SubjectObserverPattern;
using weatherMonitoringService.WeatherBots;
using weatherMonitoringService.WeatherStations.FileFormats;

namespace weatherMonitoringService.WeatherStations
{
    public class WeatherStation : IWeatherStation
    {
        private List<IObserver> _weatherBots;
        private readonly IWeatherStationFileFormat _fileFormat;

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


        public WeatherStation(IWeatherStationFileFormat fileFormat)
        {
            _fileFormat = fileFormat;
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

        public void GetWeatherInformation()
        {
            _fileFormat.GetWeatherInformation();
        }

    }
}
