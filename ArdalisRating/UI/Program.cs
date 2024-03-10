using ArdalisRating.Core;
using ArdalisRating.Core.Raters;
using ArdalisRating.Infrastructure.Loggers;
using ArdalisRating.Infrastructure.PolicySources;
using ArdalisRating.Infrastructure.Serializers;

namespace ArdalisRating.UI
{
    class Program
    {
        static void Main(string[] args)
        {
            var logger = new FileLogger();

            logger.Log("Ardalis Insurance Rating System Starting...");

            var engine = new RatingEngine(
                logger,
                new FilePolicySource(),
                new JsonPolicySerializer(),
                new RaterFactory(logger));

            engine.Rate();

            if (engine.Rating > 0)
            {
                logger.Log($"Rating: {engine.Rating}");
            }
            else
            {
                logger.Log("No rating produced.");
            }

            logger.Log("");
        }
    }
}
