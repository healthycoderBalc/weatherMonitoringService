using AutoFixture;
using AutoFixture.Xunit2;
using weatherMonitoringService.Bots;
using weatherMonitoringService.Bots.BotsModels;
using weatherMonitoringService.WeatherData;

namespace weatherMonitoringService.Tests
{
    public class RainBotTests
    {
        private readonly Fixture _fixture;

        public RainBotTests()
        {
            _fixture = new Fixture();

        }

        [Theory]
        [InlineAutoData]
        public void ShouldBeActivatedWhenHumidityPassesThreshold(int threshold)
        {
            // Arrange
            var rainBot = _fixture.Build<RainBot>()
                .With(b => b.HumidityThreshold, threshold)
                .With(b => b.Enabled, true)
                .Create();

            var random = new Random();
            double actualHumidityAboveThreshold = random.Next(threshold, int.MaxValue);

            var weatherData = _fixture.Build<WeatherDataModel>()
                .With(w => w.Humidity, actualHumidityAboveThreshold)
                .Create();

            // Act
            var isActivated = rainBot.CheckThreshold(weatherData);

            // Assert
            Assert.True(isActivated);

        }

        [Theory]
        [InlineAutoData]
        public void ShouldNotBeActivatedWhenHumidityBelowThreshold(int threshold)
        {
            // Arrange
            var rainBot = _fixture.Build<RainBot>()
                .With(b => b.HumidityThreshold, threshold)
                .With(b => b.Enabled, true)
                .Create();

            var random = new Random();
            double actualHumidityBelowThreshold = random.Next(int.MinValue, threshold);

            var weatherData = _fixture.Build<WeatherDataModel>()
                .With(w => w.Humidity, actualHumidityBelowThreshold)
                .Create();

            // Act
            var isActivated = rainBot.CheckThreshold(weatherData);

            // Assert
            Assert.True(!isActivated);

        }
    }

}