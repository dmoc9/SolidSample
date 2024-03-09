using System.Linq;
using Xunit;

namespace ArdalisRating.Tests
{
    public class FloodPolicyRaterRateTests
    {
        private readonly FakeLogger _logger;

        public FloodPolicyRaterRateTests()
        {
            _logger = new FakeLogger();
        }

        [Theory]
        [InlineData(0, 200000)]
        [InlineData(200000, 0)]
        public void LogsBondAmountAndValuationRequiredAndSetsRatingTo0Given0BondAmountOr0Valuation(decimal bondAmount, decimal valuation)
        {
            var policy = new Policy
            {
                Type = PolicyType.Flood.ToString(),
                BondAmount = bondAmount,
                Valuation = valuation
            };
            var rating = new FloodPolicyRater(_logger);

            var result = rating.Rate(policy);

            Assert.Equal(0m, result);
            Assert.Equal("Flood policy must specify Bond Amount and Valuation.", _logger.LoggedMessages.Last());
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-1000)]
        public void LogsPolicyNotAvailableMessageAndSetsRatingTo0GivenElevationAtOrBelowSeaLevel(int elevation)
        {
            var policy = new Policy
            {
                Type = PolicyType.Flood.ToString(),
                BondAmount = 200000m,
                Valuation = 200000m,
                ElevationAboveSeaLevelFeet = elevation
            };
            var rating = new FloodPolicyRater(_logger);

            var result = rating.Rate(policy);

            Assert.Equal(0m, result);
            Assert.Equal("Flood policy is not available for elevations at or below sea level.", _logger.LoggedMessages.Last());
        }

        [Fact]
        public void LogsInsufficientBondAmountMessageAndSetsRatingTo0Given200000BondAmountWith260000Valuation()
        {
            var policy = new Policy
            {
                Type = PolicyType.Flood.ToString(),
                BondAmount = 200000m,
                Valuation = 260000m,
                ElevationAboveSeaLevelFeet = 1000
            };
            var rating = new FloodPolicyRater(_logger);

            var result = rating.Rate(policy);

            Assert.Equal(0m, result);
            Assert.Equal("Insufficient bond amount.", _logger.LoggedMessages.Last());
        }

        [Fact]
        public void SetsRatingTo0Given200000BondAmountWith260000Valuation()
        {
            var policy = new Policy
            {
                Type = PolicyType.Flood.ToString(),
                BondAmount = 200000m,
                Valuation = 260000m
            };
            var rating = new FloodPolicyRater(_logger);

            var result = rating.Rate(policy);

            Assert.Equal(0m, result);
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
            var rating = new FloodPolicyRater(_logger);

            var result = rating.Rate(policy);

            Assert.Equal(20000m, result);
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
            var rating = new FloodPolicyRater(_logger);

            var result = rating.Rate(policy);

            Assert.Equal(15000m, result);
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
            var rating = new FloodPolicyRater(_logger);

            var result = rating.Rate(policy);

            Assert.Equal(11000m, result);
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
            var rating = new FloodPolicyRater(_logger);

            var result = rating.Rate(policy);

            Assert.Equal(10000m, result);
        }
    }
}
