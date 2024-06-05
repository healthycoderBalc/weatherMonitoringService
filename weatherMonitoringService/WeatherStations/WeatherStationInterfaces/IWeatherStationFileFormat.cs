using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace weatherMonitoringService.WeatherStations.WeatherStationInterfaces
{
    public interface IWeatherStationFileFormat
    {
        string Format { get; }
        void AddWeatherInformation(WeatherStation weatherStation, string weatherInformation);
    }
}
