using weatherMonitoringService.SubjectObserverPattern;
using weatherMonitoringService.WeatherBots.BotConfiguration;
using weatherMonitoringService.WeatherStations;

namespace weatherMonitoringService.WeatherBots
{
    public interface IWeatherBot : IObserver, IBaseWeatherBot, IConfigurationWeatherBot
    {
        public string PropertyMeasured { get; }
        bool CheckingThreshold(WeatherStation weatherStation);
    }
}