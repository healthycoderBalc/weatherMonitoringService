using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using weatherMonitoringService.SubjectObserverPattern;

namespace weatherMonitoringService.WeatherBots
{
    public interface IWeatherTemperatureBot : IObserver, IWeatherBot
    {
        float TemperatureThreshold { get; set; }
    }
}
