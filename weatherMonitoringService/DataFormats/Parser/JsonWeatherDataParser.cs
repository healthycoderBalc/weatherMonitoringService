using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using weatherMonitoringService.WeatherData;

namespace weatherMonitoringService.DataFormats.Parser
{
    public class JsonWeatherDataParser : IWeatherDataParser
    {
        public WeatherDataModel Parse(string inputData)
        {
            var weatherData = JsonSerializer
                .Deserialize<WeatherDataModel>(inputData) ??
                throw new Exception("The information provided is not valid");

            return weatherData;
        }
    }
}
