using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using weatherMonitoringService.SubjectObserverPattern;
using weatherMonitoringService.WeatherData;

namespace weatherMonitoringService.Bots.BotsModels
{
    public class RainBot : WeatherBotBase
    {
        public double HumidityThreshold { get; set; }

        public override bool CheckThreshold(WeatherDataModel weatherData)
        {
            var thresholdTrespassed = false;
            if (Enabled &&
                weatherData.Humidity > HumidityThreshold)
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
            stringBuilder.AppendLine($"# Humidity Threshold: {HumidityThreshold} ");
            stringBuilder.AppendLine($"# Message: {Message} ");
            stringBuilder.AppendLine();

            return stringBuilder.ToString();
        }
    }
}
