using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace weatherMonitoringService.WeatherBots.BotConfiguration
{
    public interface IConfigurationWeatherBot : IBaseWeatherBot
    {
        float? HumidityThreshold { get; set; }
        float? TemperatureThreshold { get; set; }
    }
}
