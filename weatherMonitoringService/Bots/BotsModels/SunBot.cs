using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using weatherMonitoringService.ConsoleInterface;
using weatherMonitoringService.SubjectObserverPattern;
using weatherMonitoringService.WeatherData;

namespace weatherMonitoringService.Bots.BotsModels
{
    public class SunBot : WeatherBotBase
    {
        public double TemperatureThreshold { get; set; }

        public override bool CheckThreshold(WeatherDataModel weatherData)
        {
            var thresholdTrespassed = false;
            if (Enabled &&
                weatherData.Temperature > TemperatureThreshold)
            {
                thresholdTrespassed = true;
            }

            return thresholdTrespassed;
        }
        public override void Update(WeatherDataModel weatherData)
        {
            bool shouldBeActivated = CheckThreshold(weatherData);
            Activate(shouldBeActivated);
        }

        public override string ToString()
        {
            StringBuilder stringBuilder = new StringBuilder();

            stringBuilder.AppendLine($"BotName: {BotName} ");
            stringBuilder.AppendLine($"# Enabled: {Enabled} ");
            stringBuilder.AppendLine($"# Temperature Threshold: {TemperatureThreshold} ");
            stringBuilder.AppendLine($"# Message: {Message} ");
            stringBuilder.AppendLine();

            return stringBuilder.ToString();
        }
    }
}
