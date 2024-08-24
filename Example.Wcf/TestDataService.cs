using Shared.Wcf;
using System;
using System.ServiceModel;

namespace Example.Wcf
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single)]
    public class TestDataService : ITestDataService, IWcfHealthCheck
    {
        public string GetData()
        {
            return $"Test data generated at {DateTime.Now}";
        }

        public bool HealthCheck()
        {
            Console.WriteLine($"HealthCheck called at {DateTime.Now}");
            return true;
        }
    }
}
