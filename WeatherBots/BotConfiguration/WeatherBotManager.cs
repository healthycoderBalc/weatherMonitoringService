using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using weatherMonitoringService.ConsoleInterface;
using weatherMonitoringService.WeatherStations;

namespace weatherMonitoringService.WeatherBots.BotConfiguration
{
    public class WeatherBotManager
    {
        public static List<IWeatherBot> LoadBots()
        {
            List<IWeatherBot> loadedBots = ReadConfigurationFile.GetAllBotsFromFile();
            ConsoleInfoMessages.PrintLoadedBots(loadedBots);
            return loadedBots;
        }

        public static void AttachBotsToWeatherStation(WeatherStation? weatherStation, List<IWeatherBot> loadedBots)
        {
            weatherStation?.AttachRange(loadedBots.Cast<SubjectObserverPattern.IObserver>().ToList());
        }

    }
}
