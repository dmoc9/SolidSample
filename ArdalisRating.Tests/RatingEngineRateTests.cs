using Newtonsoft.Json;
using System.IO;
using Xunit;

namespace ArdalisRating.Tests
{
    public class RatingEngineRateTests
    {
        private readonly RatingEngine _engine;
        private readonly FakeLogger _logger;

        public RatingEngineRateTests()
        {
            _logger = new FakeLogger();
            _engine = new RatingEngine(_logger);
        }

        [Fact]
        public void LogsStartingLoadingAndCompletingMessages()
        {
            var policy = new Policy
            {
                Type = PolicyType.Land.ToString(),
                BondAmount = 200000m,
                Valuation = 200000m
            };
            string json = JsonConvert.SerializeObject(policy);
            File.WriteAllText("policy.json", json);

            _engine.Rate();

            Assert.Contains(_logger.LoggedMessages, m => m == "Starting rate.");
            Assert.Contains(_logger.LoggedMessages, m => m == "Loading policy.");
            Assert.Contains(_logger.LoggedMessages, m => m == "Rating completed.");
        }

        [Fact]
        public void SetsRatingTo10000For200000LandPolicy()
        {
            var policy = new Policy
            {
                Type = PolicyType.Land.ToString(),
                BondAmount = 200000m,
                Valuation = 200000m
            };
            string json = JsonConvert.SerializeObject(policy);
            File.WriteAllText("policy.json", json);

            _engine.Rate();
            var result = _engine.Rating;

            Assert.Equal(10000m, result);
        }

        [Fact]
        public void SetsRatingTo0For200000BondOn260000LandPolicy()
        {
            var policy = new Policy
            {
                Type = PolicyType.Land.ToString(),
                BondAmount = 200000m,
                Valuation = 260000m
            };
            string json = JsonConvert.SerializeObject(policy);
            File.WriteAllText("policy.json", json);

            _engine.Rate();
            var result = _engine.Rating;

            Assert.Equal(0m, result);
        }
    }
}
