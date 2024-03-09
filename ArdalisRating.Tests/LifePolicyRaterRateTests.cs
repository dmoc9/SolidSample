using System;
using System.Linq;
using Xunit;

namespace ArdalisRating.Tests
{
    public class LifePolicyRaterRateTests
    {
        [Fact]
        public void LogsCentenariansNotEligibleMessageGivenDateOfBirthIsOver100YearsAgo()
        {
            var policy = new Policy
            {
                Type = PolicyType.Life.ToString(),
                DateOfBirth = DateTime.Now.AddYears(-100).AddDays(-1)
            };
            var logger = new FakeLogger();
            var rater = new LifePolicyRater(null)
            {
                Logger = logger
            };

            rater.Rate(policy);

            Assert.Equal("Centenarians are not eligible for coverage.", logger.LoggedMessages.Last());
        }

        [Fact]
        public void LogsAmountRequiredMessageGiven0Amount()
        {
            var policy = new Policy
            {
                Type = PolicyType.Life.ToString(),
                DateOfBirth = DateTime.Now.AddYears(-20),
                Amount = 0m
            };
            var logger = new FakeLogger();
            var rater = new LifePolicyRater(null)
            {
                Logger = logger
            };

            rater.Rate(policy);

            Assert.Equal("Life policy must include an Amount.", logger.LoggedMessages.Last());
        }

        [Fact]
        public void ReturnsRatingOf2500Given20YearOldNonSmokerOn25000()
        {
            var policy = new Policy
            {
                Type = PolicyType.Life.ToString(),
                DateOfBirth = DateTime.Now.AddYears(-20),
                Amount = 25000m,
                IsSmoker = false
            };
            var ratingUpdater = new FakeRatingUpdater();
            var rater = new LifePolicyRater(ratingUpdater);

            rater.Rate(policy);

            Assert.Equal(2500m, ratingUpdater.NewRating.Value);
        }

        [Fact]
        public void ReturnsRatingOf5000Given20YearOldSmokerOn25000()
        {
            var policy = new Policy
            {
                Type = PolicyType.Life.ToString(),
                DateOfBirth = DateTime.Now.AddYears(-20),
                Amount = 25000m,
                IsSmoker = true
            };
            var ratingUpdater = new FakeRatingUpdater();
            var rater = new LifePolicyRater(ratingUpdater);

            rater.Rate(policy);

            Assert.Equal(5000m, ratingUpdater.NewRating.Value);
        }
    }
}
