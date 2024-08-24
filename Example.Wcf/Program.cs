using Shared.Utils;
using Shared.Wcf;
using System;
using System.ServiceModel;

namespace Example.Wcf
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var port = GetPort(args);
            var baseAddress = new Uri($"net.tcp://localhost:{port}/TestService");
            var host = new ServiceHost(typeof(TestDataService), baseAddress);

            try
            {
                var binding = new NetTcpBinding(SecurityMode.None, false);
                host.AddServiceEndpoint(typeof(ITestDataService), binding, "");
                host.AddServiceEndpoint(typeof(IWcfHealthCheck), binding, "hc");

                host.Open();
                Console.WriteLine("Service endpoint:");
                Console.WriteLine(baseAddress);
                Console.WriteLine();
                Console.WriteLine("Press <Enter> to terminate the service.");
                Console.ReadLine();

                host.Close();
            }
            catch (CommunicationException ce)
            {
                Console.WriteLine("An exception occurred: {0}", ce.Message);
                host.Abort();
            }
        }

        private static string GetPort(string[] args)
        {
            var arguments = Helpers.GetArguments(args);
            return arguments.ContainsKey("port") ? arguments["port"] : "9001";
        }
    }
}
