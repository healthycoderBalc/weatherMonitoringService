using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using weatherMonitoringService.ReflectionUtilities;
using weatherMonitoringService.WeatherStations.WeatherStationInterfaces;

namespace weatherMonitoringService.WeatherStations.FileFormats
{
    public class XMLWeatherStation : IWeatherStationFileFormat
    {
        public string Format { get; } = "XMLFormatDetector";
        public void AddWeatherInformation(WeatherStation weatherStation, string weatherInformation)
        {
            XmlDocument xmlDocument = new();
            xmlDocument.LoadXml(weatherInformation);
            string mainXmlNode = "/WeatherData/";
            IEnumerable<PropertyInfo> properties = GenericReflectionUtilities.GetPropertiesOfAClass<IWeatherData>();

            foreach (PropertyInfo property in properties)
            {
                string? propertyValue = ExtractXMLNode(xmlDocument, $"{mainXmlNode}{property.Name}");
                if (!string.IsNullOrWhiteSpace(propertyValue))
                {
                    AssignExtractedValueToProperty(weatherStation, property.Name, propertyValue);
                }
            }
        }

        private static string? ExtractXMLNode(XmlDocument xmlDocument, string node)
        {
            return xmlDocument?.SelectSingleNode(node)?.InnerText;
        }

        private static void AssignExtractedValueToProperty(WeatherStation weatherStation, string propertyName, string propertyValue)
        {
            PropertyInfo? propertyInfo = typeof(WeatherStation).GetProperty(propertyName);
            if (propertyInfo == null) { return; }
            if (propertyValue == null) { return; }
            if (propertyInfo.PropertyType == typeof(string))
            {
                propertyInfo.SetValue(weatherStation, propertyValue);
            }
            else if (propertyInfo.PropertyType == typeof(float))
            {
                if (float.TryParse(propertyValue, out float floatPropertyValue))
                {
                    propertyInfo.SetValue(weatherStation, floatPropertyValue);
                }
            }
        }
    }
}
