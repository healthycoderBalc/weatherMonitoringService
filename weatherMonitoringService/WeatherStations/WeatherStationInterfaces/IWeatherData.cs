using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using weatherMonitoringService.SubjectObserverPattern;

namespace weatherMonitoringService.WeatherStations.WeatherStationInterfaces
{
    public interface IWeatherData : ISubject
    {
        float Temperature { get; set; }
        float Humidity { get; set; }
        string Location { get; set; }
    }
}
