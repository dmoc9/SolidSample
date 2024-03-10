using ArdalisRating.Core.Interfaces;
using System.IO;

namespace ArdalisRating.Infrastructure.PolicySources
{
    public class FilePolicySource : IPolicySource
    {
        public string GetPolicyFromSource()
        {
            return File.ReadAllText("policy.json");
        }
    }
}
