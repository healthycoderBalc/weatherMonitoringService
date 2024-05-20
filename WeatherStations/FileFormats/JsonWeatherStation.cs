using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using weatherMonitoringService.WeatherStations.WeatherStationInterfaces;

namespace weatherMonitoringService.WeatherStations.FileFormats
{
    public class JsonWeatherStation: IWeatherStationFileFormat
    {
        public string Format { get; } = "JsonFormatDetector";
        public void AddWeatherInformation(WeatherStation weatherStation, string weatherInformation)
        {
            WeatherStation? weatherData = JsonSerializer.Deserialize<WeatherStation>(weatherInformation);
            if (weatherData != null)
            {
                weatherStation.Location = weatherData.Location;
                weatherStation.Temperature = weatherData.Temperature;
                weatherStation.Humidity = weatherData.Humidity;
            }
        }

    }
}
