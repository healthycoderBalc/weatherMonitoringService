using AutoFixture;
using AutoFixture.Xunit2;
using weatherMonitoringService.Bots;
using weatherMonitoringService.Bots.BotsModels;
using weatherMonitoringService.WeatherData;

namespace weatherMonitoringService.Tests
{
    public class SnowBotTests
    {
        private readonly Fixture _fixture;

        public SnowBotTests()
        {
            _fixture = new Fixture();

        }

        [Theory]
        [InlineAutoData]
        public void ShouldBeActivatedWhenTemperatureBelowThreshold(int threshold)
        {
            // Arrange
            var snowBot = _fixture.Build<SnowBot>()
                .With(b => b.TemperatureThreshold, threshold)
                .With(b => b.Enabled, true)
                .Create();

            var random = new Random();
            double actualTemperatureBelowThreshold = random.Next(int.MinValue, threshold);

            var weatherData = _fixture.Build<WeatherDataModel>()
                .With(w => w.Temperature, actualTemperatureBelowThreshold)
                .Create();

            // Act
            var isActivated = snowBot.CheckThreshold(weatherData);

            // Assert
            Assert.True(isActivated);

        }

        [Theory]
        [InlineAutoData]
        public void ShouldNotBeActivatedWhenTemperatureAboveThreshold(int threshold)
        {
            // Arrange
            var snowBot = _fixture.Build<SnowBot>()
                .With(b => b.TemperatureThreshold, threshold)
                .With(b => b.Enabled, true)
                .Create();

            var random = new Random();
            double actualTemperatureAboveThreshold = random.Next(threshold, int.MaxValue);

            var weatherData = _fixture.Build<WeatherDataModel>()
                .With(w => w.Temperature, actualTemperatureAboveThreshold)
                .Create();

            // Act
            var isActivated = snowBot.CheckThreshold(weatherData);

            // Assert
            Assert.True(!isActivated);

        }
    }

}