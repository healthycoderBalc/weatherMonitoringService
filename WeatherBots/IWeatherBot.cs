using weatherMonitoringService.SubjectObserverPattern;
using weatherMonitoringService.WeatherStations;

namespace weatherMonitoringService.WeatherBots
{
    public interface IWeatherBot : IObserver
    {
        string BotName { get; set; }
        bool Enabled { get; set; }
        string Message { get; set; }
        bool CheckingThreshold(WeatherStation weatherStation);
    }
}