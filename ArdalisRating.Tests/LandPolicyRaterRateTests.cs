using System.Linq;
using Xunit;

namespace ArdalisRating.Tests
{
    public class LandPolicyRaterRateTests
    {
        private readonly FakeLogger _logger;

        public LandPolicyRaterRateTests()
        {
            _logger = new FakeLogger();
        }

        [Theory]
        [InlineData(0, 200000)]
        [InlineData(200000, 0)]
        public void LogsBondAmountAndValuationRequiredMessageAndSetsRatingTo0Given0BondAmountOr0Valuation(decimal bondAmount, decimal valuation)
        {
            var policy = new Policy
            {
                Type = PolicyType.Land.ToString(),
                BondAmount = bondAmount,
                Valuation = valuation
            };
            var rater = new LandPolicyRater(_logger);

            var result = rater.Rate(policy);

            Assert.Equal(0m, result);
            Assert.Equal("Land policy must specify Bond Amount and Valuation.", _logger.LoggedMessages.Last());
        }

        [Fact]
        public void LogsInsufficientBondAmountMessageAndSetsRatingTo0Given200000BondAmountWith260000Valuation()
        {
            var policy = new Policy
            {
                Type = PolicyType.Land.ToString(),
                BondAmount = 200000m,
                Valuation = 260000m
            };
            var rater = new LandPolicyRater(_logger);

            var result = rater.Rate(policy);

            Assert.Equal(0m, result);
            Assert.Equal("Insufficient bond amount.", _logger.LoggedMessages.Last());
        }

        [Fact]
        public void SetsRatingTo0Given200000BondAmountWith260000Valuation()
        {
            var policy = new Policy
            {
                Type = PolicyType.Land.ToString(),
                BondAmount = 200000m,
                Valuation = 260000m
            };
            var rater = new LandPolicyRater(_logger);

            var result = rater.Rate(policy);

            Assert.Equal(0m, result);
        }

        [Fact]
        public void SetsRatingTo10000Given200000BondAmountAnd200000Valuation()
        {
            var policy = new Policy
            {
                Type = PolicyType.Land.ToString(),
                BondAmount = 200000m,
                Valuation = 200000m
            };
            var rater = new LandPolicyRater(_logger);

            var result = rater.Rate(policy);

            Assert.Equal(10000m, result);
        }

    }
}
