﻿using System;

namespace ArdalisRating
{
    public class DefaultRatingContext : IRatingContext
    {
        public RatingEngine Engine { get; set; }

        private readonly IPolicySource _policySource;
        private readonly IPolicySerializer _policySerializer;

        public DefaultRatingContext(
            IPolicySource policySource,
            IPolicySerializer policySerializer)
        {
            _policySource = policySource;
            _policySerializer = policySerializer;
        }

        public Rater CreateRaterForPolicy(Policy policy, IRatingContext context)
        {
            return new RaterFactory().Create(policy, context);
        }

        public Policy GetPolicyFromJsonString(string policyJson)
        {
            return _policySerializer.GetPolicyFromString(policyJson);
        }

        public Policy GetPolicyFromXmlString(string policyXml)
        {
            throw new NotImplementedException();
        }

        public string LoadPolicyFromFile()
        {
            return _policySource.GetPolicyFromSource();
        }

        public string LoadPolicyFromURI(string uri)
        {
            throw new NotImplementedException();
        }

        public void Log(string message)
        {
            new ConsoleLogger().Log(message);
        }
    }
}
