using AutoFixture;
using AutoFixture.Xunit2;
using weatherMonitoringService.Bots;
using weatherMonitoringService.Bots.BotsModels;
using weatherMonitoringService.WeatherData;

namespace weatherMonitoringService.Tests
{
    public class SunBotTests
    {
        private readonly Fixture _fixture;

        public SunBotTests()
        {
            _fixture = new Fixture();

        }

        [Theory]
        [InlineAutoData]
        public void ShouldBeActivatedWhenTemperaturePassesThreshold(int threshold)
        {
            // Arrange
            var sunBot = _fixture.Build<SunBot>()
                .With(b => b.TemperatureThreshold, threshold)
                .With(b => b.Enabled, true)
                .Create();

            var random = new Random();
            double actualTemperatureAboveThreshold = random.Next(threshold, int.MaxValue);

            var weatherData = _fixture.Build<WeatherDataModel>()
                .With(w => w.Temperature, actualTemperatureAboveThreshold)
                .Create();

            // Act
            var isActivated = sunBot.CheckThreshold(weatherData);

            // Assert
            Assert.True(isActivated);

        }

        [Theory]
        [InlineAutoData]
        public void ShouldNotBeActivatedWhenTemperatureBelowThreshold(int threshold)
        {
            // Arrange
            var sunBot = _fixture.Build<SunBot>()
                .With(b => b.TemperatureThreshold, threshold)
                .With(b => b.Enabled, true)
                .Create();

            var random = new Random();
            double actualTemperatureBelowThreshold = random.Next(int.MinValue, threshold);

            var weatherData = _fixture.Build<WeatherDataModel>()
                .With(w => w.Temperature, actualTemperatureBelowThreshold)
                .Create();

            // Act
            var isActivated = sunBot.CheckThreshold(weatherData);

            // Assert
            Assert.True(!isActivated);

        }
    }

}