using AutoFixture;
using AutoFixture.Xunit2;
using Moq;
using weatherMonitoringService.Bots;
using weatherMonitoringService.Bots.BotsModels;
using weatherMonitoringService.DataFormats.Parser;
using weatherMonitoringService.Utilities;
using weatherMonitoringService.WeatherData;

namespace weatherMonitoringService.Tests
{
    public class DataParsingTests
    {
        private readonly Fixture _fixture;
        private readonly JsonWeatherDataParser _jsonWeatherDataParser;
        private readonly XmlWeatherDataParser _xmlWeatherDataParser;

        public DataParsingTests()
        {
            _fixture = new Fixture();
            _jsonWeatherDataParser = new JsonWeatherDataParser();
            _xmlWeatherDataParser = new XmlWeatherDataParser();
        }

        [Theory]
        [InlineAutoData]
        public void ShouldBeCorrectlyParseJsonWeatherData(string location, double humidity, double temperature)
        {
            // Arrange
            var weatherData = _fixture.Build<WeatherDataModel>()
                .With(w => w.Location, location)
                .With(w => w.Humidity, humidity)
                .With(w => w.Temperature, temperature)
                .Create();

            var inputData = $"{{\"Location\": \"{location}\", \"Temperature\": {temperature}, \"Humidity\": {humidity}}}";
            // Act
            var actualWeatherData = _jsonWeatherDataParser.Parse(inputData);

            // Assert
            Assert.Equal(weatherData.Location, actualWeatherData.Location);
            Assert.Equal(weatherData.Temperature, actualWeatherData.Temperature);
            Assert.Equal(weatherData.Humidity, actualWeatherData.Humidity);
        }

        [Theory]
        [InlineAutoData]
        public void ShouldBeCorrectlyParseXMLWeatherData(string location, double humidity, double temperature)
        {
            // Arrange
            var weatherData = _fixture.Build<WeatherDataModel>()
                .With(w => w.Location, location)
                .With(w => w.Humidity, humidity)
                .With(w => w.Temperature, temperature)
                .Create();

            var inputData = $"<WeatherData><Location>{location}</Location><Temperature>{temperature}</Temperature><Humidity>{humidity}</Humidity></WeatherData>";
            // Act
            var actualWeatherData = _xmlWeatherDataParser.Parse(inputData);

            // Assert
            Assert.Equal(weatherData.Location, actualWeatherData.Location);
            Assert.Equal(weatherData.Temperature, actualWeatherData.Temperature);
            Assert.Equal(weatherData.Humidity, actualWeatherData.Humidity);
        }
    }

}