using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using weatherMonitoringService.WeatherBots;
using System.Configuration;
using Microsoft.Extensions.Configuration;
using System.Reflection;
using weatherMonitoringService.ReflectionUtilities;
using weatherMonitoringService.WeatherStations.WeatherStationInterfaces;

namespace weatherMonitoringService.WeatherBots.BotConfiguration
{
    public static class ReadConfigurationFile
    {
        private static readonly List<IWeatherBot> _weatherBots = [];

        public static List<IWeatherBot> GetAllBotsFromFile()
        {
            IConfiguration config = new ConfigurationBuilder().AddJsonFile("botsettings.json").Build();
            var bots = config.GetChildren();
            foreach (IConfigurationSection bot in bots)
            {
                AddWeatherBot(bot);
            }
            return _weatherBots;
        }

        private static void AddWeatherBot(IConfigurationSection currentBot)
        {
            Dictionary<string, string> botProperties = [];

            string botName = currentBot.Key;
            botProperties.Add("BotName", botName);

            IEnumerable<PropertyInfo> singleProperties = GenericReflectionUtilities.GetPropertiesOfAClass<IConfigurationWeatherBot>();
            foreach (PropertyInfo property in singleProperties)
            {
                if (property.Name != "BotName")
                {
                    string? propertyValue = currentBot.GetSection(property.Name).Value;
                    if (propertyValue == null) continue;
                    botProperties.Add(property.Name, propertyValue);
                }
            }

            IWeatherBot? bot = CreateBot(botProperties);
            if (bot == null)
            {
                return;
            }
            _weatherBots.Add(bot);
        }

        private static IWeatherBot? CreateBot(Dictionary<string, string> botProperties)
        {
            if (botProperties["Message"] == null) return null;
            if (!botProperties.ContainsKey("TemperatureThreshold"))
            {
                if (!botProperties.ContainsKey("HumidityThreshold")) return null;
                HumidityBot humidityBot = new HumidityBot();
                foreach (KeyValuePair<string, string> property in botProperties)
                {
                    AddPropertyToWeatherBot(humidityBot, property.Key, botProperties[property.Key]);
                }
                return humidityBot;
            }
            TemperatureBot temperatureBot = new TemperatureBot();
            foreach (KeyValuePair<string, string> property in botProperties)
            {
                AddPropertyToWeatherBot(temperatureBot, property.Key, property.Value);
            }
            return temperatureBot;
        }

        private static void AddPropertyToWeatherBot(IWeatherBot weatherBot, string propertyName, string propertyValue)
        {
            PropertyInfo? propertyInfo = typeof(IConfigurationWeatherBot).GetProperty(propertyName) ?? typeof(IBaseWeatherBot).GetProperty(propertyName);

            if (propertyInfo == null) return;
            if (propertyValue == null || string.IsNullOrEmpty(propertyValue)) return;
            AddingPropertyValueAccordingToType(propertyInfo, weatherBot, propertyValue);
        }

        private static void AddingPropertyValueAccordingToType(PropertyInfo propertyInfo, IWeatherBot weatherBot, string propertyValue)
        {
            if (propertyInfo.PropertyType == typeof(string))
            {
                propertyInfo.SetValue(weatherBot, propertyValue);
            }
            else if (propertyInfo.PropertyType == typeof(float?))
            {
                if (float.TryParse(propertyValue, out float floatPropertyValue))
                {
                    propertyInfo.SetValue(weatherBot, (float?)floatPropertyValue);
                }
            }
            else if (propertyInfo.PropertyType == typeof(bool))
            {
                if (bool.TryParse(propertyValue, out bool boolPropertyValue))
                {
                    propertyInfo.SetValue(weatherBot, boolPropertyValue);
                }
            }
        }
    }
}
