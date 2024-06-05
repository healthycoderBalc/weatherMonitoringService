namespace weatherMonitoringService.ConsoleInterface
{
    public static class ConsoleInputManager
    {
        public static string? ObtainWeatherData()
        {
            Console.WriteLine("Enter weather data (or Hit Enter to Exit program): ");
            string? weatherData = Console.ReadLine();
            return weatherData;
        }
    }
}