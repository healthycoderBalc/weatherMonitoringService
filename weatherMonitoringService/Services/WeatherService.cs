using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using weatherMonitoringService.Bots;
using weatherMonitoringService.Bots.BotsModels;
using weatherMonitoringService.ConsoleInterface;
using weatherMonitoringService.DataFormats.Parser;
using weatherMonitoringService.SubjectObserverPattern;
using weatherMonitoringService.WeatherData;

namespace weatherMonitoringService.Services
{
    public class WeatherService : ISubject
    {
        private readonly WeatherBotRepository _weatherBotRepository;
        private readonly IWeatherDataParser _weatherDataParser;
        private List<WeatherBotBase> _attachedWeatherBots;
        private readonly string _inputData;



        public WeatherService(WeatherBotRepository weatherBotRepository, IWeatherDataParser weatherDataParser, string inputData)
        {
            _attachedWeatherBots = [];
            _weatherDataParser = weatherDataParser;
            _weatherBotRepository = weatherBotRepository;
            _inputData = inputData;
        }

        public void Start()
        {
            this.AttachRange(_weatherBotRepository.WeatherBots);

            var weatherData = Parse(_inputData);
            if (weatherData == null) return;
            if (string.IsNullOrEmpty(_inputData)) return;

            Notify(weatherData);
        }


        public void AttachRange(List<WeatherBotBase> observers)
        {
            _attachedWeatherBots.AddRange(observers);
        }

        public void Attach(WeatherBotBase observer)
        {
            _attachedWeatherBots.Add(observer);
        }

        public void Notify(WeatherDataModel weatherData)
        {
            foreach (IObserver observer in _attachedWeatherBots)
            {
                observer.Update(weatherData);
            }
        }

        public WeatherDataModel Parse(string inputData)
        {
            return _weatherDataParser.Parse(inputData);
        }

    }
}
