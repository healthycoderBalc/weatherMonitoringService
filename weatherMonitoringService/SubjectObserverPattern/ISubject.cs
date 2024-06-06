using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using weatherMonitoringService.Bots.BotsModels;
using weatherMonitoringService.WeatherData;

namespace weatherMonitoringService.SubjectObserverPattern
{
    public interface ISubject
    {
        void Attach(WeatherBotBase weatherBotBase);
        void Notify(WeatherDataModel weatherData);
    }
}
