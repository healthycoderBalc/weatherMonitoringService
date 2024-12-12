using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using weatherMonitoringService.ConsoleInterface;
using weatherMonitoringService.DataFormats;
using weatherMonitoringService.DataFormats.Detector;
using weatherMonitoringService.DataFormats.Parser;

namespace weatherMonitoringService.Services
{
    public class DataParserService
    {
        private readonly JsonFormatDetector _jsonFormatDetector;
        private readonly XMLFormatDetector _xmlFormatDetector;

        public DataParserService(JsonFormatDetector jsonFormatDetector, XMLFormatDetector xMLFormatDetector)
        {
            _jsonFormatDetector = jsonFormatDetector;
            _xmlFormatDetector = xMLFormatDetector;
        }


        public IWeatherDataParser? DetermineParser(string inputData)
        {
            var isJson = _jsonFormatDetector.IsThisFormat(inputData);
            var isXml = _xmlFormatDetector.IsThisFormat(inputData);

            if (isJson)
            {
                return new JsonWeatherDataParser();
            }
            else if (isXml)
            {
                return new XmlWeatherDataParser();
            }
            return null;
        }
    }
}
