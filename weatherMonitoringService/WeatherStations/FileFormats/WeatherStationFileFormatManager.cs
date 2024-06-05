using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using weatherMonitoringService.FileFormatManagement;
using weatherMonitoringService.ConsoleInterface;
using weatherMonitoringService.ReflectionUtilities;
using weatherMonitoringService.WeatherStations.WeatherStationInterfaces;

namespace weatherMonitoringService.WeatherStations.FileFormats
{
    public static class WeatherStationFileFormatManager
    {
        public static string GetWeatherStationFileFormat(string? weatherData)
        {
            Type formatDetectorType = typeof(IFormatDetector);
            string methodName = "IsThisFormat";
            string? weatherDataFormat = FormatManager.DetectFileFormat(weatherData, formatDetectorType, methodName);

            ConsoleInfoMessages.PrintWeatherStationFileFormat(weatherDataFormat);

            return weatherDataFormat;
        }

        public static Type? GetWeatherStationFileFormatStrategy(string weatherDataFormat)
        {
            IEnumerable<Type>? weatherStations = GenericReflectionUtilities.GetClassesImplementingAnInterface(typeof(IWeatherStationFileFormat));

            Type? weatherStation = weatherStations.FirstOrDefault(weatherStation =>
            {
                PropertyInfo? formatProperty = weatherStation.GetProperty("Format");
                if (formatProperty == null) return false;
                object? formatValue = formatProperty.GetValue(Activator.CreateInstance(weatherStation));
                return formatValue != null && formatValue.Equals(weatherDataFormat);
            });

            ConsoleInfoMessages.PrintWeatherStation(weatherStation?.Name ?? "none");

            return weatherStation;
        }

        public static WeatherStation? DetectWeatherStationFileFormat(WeatherStation? weatherStation, string? weatherInformation)
        {
            string weatherDataFormat = WeatherStationFileFormatManager.GetWeatherStationFileFormat(weatherInformation);
            Type? strategySelected = WeatherStationFileFormatManager.GetWeatherStationFileFormatStrategy(weatherDataFormat);

            if (strategySelected == null)
            {
                ConsoleErrorMessages.NoWeatherStationFound(weatherDataFormat);
            }
            else
            {
                if (Activator.CreateInstance(strategySelected) is not IWeatherStationFileFormat strategyInstance)
                {
                    ConsoleErrorMessages.NoInstanceStrategyCreated();
                }
                else
                {
                    weatherStation = new(strategyInstance);
                }
            }
            return weatherStation;
        }
    }
}
