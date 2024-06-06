using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;
using System.Xml.Linq;

using weatherMonitoringService.WeatherData;

namespace weatherMonitoringService.DataFormats.Parser
{
    public class XmlWeatherDataParser : IWeatherDataParser
    {
        public WeatherDataModel Parse(string inputData)
        {
            XmlDocument xmlDocument = new();
            xmlDocument.LoadXml(inputData);
            string mainXmlNode = "/WeatherData/";

            WeatherDataModel weatherData = new WeatherDataModel();

            string location = xmlDocument?.SelectSingleNode($"{mainXmlNode}Location")?.InnerText ?? "";
            string temperatureString = xmlDocument?.SelectSingleNode($"{mainXmlNode}Temperature")?.InnerText ?? "";
            string humidityString = xmlDocument?.SelectSingleNode($"{mainXmlNode}Humidity")?.InnerText ?? "";

            bool parsingTemperature = double.TryParse(temperatureString, out double temperature);
            bool parsingHumidity = double.TryParse(humidityString, out double humidity);


            if (string.IsNullOrEmpty(location) ||
                string.IsNullOrEmpty(temperatureString) ||
                string.IsNullOrEmpty(humidityString) ||
                !parsingTemperature ||
                !parsingHumidity)
            {
                throw new Exception("There were one or more empty property/ies in weather data received");
            }

            weatherData.Location = location;
            weatherData.Temperature = temperature;
            weatherData.Humidity = humidity;

            return weatherData;

        }
    }
}
