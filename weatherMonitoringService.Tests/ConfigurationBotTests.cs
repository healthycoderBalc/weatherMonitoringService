using AutoFixture;
using Moq;
using weatherMonitoringService.Bots;
using weatherMonitoringService.Bots.BotsModels;
using weatherMonitoringService.Utilities;

namespace weatherMonitoringService.Tests
{
    public class ConfigurationBotTests
    {
        private readonly Fixture _fixture;
        private readonly Mock<IManageBotsLoading> _manageBotsLoadingMock;

        public ConfigurationBotTests()
        {
            _fixture = new Fixture();
            _manageBotsLoadingMock = new Mock<IManageBotsLoading>();
        }

        [Fact]
        public void ShouldBeCorrectlyLoaded()
        {
            // Arrange
            var messageRainBot = "It looks like it's about to pour down!";
            var rainBot = _fixture.Build<RainBot>()
                .With(b => b.HumidityThreshold, 70)
                .With(b => b.Message, messageRainBot)
                .With(b => b.Enabled, true)
                .With(b => b.BotName, "RainBot")
                .Create();

            var messageSunBot = "Wow, it's a scorcher out there!";
            var sunBot = _fixture.Build<SunBot>()
                .With(b => b.TemperatureThreshold, 30)
                .With(b => b.Message, messageSunBot)
                .With(b => b.Enabled, true)
                .With(b => b.BotName, "SunBot")
                .Create();

            var messageSnowBot = "Brrr, it's getting chilly!";
            var snowBot = _fixture.Build<SnowBot>()
                .With(b => b.TemperatureThreshold, 0)
                .With(b => b.Message, messageSnowBot)
                .With(b => b.Enabled, true)
                .With(b => b.BotName, "SnowBot")
                .Create();

            var expectedBots = new List<WeatherBotBase> { rainBot, sunBot, snowBot };

            _manageBotsLoadingMock.Setup(m => m.LoadBotsFromFile()).Returns(expectedBots);

            // Act
            List<WeatherBotBase> actualBots = _manageBotsLoadingMock.Object.LoadBotsFromFile();

            // Assert
            Assert.Equal(expectedBots.Count, actualBots.Count);
            Assert.Contains(rainBot, actualBots);
            Assert.Contains(sunBot, actualBots);
            Assert.Contains(snowBot, actualBots);
        }
    }

}