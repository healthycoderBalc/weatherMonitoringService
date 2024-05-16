using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using weatherMonitoringService.ConsoleInterface;
using weatherMonitoringService.SubjectObserverPattern;
using weatherMonitoringService.WeatherStations;

namespace weatherMonitoringService.WeatherBots
{
    public class HumidityBot : IWeatherBot
    {
        public string BotName { get; set; }
        public bool Enabled { get; set; }
        public float HumidityThreshold { get; set; }
        public string Message { get; set; }


        public HumidityBot(string botName, bool enabled, float humidityThreshold, string message)
        {
            BotName = botName;
            Enabled = enabled;
            HumidityThreshold = humidityThreshold;
            Message = message;
        }

        public void Update(ISubject subject, string propertyName)
        {
            if (subject is not WeatherStation weatherStation || propertyName != "Humidity")
            {
                return;
            }
            if (propertyName == "Humidity")
            {
                bool performAction = CheckingThreshold(weatherStation);
                PerformSpecificAction(performAction);
            }
        }

        public bool CheckingThreshold(WeatherStation weatherStation)
        {
            bool highHumidity = weatherStation.Humidity > HumidityThreshold;
            bool lowHumidity = weatherStation.Humidity < HumidityThreshold;
            bool enabledOnHighHumidity = Enabled && highHumidity;
            bool enabledOnLowHumidity = !Enabled && lowHumidity;
            bool thresholdTrespassed = enabledOnHighHumidity || enabledOnLowHumidity;

            return thresholdTrespassed;
        }

        private void PerformSpecificAction(bool performAction)
        {
            if (!performAction) return;
            ConsoleMessages.BotMessage(this);

        }

        public override string ToString()
        {
            StringBuilder stringBuilder = new StringBuilder();
            PropertyInfo[] properties = GetType().GetProperties();
            foreach (PropertyInfo property in properties)
            {
                stringBuilder.Append(property.Name);
                stringBuilder.Append(": ");
                stringBuilder.Append(property.GetValue(this));
                stringBuilder.AppendLine();
            }

            return stringBuilder.ToString();
        }
    }
}
