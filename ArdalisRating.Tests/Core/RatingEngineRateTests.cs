using ArdalisRating.Core;
using ArdalisRating.Core.Model;
using ArdalisRating.Core.Raters;
using ArdalisRating.Infrastructure.Serializers;
using ArdalisRating.Tests.Common.Fakes;
using Newtonsoft.Json;
using Xunit;

namespace ArdalisRating.Tests.Core
{
    public class RatingEngineRateTests
    {
        private readonly RatingEngine _engine;
        private readonly FakeLogger _logger;
        private readonly FakePolicySource _policySource;
        private readonly JsonPolicySerializer _policySerializer;
        private readonly RaterFactory _raterFactory;

        public RatingEngineRateTests()
        {
            _logger = new FakeLogger();
            _policySource = new FakePolicySource();
            _policySerializer = new JsonPolicySerializer();
            _raterFactory = new RaterFactory(_logger);
            _engine = new RatingEngine(_logger, _policySource, _policySerializer, _raterFactory);
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
