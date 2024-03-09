using System;
using System.Linq;
using Xunit;

namespace ArdalisRating.Tests
{
    public class LifePolicyRaterRateTests
    {
        private readonly FakeLogger _logger;

        public LifePolicyRaterRateTests()
        {
            _logger = new FakeLogger();
        }

        [Fact]
        public void LogsCentenariansNotEligibleMessageAndSetsRatingTo0GivenDateOfBirthIsOver100YearsAgo()
        {
            var policy = new Policy
            {
                Type = PolicyType.Life.ToString(),
                DateOfBirth = DateTime.Now.AddYears(-100).AddDays(-1)
            };
            var rater = new LifePolicyRater(_logger);

            var result = rater.Rate(policy);

            Assert.Equal(0m, result);
            Assert.Equal("Centenarians are not eligible for coverage.", _logger.LoggedMessages.Last());
        }

        [Fact]
        public void LogsAmountRequiredMessageAndSetsRatingTo0Given0Amount()
        {
            var policy = new Policy
            {
                Type = PolicyType.Life.ToString(),
                DateOfBirth = DateTime.Now.AddYears(-20),
                Amount = 0m
            };
            var rater = new LifePolicyRater(_logger);

            var result = rater.Rate(policy);

            Assert.Equal(0m, result);
            Assert.Equal("Life policy must include an Amount.", _logger.LoggedMessages.Last());
        }

        [Fact]
        public void SetsRatingTo2500Given20YearOldNonSmokerOn25000()
        {
            var policy = new Policy
            {
                Type = PolicyType.Life.ToString(),
                DateOfBirth = DateTime.Now.AddYears(-20),
                Amount = 25000m,
                IsSmoker = false
            };
            var rater = new LifePolicyRater(_logger);

            var result = rater.Rate(policy);

            Assert.Equal(2500m, result);
        }

        [Fact]
        public void SetsRatingTo5000Given20YearOldSmokerOn25000()
        {
            var policy = new Policy
            {
                Type = PolicyType.Life.ToString(),
                DateOfBirth = DateTime.Now.AddYears(-20),
                Amount = 25000m,
                IsSmoker = true
            };
            var rater = new LifePolicyRater(_logger);

            var result = rater.Rate(policy);

            Assert.Equal(5000m, result);
        }
    }
}
