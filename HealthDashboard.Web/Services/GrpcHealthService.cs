using Grpc.Health.V1;
using Grpc.Net.Client;
using Grpc.Net.Client.Web;
using HealthDashboard.Web.Interfaces;

namespace HealthDashboard.Web.Services;

public class GrpcHealthService : IGrpcHealthService
{
    private static readonly Dictionary<string, GrpcChannel> _channels = [];

    public async Task<bool> CheckHealthAsync(string address)
    {
        try
        {
            var channel = GetChannel(address);

            var client = new Health.HealthClient(channel);

            var response = await client.CheckAsync(new HealthCheckRequest());
            var status = response.Status;

            return status == HealthCheckResponse.Types.ServingStatus.Serving;
        }
        catch (Exception)
        {
            return false;
        }
    }

    private GrpcChannel GetChannel(string address)
    {
        if (_channels.ContainsKey(address))
            return _channels[address];

        var httpClient = new HttpClient(new GrpcWebHandler(GrpcWebMode.GrpcWeb, new HttpClientHandler()));
        var channel = GrpcChannel.ForAddress(address, new GrpcChannelOptions { HttpClient = httpClient });

        _channels.Add(address, channel);

        return channel;
    }
}
