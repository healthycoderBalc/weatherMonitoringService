using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using weatherMonitoringService.SubjectObserverPattern;
using weatherMonitoringService.WeatherBots;
using weatherMonitoringService.WeatherStations.FileFormats;
using weatherMonitoringService.WeatherStations.WeatherStationInterfaces;

namespace weatherMonitoringService.WeatherStations
{
    public class WeatherStation : IWeatherStation
    {
        private List<IObserver> _weatherBots;
        private readonly IWeatherStationFileFormat _fileFormat;

        private float _temperature;
        private float _humidity;
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
            _weatherBots = [];
            Location = string.Empty;
        }

        public WeatherStation(IWeatherStationFileFormat fileFormat) : this()
        {
            _fileFormat = fileFormat;
            
        }

        public void Attach(IObserver observer)
        {
            _weatherBots.Add(observer);
        }

        public void AttachRange(List<IObserver> observers)
        {
            _weatherBots.AddRange(observers);
        }
        public void Notify(string propertyName)
        {
            foreach (IObserver observer in _weatherBots)
            {
                observer.Update(this, propertyName);
            }
        }

        public void AddWeatherInformation(WeatherStation weatherStation,string weatherInformation)
        {
            _fileFormat.AddWeatherInformation(weatherStation,weatherInformation);
        }
    }
}
