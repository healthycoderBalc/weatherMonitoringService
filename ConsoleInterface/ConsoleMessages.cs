using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using weatherMonitoringService.WeatherBots;

namespace weatherMonitoringService.ConsoleInterface
{
    public static class ConsoleMessages
    {
        public static void BotMessage(IWeatherBot bot)
        {
            Console.WriteLine($"{bot.BotName} activated!");
            Console.WriteLine($"{bot.BotName}: \"{bot.Message}\"");
            Console.WriteLine();
            Console.ReadLine();
        }

        public static void PrintLoadedBots(List<IWeatherBot> loadedBots)
        {
            Console.WriteLine("*************************");
            foreach (IWeatherBot bot in loadedBots)
            {
                Console.WriteLine(bot.ToString());
            }
            Console.WriteLine("Bots loaded successfully!");
            Console.WriteLine("*************************");
            Console.ReadLine();
        }
    }
}
