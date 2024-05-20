using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace weatherMonitoringService.WeatherBots
{
    public interface IBaseWeatherBot
    {
        string BotName { get; set; }
        bool Enabled { get; set; }
        string Message { get; set; }
    }
}
