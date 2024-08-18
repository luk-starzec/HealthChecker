using Grpc.Health.V1;
using Grpc.Net.Client;
using HealthDashboard.WebApp.Interfaces;

namespace HealthDashboard.WebApp.Services;

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

        var channel = GrpcChannel.ForAddress(address);
        _channels.Add(address, channel);

        return channel;
    }
}
