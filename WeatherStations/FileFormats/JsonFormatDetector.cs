using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace weatherMonitoringService.WeatherStations.FileFormats
{
    public class JsonFormatDetector: IFormatDetector
    {
        public bool IsThisFormat(string input)
        {
            try
            {
                JsonSerializer.Deserialize<JsonElement>(input);
                return true;
            }
            catch (JsonException)
            {
                return false;
            }
        }
    }
}
