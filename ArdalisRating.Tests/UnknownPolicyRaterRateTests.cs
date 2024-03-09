﻿using System.Linq;
using Xunit;

namespace ArdalisRating.Tests
{
    public class UnknownPolicyRaterRateTests
    {
        private readonly FakeLogger _logger;

        public UnknownPolicyRaterRateTests()
        {
            _logger = new FakeLogger();
        }

        [Fact]
        public void LogsUnknownPolicyTypeMessageAndSetsRatingTo0()
        {
            var policy = new Policy
            {
                Type = "Foo"
            };
            var rating = new UnknownPolicyRater(_logger);

            var result = rating.Rate(policy);

            Assert.Equal(0m, result);
            Assert.Equal("Unknown policy type", _logger.LoggedMessages.Last());
        }
    }
}
