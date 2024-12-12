using weatherMonitoringService.Bots.BotsModels;

namespace weatherMonitoringService.Utilities
{
    public interface IManageBotsLoading
    {
        List<WeatherBotBase> LoadBotsFromFile();
    }
}