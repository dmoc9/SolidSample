using ArdalisRating.Core.Interfaces;
using ArdalisRating.Core.Model;

namespace ArdalisRating.Core.Raters
{
    public abstract class Rater
    {
        public ILogger Logger { get; set; }

        public Rater(ILogger logger)
        {
            Logger = logger;
        }

        public abstract decimal Rate(Policy policy);
    }
}
