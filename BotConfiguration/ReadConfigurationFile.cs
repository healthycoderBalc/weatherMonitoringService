using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using weatherMonitoringService.WeatherBots;
using System.Configuration;
using Microsoft.Extensions.Configuration;
using static System.Collections.Specialized.BitVector32;

namespace weatherMonitoringService.BotConfiguration
{
    public static class ReadConfigurationFile
    {
        public static List<IWeatherBot> _weatherBots = new List<IWeatherBot>();


        public static List<IWeatherBot> GetAllBots()
        {
            IConfiguration config = new ConfigurationBuilder().AddJsonFile("botsettings.json").Build();
            var bots = config.GetChildren();
            foreach (var bot in bots)
            {
                AddWeatherBot(bot);
            }
            return _weatherBots;
        }

        private static void AddWeatherBot(IConfigurationSection section)
        {
            string botName = section.Key;
            bool enabledParsingSuccess = bool.TryParse(section.GetSection("enabled").Value, out bool enabled);
            string? message = section.GetSection("message").Value;

            string? temperatureThresholdString = section.GetSection("temperatureThreshold").Value;
            bool temperatureParsingSuccess = false;
            float? temperatureThreshold = null;
            if (temperatureThresholdString != null)
            {
                temperatureParsingSuccess = float.TryParse(temperatureThresholdString, out float temporaryTemperatureThreshold);
                if (temperatureParsingSuccess)
                {
                    temperatureThreshold = temporaryTemperatureThreshold;
                }
            }

            string? humidityThresholdString = section.GetSection("humidityThreshold").Value;
            bool humidityParsingSuccess = false;
            float? humidityThreshold = null;
            if (humidityThresholdString != null)
            {
                humidityParsingSuccess = float.TryParse(humidityThresholdString, out float temporaryHumidityThreshold);
                if (humidityParsingSuccess)
                {
                    humidityThreshold = temporaryHumidityThreshold;
                }
            }

            bool thresholdParsedSuccessfully = temperatureParsingSuccess || humidityParsingSuccess;
            bool allParsedSuccessfully = thresholdParsedSuccessfully && enabledParsingSuccess;
            if (!allParsedSuccessfully)
            {
                return;
            }

            IWeatherBot? bot = CreateBot(botName, enabled, message, temperatureThreshold, humidityThreshold);
            if (bot == null)
            {
                return;
            }
            _weatherBots.Add(bot);
        }

        private static IWeatherBot? CreateBot(string botName, bool enabled, string? message, float? temperatureThreshold, float? humidityThreshold)
        {
            IWeatherBot bot;
            if (message == null)
            {
                return null;
            }
            if (temperatureThreshold == null)
            {
                if (humidityThreshold == null)
                {
                    return null;
                }
                bot = new HumidityBot(botName, enabled, (float)humidityThreshold, message);
                return bot;
            }
            bot = new TemperatureBot(botName, enabled, (float)temperatureThreshold, message);
            return bot;

        }
    }
}
