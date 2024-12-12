using System.Collections.Generic;
using weatherMonitoringService.Bots;
using weatherMonitoringService.ConsoleInterface;
using weatherMonitoringService.DataFormats.Detector;
using weatherMonitoringService.DataFormats.Parser;
using weatherMonitoringService.Services;
using weatherMonitoringService.Utilities;

namespace weatherMonitoringService
{
    public class Program
    {
        public static void Main(string[] args)
        {
            JsonFormatDetector jsonFormatDetector = new JsonFormatDetector();
            XMLFormatDetector xmlFormatDetector = new XMLFormatDetector();
            DataParserService dataParserService = new DataParserService(jsonFormatDetector, xmlFormatDetector);
            ManageBotsLoading manageBotsLoading = new ManageBotsLoading();
            WeatherBotRepository weatherBotRepository = new WeatherBotRepository(manageBotsLoading);

            ConsoleInfoMessages.PrintLoadedBots(weatherBotRepository.WeatherBots);

            string inputData;
            do
            {
                inputData = ConsoleInputManager.ObtainWeatherData() ?? "";
                IWeatherDataParser? parser = dataParserService.DetermineParser(inputData);
                if (parser != null)
                {
                    WeatherService weatherService = new WeatherService(weatherBotRepository, parser, inputData);
                    weatherService.Start();
                }

            } while (inputData != string.Empty);
        }

    }
}

