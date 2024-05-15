namespace weatherMonitoringService.WeatherBots
{
    public interface IWeatherBot
    {
        string BotName { get; set; }
        bool Enabled { get; set; }
        string Message { get; set; }
    }
}