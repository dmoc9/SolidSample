using ArdalisRating.Core.Interfaces;
using ArdalisRating.Core.Model;
using System;

namespace ArdalisRating.Core.Raters
{
    public class RaterFactory
    {
        private readonly ILogger _logger;

        public RaterFactory(ILogger logger)
        {
            _logger = logger;
        }

        public Rater Create(Policy policy)
        {
            try
            {
                return (Rater)Activator.CreateInstance(
                    Type.GetType($"ArdalisRating.Core.Raters.{policy.Type}PolicyRater"),
                    new object[] { _logger });
            }
            catch
            {
                return new UnknownPolicyRater(_logger);
            }
        }
    }
}
