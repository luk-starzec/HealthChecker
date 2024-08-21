using HealthDashboard.WebApp.Interfaces;
using HealthDashboard.WebApp.Models;

namespace HealthDashboard.WebApp.Services;

public class HealthService : IHealthService, IDisposable
{
    private readonly IApiHealthService _apiHealthService;
    private readonly IGrpcHealthService _grpcHealthService;
    private readonly IWcfHealthService _wcfHealthService;
    private readonly EventBus _eventBus;

    private Dictionary<string, EndpointInfo> _endpoints = [];

    private readonly bool _useRandomResult = false;
    private readonly bool _useRandomDelay = false;
    private readonly Random _rnd = new();

    public HealthService(IApiHealthService apiHealthService, IGrpcHealthService grpcHealthService, IWcfHealthService wcfHealthService, EventBus eventBus)
    {
        _apiHealthService = apiHealthService;
        _grpcHealthService = grpcHealthService;
        _wcfHealthService = wcfHealthService;
        _eventBus = eventBus;

        _eventBus.OnElapsed += CheckHealthAllAsync;
    }

    public void Dispose()
    {
        _eventBus.OnElapsed -= CheckHealthAllAsync;
    }

    public void RegisterEndpoint(string name, EndpointInfo endpoint)
    {
        if (!_endpoints.ContainsKey(name))
            _endpoints.Add(name, endpoint);
    }

    public async Task CheckHealthAsync(string name, EndpointInfo endpoint)
    {
        _eventBus.OnHealthChecking?.Invoke(name);

        var isHealthy = await CheckHealthAsync(endpoint);
        var time = DateTime.Now;
        _eventBus.OnHealthChecked?.Invoke(name, isHealthy, time);
    }

    private async Task<bool> CheckHealthAsync(EndpointInfo endpoint)
    {
        if (_useRandomDelay)
        {
            var t = _rnd.Next(1, 5);
            await Task.Delay(t * 1000);
        }
        if (_useRandomResult)
            return _rnd.Next(0, 2) > 0;

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

    private async Task CheckHealthAllAsync()
    {
        await Parallel.ForEachAsync(_endpoints, async (e, _) => await CheckHealthAsync(e.Key, e.Value));
    }

}
