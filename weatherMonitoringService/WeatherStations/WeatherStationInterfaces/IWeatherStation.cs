using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using weatherMonitoringService.SubjectObserverPattern;

namespace weatherMonitoringService.WeatherStations.WeatherStationInterfaces
{
    public interface IWeatherStation : IWeatherData
    {
        void AddWeatherInformation(WeatherStation weatherStation, string weatherInformation);
    }
}
