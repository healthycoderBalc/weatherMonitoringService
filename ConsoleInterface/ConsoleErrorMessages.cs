namespace weatherMonitoringService.ConsoleInterface
{
    public static class ConsoleErrorMessages
    {

        public static void NoClassesImplementingInterface()
        {
            Console.WriteLine($"No class found implementing that interface");
        }
        public static void NoInstanceStrategyCreated()
        {
            Console.WriteLine($"An instance for the class couln't be created.");
        }

        public static void NoMatchingFormat()
        {
            Console.WriteLine($"No matching format found.");
        }

        public static void NoWeatherStationFound(string fileFormat)
        {
            Console.WriteLine($"No weather station found for format: {fileFormat}");
        }
    }
}