using Microsoft.Extensions.Configuration;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Text.Json.Nodes;
using System.Threading.Tasks;
using weatherMonitoringService.Bots.BotsModels;
using weatherMonitoringService.DataFormats;

namespace weatherMonitoringService.Utilities
{
    public class ManageBotsLoading : IManageBotsLoading
    {
      
        public List<WeatherBotBase> LoadBotsFromFile()
        {

            var config = JObject.Parse(File.ReadAllText("botsettings.json"));
            var bots = new List<WeatherBotBase>();

            foreach (var currentBot in config)
            {
                string botName = currentBot.Key;
                if (currentBot.Value is JObject botProperties)
                {
                    AddWeatherBot(botName, bots, botProperties);
                }

            }
            return bots;
        }

        private void AddWeatherBot(string botName, List<WeatherBotBase> weatherBots, JObject botProperties)
        {
            bool enabled = botProperties["enabled"]?.Value<bool>() ?? false;
            string message = botProperties["message"]?.Value<string>() ?? "Default message";

            if (!botProperties.ContainsKey("enabled") || !botProperties.ContainsKey("message"))
            {
                Console.WriteLine($"Error: Missing 'enabled' or 'message' configuration for {botName}");
                return;
            }
            switch (botName)
            {
                case "RainBot":
                    AddRainBot(botProperties, botName, weatherBots, enabled, message);
                    break;
                case "SunBot":
                case "SnowBot":
                    AddTemperatureBot(botProperties, botName, weatherBots, enabled, message);
                    break;
                default:
                    Console.WriteLine($"Error: Unrecognized bot name {botName}");
                    break;
            }
        }

        private void AddRainBot(JObject botProperties, string botName, List<WeatherBotBase> weatherBots, bool enabled, string message)
        {
            if (!botProperties.ContainsKey("humidityThreshold"))
            {
                Console.WriteLine($"Error: Missing 'humidityThreshold' configuration for {botName}");
                return;
            }
            double? humidityThreshold = botProperties["humidityThreshold"]?.Value<double>() ?? null;
            if (humidityThreshold != null)
            {
                weatherBots.Add(new RainBot
                {
                    BotName = botName,
                    Enabled = enabled,
                    HumidityThreshold = (double)humidityThreshold,
                    Message = message,
                });
            }
        }

        private void AddTemperatureBot(JObject botProperties, string botName, List<WeatherBotBase> weatherBots, bool enabled, string message)
        {
            if (!botProperties.ContainsKey("temperatureThreshold"))
            {
                Console.WriteLine($"Error: Missing 'temperatureThreshold' configuration for {botName}");
                return;
            }
            double? temperatureThreshold = botProperties["temperatureThreshold"]?.Value<double>() ?? null;
            if (temperatureThreshold != null)
            {
                if (botName == "SunBot")
                {
                    weatherBots.Add(new SunBot
                    {
                        BotName = botName,
                        Enabled = enabled,
                        TemperatureThreshold = (double)temperatureThreshold,
                        Message = message,
                    });
                }
                else if (botName == "SnowBot")
                {
                    weatherBots.Add(new SnowBot
                    {
                        BotName = botName,
                        Enabled = enabled,
                        TemperatureThreshold = (double)temperatureThreshold,
                        Message = message,
                    });
                }
            }
        }
    }
}
