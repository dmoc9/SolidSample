using System.Linq;
using Xunit;

namespace ArdalisRating.Tests
{
    public class UnknownPolicyRaterRateTests
    {
        [Fact]
        public void LogsUnknownPolicyTypeMessage()
        {
            var policy = new Policy
            {
                Type = "Foo"
            };
            var logger = new FakeLogger();
            var rating = new UnknownPolicyRater(null)
            {
                Logger = logger
            };

            rating.Rate(policy);

            Assert.Equal("Unknown policy type", logger.LoggedMessages.Last());
        }
    }
}
