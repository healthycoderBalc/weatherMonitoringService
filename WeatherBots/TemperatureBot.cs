﻿using System;
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
    public class TemperatureBot : IWeatherBot
    {
        public string BotName { get; set; }
        public bool Enabled { get; set; }
        public float TemperatureThreshold { get; set; }
        public string Message { get; set; }


        public TemperatureBot(string botName, bool enabled, float temperatureThreshold, string message)
        {
            BotName = botName;
            Enabled = enabled;
            TemperatureThreshold = temperatureThreshold;
            Message = message;
        }

        public void Update(ISubject subject, string propertyName)
        {
            if (subject is not WeatherStation weatherStation || propertyName != "Temperature")
            {
                return;
            }
            if (propertyName == "Temperature")
            {
                bool performAction = CheckingThreshold(weatherStation);
                PerformSpecificAction(performAction);
            }
        }

        public bool CheckingThreshold(WeatherStation weatherStation)
        {
            bool highTemperature = weatherStation.Temperature > TemperatureThreshold;
            bool lowTemperature = weatherStation.Temperature < TemperatureThreshold;
            bool enabledOnHighTemperature = Enabled && highTemperature;
            bool enabledOnLowTemperature = !Enabled && lowTemperature;
            bool thresholdTrespassed = enabledOnHighTemperature || enabledOnLowTemperature;

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
