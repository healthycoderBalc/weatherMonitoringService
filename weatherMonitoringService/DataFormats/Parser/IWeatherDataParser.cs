using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using weatherMonitoringService.WeatherData;

namespace weatherMonitoringService.DataFormats.Parser
{
    public interface IWeatherDataParser
    {
        WeatherDataModel Parse(string inputData);
    }
}
