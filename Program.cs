using System.Collections.Generic;
using weatherMonitoringService.ConsoleInterface;
using weatherMonitoringService.FileFormatManagement;
using weatherMonitoringService.WeatherBots;
using weatherMonitoringService.WeatherBots.BotConfiguration;
using weatherMonitoringService.WeatherStations;
using weatherMonitoringService.WeatherStations.FileFormats;
using weatherMonitoringService.WeatherStations.WeatherStationInterfaces;

namespace weatherMonitoringService
{
    public class Program
    {
        public static void Main(string[] args)
        {
            List<IWeatherBot> loadedBots = WeatherBotManager.LoadBots();

            WeatherStation? weatherStation = null;
            string weatherInformation;
            do
            {
                weatherInformation = WeatherStationManager.GetWeatherInformation();
                if (weatherInformation != string.Empty)
                {

                    weatherStation = WeatherStationFileFormatManager.DetectWeatherStationFileFormat(weatherStation, weatherInformation);

                    WeatherBotManager.AttachBotsToWeatherStation(weatherStation, loadedBots);

                    WeatherStationManager.LoadValuesToWeatherStation(weatherStation, weatherInformation);
                }
            } while (weatherInformation != string.Empty);
        }


    }
}

