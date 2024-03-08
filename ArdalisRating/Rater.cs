namespace ArdalisRating
{
    public abstract class Rater
    {
        protected readonly IRatingContext _context;
        public ILogger Logger { get; set; } = new ConsoleLogger();

        public Rater(IRatingContext context)
        {
            _context = context;
        }

        public abstract void Rate(Policy policy);
    }
}
