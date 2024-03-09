using System.Linq;
using Xunit;

namespace ArdalisRating.Tests
{
    public class LandPolicyRaterRateTests
    {
        [Theory]
        [InlineData(0, 200000)]
        [InlineData(200000, 0)]
        public void LogsBondAmountAndValuationRequiredGiven0BondAmountOr0Valuation(decimal bondAmount, decimal valuation)
        {
            var policy = new Policy
            {
                Type = PolicyType.Land.ToString(),
                BondAmount = bondAmount,
                Valuation = valuation
            };
            var logger = new FakeLogger();
            var rater = new LandPolicyRater(null)
            {
                Logger = logger
            };

            rater.Rate(policy);

            Assert.Equal("Land policy must specify Bond Amount and Valuation.", logger.LoggedMessages.Last());
        }
        
        [Fact]
        public void LogsInsufficientBondAmountMessageGiven200000BondAmountWith260000Valuation()
        {
            var policy = new Policy
            {
                Type = PolicyType.Land.ToString(),
                BondAmount = 200000m,
                Valuation = 260000m
            };
            var logger = new FakeLogger();
            var rater = new LandPolicyRater(null)
            {
                Logger = logger
            };

            rater.Rate(policy);

            Assert.Equal("Insufficient bond amount.", logger.LoggedMessages.Last());
        }

        [Fact]
        public void SetsRatingToNullGiven200000BondAmountWith260000Valuation()
        {
            var policy = new Policy
            {
                Type = PolicyType.Land.ToString(),
                BondAmount = 200000m,
                Valuation = 260000m
            };
            var ratingUpdater = new FakeRatingUpdater();
            var rater = new LandPolicyRater(ratingUpdater);

            rater.Rate(policy);

            Assert.Null(ratingUpdater.NewRating);
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
            var ratingUpdater = new FakeRatingUpdater();
            var rater = new LandPolicyRater(ratingUpdater);

            rater.Rate(policy);

            Assert.Equal(10000m, ratingUpdater.NewRating.Value);
        }

    }
}
