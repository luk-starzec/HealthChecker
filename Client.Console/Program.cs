using Grpc.Health.V1;
using Grpc.Net.Client;
using Shared.Wcf;
using System.ServiceModel;

Console.WriteLine("App started");

GetWcfHealth();
await GetGrpcHealth();
await GetApiHealth();

Console.ReadLine();

void GetWcfHealth()
{
    var baseAddress = new Uri("net.tcp://localhost:9001/TestService/hc");
    var binding = new NetTcpBinding(SecurityMode.None, false);
    var endpoint = new EndpointAddress(baseAddress);
    var channel = new ChannelFactory<IWcfHealthCheck>(binding, endpoint);
    var proxy = channel.CreateChannel(endpoint);

    var result = proxy?.HealthCheck();
    Console.WriteLine($"WCF Health: {result}");

    channel.Close();
}

async Task GetGrpcHealth()
{
    var channel = GrpcChannel.ForAddress("https://localhost:8001");
    var client = new Health.HealthClient(channel);

    var response = await client.CheckAsync(new HealthCheckRequest());
    var status = response.Status;

    Console.WriteLine($"gRPC Health: {status}");
}

async Task GetApiHealth()
{
    using HttpClient client = new();
    client.BaseAddress = new Uri("https://localhost:7001");
    var result = await client.GetAsync("hc");
    var status = result.IsSuccessStatusCode;
    Console.WriteLine($"API Health: {status}");
}