using Xunit;

namespace ArdalisRating.Tests
{
    public class JsonPolicySerializerTests
    {
        [Fact]
        public void ReturnsDefaultPolicyFromEmptyJsonString()
        {
            var inputJson = "{}";
            var serializer = new JsonPolicySerializer();

            var result = serializer.GetPolicyFromJsonString(inputJson);

            var policy = new Policy();
            Assert.Equal(policy.Type, result.Type);
        }
    }
}
