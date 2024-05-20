using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using weatherMonitoringService.ConsoleInterface;

namespace weatherMonitoringService.WeatherStations
{
    public class WeatherStationManager
    {
        public static string GetWeatherInformation()
        {
            string? weatherInformation = ConsoleInputManager.ObtainWeatherData();
            return weatherInformation ?? "";
        }

        public static void LoadValuesToWeatherStation(WeatherStation? weatherStation, string weatherInformation)
        {
            weatherStation?.AddWeatherInformation(weatherStation, weatherInformation);
        }
    }
}
