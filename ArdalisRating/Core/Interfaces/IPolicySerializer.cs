using ArdalisRating.Core.Model;

namespace ArdalisRating.Core.Interfaces
{
    public interface IPolicySerializer
    {
        Policy GetPolicyFromString(string policyString);
    }
}