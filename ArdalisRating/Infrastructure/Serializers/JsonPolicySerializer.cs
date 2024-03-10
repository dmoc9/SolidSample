using ArdalisRating.Core.Interfaces;
using ArdalisRating.Core.Model;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace ArdalisRating.Infrastructure.Serializers
{
    public class JsonPolicySerializer : IPolicySerializer
    {
        public Policy GetPolicyFromString(string policyString)
        {
            return JsonConvert.DeserializeObject<Policy>(policyString, new StringEnumConverter());
        }
    }
}
