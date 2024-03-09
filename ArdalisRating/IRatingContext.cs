namespace ArdalisRating
{
    public interface IRatingContext : ILogger
    {
        RatingEngine Engine { get; set; }
        Rater CreateRaterForPolicy(Policy policy);
        Policy GetPolicyFromJsonString(string policyJson);
        Policy GetPolicyFromXmlString(string policyXml);
        string LoadPolicyFromFile();
        string LoadPolicyFromURI(string uri);
    }
}
