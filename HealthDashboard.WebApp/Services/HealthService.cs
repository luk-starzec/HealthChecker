using HealthDashboard.WebApp.Interfaces;
using HealthDashboard.WebApp.ViewModels;

namespace HealthDashboard.WebApp.Services;

public class HealthService : IHealthService
{
    private readonly IApiHealthService _apiHealthService;
    private readonly IGrpcHealthService _grpcHealthService;
    private readonly IWcfHealthService _wcfHealthService;

    public HealthService(IApiHealthService apiHealthService, IGrpcHealthService grpcHealthService, IWcfHealthService wcfHealthService)
    {
        _apiHealthService = apiHealthService;
        _grpcHealthService = grpcHealthService;
        _wcfHealthService = wcfHealthService;
    }

    public async Task<bool> CheckHealthAsync(EndpointInfo endpoint)
    {
        var t = new Random().Next(5, 10);
        await Task.Delay(t * 1000);

        switch (endpoint.Type)
        {
            case ServiceType.Api:
                return await _apiHealthService.CheckHealthAsync(endpoint.Address);
            case ServiceType.Grpc:
                return await _grpcHealthService.CheckHealthAsync(endpoint.Address);
            case ServiceType.Wcf:
                return await Task.Run(() => _wcfHealthService.CheckHealth(endpoint.Address));
            default:
                break;
        }
        return false;
    }


}
