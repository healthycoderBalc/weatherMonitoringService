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
    public abstract class WeatherBotBase : IObserver
    {
        private readonly ConsoleInfoMessages _consoleInfoMessages;

        public WeatherBotBase()
        {
            _consoleInfoMessages = new ConsoleInfoMessages();
        }
        public string? BotName { get; set; }
        public bool Enabled { get; set; }
        public string? Message { get; set; }
        public abstract bool CheckThreshold(WeatherDataModel data);
        public void Activate(bool activated)
        {
            if (!activated) return;
            _consoleInfoMessages.BotActivatedMessage(this);
        }

        public abstract void Update(WeatherDataModel weatherData);
    }
}
