using weatherMonitoringService.BotConfiguration;
using weatherMonitoringService.ConsoleInterface;
using weatherMonitoringService.WeatherBots;
using weatherMonitoringService.WeatherStations;
using weatherMonitoringService.WeatherStations.FileFormats;

namespace weatherMonitoringService
{
    public class Program
    {
        static void Main(string[] args)
        {
            List<IWeatherBot> loadedBots = ReadConfigurationFile.GetAllBots();
            ConsoleMessages.PrintLoadedBots(loadedBots);

            Console.WriteLine("Enter weather data: ");
            string? weatherData = Console.ReadLine();
            Type formatDetectorType = typeof(IFormatDetector);
            string? weatherDataFormat = FormatManager.DetectFileFormat(weatherData, formatDetectorType, "IsThisFormat");

            Console.WriteLine($"weatherDataFormat: {weatherDataFormat}");

            var strategy = new JsonWeatherStation();
            WeatherStation weatherStation = new(strategy);
            foreach (IWeatherBot bot in loadedBots)
            {
                weatherStation.Attach(bot);
            }


            //weatherStation.Location = "Buenos Aires";
            //weatherStation.Temperature = 35.0f; // SunBot
            //weatherStation.Humidity = 85.0f; // RainBot
            //weatherStation.Temperature = -3.5f; // SnowBot
        }
    }
}
