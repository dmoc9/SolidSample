using Newtonsoft.Json;
using System.IO;
using Xunit;

namespace ArdalisRating.Tests
{
    public class RatingEngineRateTests
    {
        [Fact]
        public void SetsRatingTo10000For200000LandPolicy()
        {
            var policy = new Policy
            {
                Type = PolicyType.Land.ToString(),
                BondAmount = 200000m,
                Valuation = 200000m
            };
            string json = JsonConvert.SerializeObject(policy);
            File.WriteAllText("policy.json", json);

            var engine = new RatingEngine();
            engine.Rate();
            var result = engine.Rating;

            Assert.Equal(10000m, result);
        }

        [Fact]
        public void SetsRatingTo0For200000BondOn260000LandPolicy()
        {
            var policy = new Policy
            {
                Type = PolicyType.Land.ToString(),
                BondAmount = 200000m,
                Valuation = 260000m
            };
            string json = JsonConvert.SerializeObject(policy);
            File.WriteAllText("policy.json", json);

            var engine = new RatingEngine();
            engine.Rate();
            var result = engine.Rating;

            Assert.Equal(0m, result);
        }
    }
}
