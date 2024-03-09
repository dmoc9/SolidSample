using System.Linq;
using Xunit;

namespace ArdalisRating.Tests
{
    public class AutoPolicyRaterRateTests
    {
        [Fact]
        public void LogsMakeRequiredMessageGivenPolicyWithoutMake()
        {
            var policy = new Policy { Type = PolicyType.Auto.ToString() };
            var logger = new FakeLogger();
            var rater = new AutoPolicyRater(null)
            {
                Logger = logger
            };

            rater.Rate(policy);

            Assert.Equal("Auto policy must specify Make", logger.LoggedMessages.Last());
        }

        [Fact]
        public void SetsRatingToNullGivenNonBMWMake()
        {
            var policy = new Policy
            {
                Type = PolicyType.Auto.ToString(),
                Make = "Mercedes"
            };
            var ratingUpdater = new FakeRatingUpdater();
            var rater = new AutoPolicyRater(ratingUpdater);

            rater.Rate(policy);

            Assert.Null(ratingUpdater.NewRating);
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
            var ratingUpdater = new FakeRatingUpdater();
            var rater = new AutoPolicyRater(ratingUpdater);

            rater.Rate(policy);

            Assert.Equal(1000m, ratingUpdater.NewRating.Value);
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
            var ratingUpdater = new FakeRatingUpdater();
            var rater = new AutoPolicyRater(ratingUpdater);

            rater.Rate(policy);

            Assert.Equal(900m, ratingUpdater.NewRating.Value);
        }
    }
}
