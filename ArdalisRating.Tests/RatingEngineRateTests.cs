using Newtonsoft.Json;
using System;
using System.IO;
using Xunit;

namespace ArdalisRating.Tests
{
    public class RatingEngineRateTests
    {
        #region Auto Policy Tests

        [Fact]
        public void ReturnsRatingOf0ForNonBMWMakeAutoPolicy()
        {
            var policy = new Policy
            {
                Type = PolicyType.Auto,
                Make = "Mercedes"
            };
            string json = JsonConvert.SerializeObject(policy);
            File.WriteAllText("policy.json", json);

            var engine = new RatingEngine();
            engine.Rate();
            var result = engine.Rating;

            Assert.Equal(0, result);
        }

        [Fact]
        public void ReturnsRatingOf1000ForBMWMakeAnd200DeductibleAutoPolicy()
        {
            var policy = new Policy
            {
                Type = PolicyType.Auto,
                Make = "BMW",
                Deductible = 200
            };
            string json = JsonConvert.SerializeObject(policy);
            File.WriteAllText("policy.json", json);

            var engine = new RatingEngine();
            engine.Rate();
            var result = engine.Rating;

            Assert.Equal(1000, result);
        }

        [Fact]
        public void ReturnsRatingOf900ForBMWMakeAnd500DeductibleAutoPolicy()
        {
            var policy = new Policy
            {
                Type = PolicyType.Auto,
                Make = "BMW",
                Deductible = 500
            };
            string json = JsonConvert.SerializeObject(policy);
            File.WriteAllText("policy.json", json);

            var engine = new RatingEngine();
            engine.Rate();
            var result = engine.Rating;

            Assert.Equal(900, result);
        }

        #endregion

        #region Land Policy Tests

        [Fact]
        public void ReturnsRatingOf0For200000BondOn260000LandPolicy()
        {
            var policy = new Policy
            {
                Type = PolicyType.Land,
                BondAmount = 200000,
                Valuation = 260000
            };
            string json = JsonConvert.SerializeObject(policy);
            File.WriteAllText("policy.json", json);

            var engine = new RatingEngine();
            engine.Rate();
            var result = engine.Rating;

            Assert.Equal(0, result);
        }

        [Fact]
        public void ReturnsRatingOf10000For200000LandPolicy()
        {
            var policy = new Policy
            {
                Type = PolicyType.Land,
                BondAmount = 200000,
                Valuation = 200000
            };
            string json = JsonConvert.SerializeObject(policy);
            File.WriteAllText("policy.json", json);

            var engine = new RatingEngine();
            engine.Rate();
            var result = engine.Rating;

            Assert.Equal(10000, result);
        }

        #endregion

        #region Life Policy Tests

        [Fact]
        public void ReturnsRatingOf2500For20YearOldNonSmokerOn25000LifePolicy()
        {
            var policy = new Policy
            {
                Type = PolicyType.Life,
                DateOfBirth = DateTime.Now.AddYears(-20),
                Amount = 25000,
                IsSmoker = false
            };
            string json = JsonConvert.SerializeObject(policy);
            File.WriteAllText("policy.json", json);

            var engine = new RatingEngine();
            engine.Rate();
            var result = engine.Rating;

            Assert.Equal(2500, result);
        }

        [Fact]
        public void ReturnsRatingOf5000For20YearOldSmokerOn25000LifePolicy()
        {
            var policy = new Policy
            {
                Type = PolicyType.Life,
                DateOfBirth = DateTime.Now.AddYears(-20),
                Amount = 25000,
                IsSmoker = true
            };
            string json = JsonConvert.SerializeObject(policy);
            File.WriteAllText("policy.json", json);

            var engine = new RatingEngine();
            engine.Rate();
            var result = engine.Rating;

            Assert.Equal(5000, result);
        }

        #endregion

        #region Flood Policy Tests

        [Fact]
        public void ReturnsRatingOf0For200000BondOn260000FloodPolicy()
        {
            var policy = new Policy
            {
                Type = PolicyType.Flood,
                BondAmount = 200000,
                Valuation = 260000
            };
            string json = JsonConvert.SerializeObject(policy);
            File.WriteAllText("policy.json", json);

            var engine = new RatingEngine();
            engine.Rate();
            var result = engine.Rating;

            Assert.Equal(0, result);
        }

        [Fact]
        public void ReturnsRatingOf20000For200000BondWith50ElevationFloodPolicy()
        {
            var policy = new Policy
            {
                Type = PolicyType.Flood,
                BondAmount = 200000,
                Valuation = 200000,
                ElevationAboveSeaLevelFeet = 50
            };
            string json = JsonConvert.SerializeObject(policy);
            File.WriteAllText("policy.json", json);

            var engine = new RatingEngine();
            engine.Rate();
            var result = engine.Rating;

            Assert.Equal(20000, result);
        }

        [Fact]
        public void ReturnsRatingOf15000For200000BondWith250ElevationFloodPolicy()
        {
            var policy = new Policy
            {
                Type = PolicyType.Flood,
                BondAmount = 200000,
                Valuation = 200000,
                ElevationAboveSeaLevelFeet = 250
            };
            string json = JsonConvert.SerializeObject(policy);
            File.WriteAllText("policy.json", json);

            var engine = new RatingEngine();
            engine.Rate();
            var result = engine.Rating;

            Assert.Equal(15000, result);
        }

        [Fact]
        public void ReturnsRatingOf11000For200000BondWith750ElevationFloodPolicy()
        {
            var policy = new Policy
            {
                Type = PolicyType.Flood,
                BondAmount = 200000,
                Valuation = 200000,
                ElevationAboveSeaLevelFeet = 750
            };
            string json = JsonConvert.SerializeObject(policy);
            File.WriteAllText("policy.json", json);

            var engine = new RatingEngine();
            engine.Rate();
            var result = engine.Rating;

            Assert.Equal(11000, result);
        }

        [Fact]
        public void ReturnsRatingOf20000For200000BondWith1000ElevationFloodPolicy()
        {
            var policy = new Policy
            {
                Type = PolicyType.Flood,
                BondAmount = 200000,
                Valuation = 200000,
                ElevationAboveSeaLevelFeet = 1000
            };
            string json = JsonConvert.SerializeObject(policy);
            File.WriteAllText("policy.json", json);

            var engine = new RatingEngine();
            engine.Rate();
            var result = engine.Rating;

            Assert.Equal(10000, result);
        }

        #endregion
    }
}
