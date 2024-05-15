using weatherMonitoringService.WeatherBots;
using weatherMonitoringService.WeatherStations;

namespace weatherMonitoringService
{
    internal class Program
    {
        static void Main(string[] args)
        {
            WeatherStation weatherStation = new();

            HumidityBot RainBot = new HumidityBot("RainBot", true, 70, "It looks like it's about to pour down!");
            TemperatureBot SunBot = new TemperatureBot("SunBot", true, 30, "Wow, it's a scorcher out there!");
            TemperatureBot SnowBot = new TemperatureBot("SnowBot", false, 0, "Brrr, it's getting chilly!");

            weatherStation.Attach(RainBot);
            weatherStation.Attach(SunBot);
            weatherStation.Attach(SnowBot);

            weatherStation.Location = "Buenos Aires";
            weatherStation.Temperature = 35.0f;
            weatherStation.Humidity = 85.0f;
            weatherStation.Temperature = -3.5f;


        }
    }
}
