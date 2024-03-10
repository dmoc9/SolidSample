using ArdalisRating.Core.Interfaces;
using ArdalisRating.Core.Model;

namespace ArdalisRating.Core.Raters
{
    public class UnknownPolicyRater : Rater
    {
        public UnknownPolicyRater(ILogger logger)
            : base(logger)
        {
        }

        public override decimal Rate(Policy policy)
        {
            Logger.Log("Unknown policy type");
            return 0m;
        }
    }
}
