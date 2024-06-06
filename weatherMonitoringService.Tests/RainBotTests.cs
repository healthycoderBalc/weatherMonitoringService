using AutoFixture;
using weatherMonitoringService.Bots;
using weatherMonitoringService.Bots.BotsModels;

namespace weatherMonitoringService.Tests
{
    public class RainBotTests
    {
        private readonly Fixture _fixture;

        public RainBotTests()
        {
            _fixture = new Fixture();
        }

        [Fact]
        public void ShouldBeActivatedWhenHumidityPassesThreshold()
        {
            // Arrange
            var message = "It looks like it's about to pour down!";
            var rainBot = _fixture.Build<RainBot>()
                .With(b => b.HumidityThreshold, 70)
                .With(b => b.Message, message)
                .With(b => b.Enabled, true)
                .With(b => b.BotName, "RainBot")
                .Create();

            // var weatherData = 
            // Act

            // Assert
        }
    }

}