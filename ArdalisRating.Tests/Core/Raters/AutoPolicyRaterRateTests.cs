using ArdalisRating.Core.Model;
using ArdalisRating.Core.Raters;
using ArdalisRating.Tests.Common.Fakes;
using System.Linq;
using Xunit;

namespace ArdalisRating.Tests.Core.Raters
{
    public class AutoPolicyRaterRateTests
    {
        private readonly FakeLogger _logger;

        public AutoPolicyRaterRateTests()
        {
            _logger = new FakeLogger();
        }

        [Fact]
        public void LogsMakeRequiredMessageAndSetsRatingTo0GivenPolicyWithoutMake()
        {
            var policy = new Policy { Type = PolicyType.Auto.ToString() };
            var rater = new AutoPolicyRater(_logger);

            var result = rater.Rate(policy);

            Assert.Equal(0m, result);
            Assert.Equal("Auto policy must specify Make", _logger.LoggedMessages.Last());
        }

        [Fact]
        public void SetsRatingTo0GivenNonBMWMake()
        {
            var policy = new Policy
            {
                Type = PolicyType.Auto.ToString(),
                Make = "Mercedes"
            };
            var rater = new AutoPolicyRater(_logger);

            var result = rater.Rate(policy);

            Assert.Equal(0m, result);
        }

        [Fact]
        public void SetsRatingTo1000GivenBMWMakeWith250Deductible()
        {
            var policy = new Policy
            {
                Type = PolicyType.Auto.ToString(),
                Make = "BMW",
                Deductible = 250m
            };
            var rater = new AutoPolicyRater(_logger);

            var result = rater.Rate(policy);

            Assert.Equal(1000m, result);
        }

        [Fact]
        public void SetsRatingTo900GivenBMWMakeWith500Deductible()
        {
            var policy = new Policy
            {
                Type = PolicyType.Auto.ToString(),
                Make = "BMW",
                Deductible = 500m
            };
            var rater = new AutoPolicyRater(_logger);

            var result = rater.Rate(policy);

            Assert.Equal(900m, result);
        }
    }
}
