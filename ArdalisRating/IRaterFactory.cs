namespace ArdalisRating
{
    public interface IRaterFactory
    {
        Rater Create(Policy policy);
    }
}