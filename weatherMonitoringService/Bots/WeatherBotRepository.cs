using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using weatherMonitoringService.Bots.BotsModels;
using weatherMonitoringService.Utilities;

namespace weatherMonitoringService.Bots
{
    public class WeatherBotRepository
    {
        public List<WeatherBotBase> WeatherBots { get; set; } = new List<WeatherBotBase>();
        private readonly IManageBotsLoading _manageBotsLoading;

        public WeatherBotRepository(IManageBotsLoading manageBotsLoading)
        {
            _manageBotsLoading = manageBotsLoading;
            WeatherBots = _manageBotsLoading.LoadBotsFromFile();
        }

    }
}
