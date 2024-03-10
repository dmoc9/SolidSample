using ArdalisRating.Core.Interfaces;

namespace ArdalisRating.Tests.Common.Fakes
{
    public class FakePolicySource : IPolicySource
    {
        public string PolicyString { get; set; } = "";

        public string GetPolicyFromSource()
        {
            return PolicyString;
        }
    }
}
