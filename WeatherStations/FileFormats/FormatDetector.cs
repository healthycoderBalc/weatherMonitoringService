using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;

namespace weatherMonitoringService.WeatherStations.FileFormats
{
    public class FormatDetector
    {
        public static bool IsJsonFormat(string input)
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

        public static bool IsXMLFormat(string input)
        {
            try
            {
                XElement.Parse(input);
                return true;
            }
            catch (XmlException)
            {
                return false;
            }
        }
    }
}
