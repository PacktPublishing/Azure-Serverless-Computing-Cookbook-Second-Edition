using System;
using Microsoft.Azure.EventHubs;
using System.Configuration;

namespace EventHubApp
{
    class Program
    {
        static void Main(string[] args)
        {
            EventHubHelper.GenerateEventHubMessages().Wait();
        }
    }
}
