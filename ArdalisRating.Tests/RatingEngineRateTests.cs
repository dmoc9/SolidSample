using Newtonsoft.Json;
using Xunit;

namespace ArdalisRating.Tests
{
    public class RatingEngineRateTests
    {
        private readonly RatingEngine _engine;
        private readonly FakeLogger _logger;
        private readonly FakePolicySource _policySource;

        public RatingEngineRateTests()
        {
            _logger = new FakeLogger();
            _policySource = new FakePolicySource();
            _engine = new RatingEngine(_logger, _policySource);
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
            _policySource.PolicyString = JsonConvert.SerializeObject(policy);

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
            _policySource.PolicyString = JsonConvert.SerializeObject(policy);

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
            _policySource.PolicyString = JsonConvert.SerializeObject(policy);

            _engine.Rate();
            var result = _engine.Rating;

            Assert.Equal(0m, result);
        }
    }
}
