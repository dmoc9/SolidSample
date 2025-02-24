﻿using ArdalisRating.Core.Interfaces;
using System.Collections.Generic;

namespace ArdalisRating.Tests.Common.Fakes
{
    public class FakeLogger : ILogger
    {
        public List<string> LoggedMessages { get; } = new List<string>();

        public void Log(string message)
        {
            LoggedMessages.Add(message);
        }
    }
}
