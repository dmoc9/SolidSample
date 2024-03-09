using Newtonsoft.Json;
using System.Linq;
using Xunit;

namespace ArdalisRating.Tests
{
    public class FloodPolicyRaterRateTests
    {
        [Theory]
        [InlineData(0, 200000)]
        [InlineData(200000, 0)]
        public void LogsBondAmountAndValuationRequiredGiven0BondAmountOr0Valuation(decimal bondAmount, decimal valuation)
        {
            var policy = new Policy
            {
                Type = PolicyType.Flood.ToString(),
                BondAmount = bondAmount,
                Valuation = valuation
            };
            var logger = new FakeLogger();
            var rating = new FloodPolicyRater(null)
            {
                Logger = logger
            };
            
            rating.Rate(policy);

            Assert.Equal("Flood policy must specify Bond Amount and Valuation.", logger.LoggedMessages.Last());
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-1000)]
        public void LogsPolicyNotAvailableMessageGivenElevationAtOrBelowSeaLevel(int elevation)
        {
            var policy = new Policy
            {
                Type = PolicyType.Flood.ToString(),
                BondAmount = 200000m,
                Valuation = 200000m,
                ElevationAboveSeaLevelFeet = elevation
            };
            var logger = new FakeLogger();
            var rating = new FloodPolicyRater(null)
            {
                Logger = logger
            };

            rating.Rate(policy);

            Assert.Equal("Flood policy is not available for elevations at or below sea level.", logger.LoggedMessages.Last());
        }

        [Fact]
        public void InsufficientBondAmountMessageGiven200000BondAmountWith260000Valuation()
        {
            var policy = new Policy
            {
                Type = PolicyType.Flood.ToString(),
                BondAmount = 200000m,
                Valuation = 260000m,
                ElevationAboveSeaLevelFeet = 1000
            };
            var logger = new FakeLogger();
            var rating = new FloodPolicyRater(null)
            {
                Logger = logger
            };

            rating.Rate(policy);

            Assert.Equal("Insufficient bond amount.", logger.LoggedMessages.Last());
        }

        [Fact]
        public void SetsRatingToNullGiven200000BondAmountWith260000Valuation()
        {
            var policy = new Policy
            {
                Type = PolicyType.Flood.ToString(),
                BondAmount = 200000m,
                Valuation = 260000m
            };
            var ratingUpdater = new FakeRatingUpdater();
            var rating = new FloodPolicyRater(ratingUpdater);
            rating.Rate(policy);

            Assert.Null(ratingUpdater.NewRating);
        }

        [Fact]
        public void SetsRatingTo20000Given200000BondAmountAnd200000ValuationWith50Elevation()
        {
            var policy = new Policy
            {
                Type = PolicyType.Flood.ToString(),
                BondAmount = 200000m,
                Valuation = 200000m,
                ElevationAboveSeaLevelFeet = 50
            };
            var ratingUpdater = new FakeRatingUpdater();
            var rating = new FloodPolicyRater(ratingUpdater);
            rating.Rate(policy);

            Assert.Equal(20000m, ratingUpdater.NewRating.Value);
        }

        [Fact]
        public void SetsRatingTo15000Given200000BondAmountAnd200000ValuationWith250Elevation()
        {
            var policy = new Policy
            {
                Type = PolicyType.Flood.ToString(),
                BondAmount = 200000m,
                Valuation = 200000m,
                ElevationAboveSeaLevelFeet = 250
            };
            var ratingUpdater = new FakeRatingUpdater();
            var rating = new FloodPolicyRater(ratingUpdater);
            rating.Rate(policy);

            Assert.Equal(15000m, ratingUpdater.NewRating.Value);
        }

        [Fact]
        public void SetsRatingTo11000Given200000BondAmountAnd200000ValuationWith750Elevation()
        {
            var policy = new Policy
            {
                Type = PolicyType.Flood.ToString(),
                BondAmount = 200000m,
                Valuation = 200000m,
                ElevationAboveSeaLevelFeet = 750
            };
            var ratingUpdater = new FakeRatingUpdater();
            var rating = new FloodPolicyRater(ratingUpdater);
            rating.Rate(policy);

            Assert.Equal(11000m, ratingUpdater.NewRating.Value);
        }

        [Fact]
        public void SetsRatingTo20000Given200000BondAmountAnd200000ValuationWith1000Elevation()
        {
            var policy = new Policy
            {
                Type = PolicyType.Flood.ToString(),
                BondAmount = 200000m,
                Valuation = 200000m,
                ElevationAboveSeaLevelFeet = 1000
            };
            var ratingUpdater = new FakeRatingUpdater();
            var rating = new FloodPolicyRater(ratingUpdater);
            rating.Rate(policy);

            Assert.Equal(10000m, ratingUpdater.NewRating.Value);
        }
    }
}
