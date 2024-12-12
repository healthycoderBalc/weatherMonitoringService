using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using weatherMonitoringService.Bots.BotsModels;

namespace weatherMonitoringService.ConsoleInterface
{
    public class ConsoleInfoMessages
    {
        public void BotActivatedMessage(WeatherBotBase bot)
        {
            Console.WriteLine($"{bot.BotName} activated!");
            Console.WriteLine($"{bot.BotName}: \"{bot.Message}\"");
            Console.WriteLine();
        }
        public static void PrintLoadedBots(List<WeatherBotBase> loadedBots)
        {
            Console.WriteLine("*************************");
            foreach (WeatherBotBase bot in loadedBots)
            {
                Console.WriteLine(bot.ToString());
            }
            Console.WriteLine("Bots loaded successfully!");
            Console.WriteLine("*************************");
        }
    }
}
