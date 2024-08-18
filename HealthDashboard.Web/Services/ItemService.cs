using HealthDashboard.Web.Interfaces;
using HealthDashboard.Web.ViewModel;
using System.Net.Http.Json;

namespace HealthDashboard.Web.Services;

public class ItemService : IItemService
{
    private readonly IApiHealthService _apiHealthService;
    private readonly IGrpcHealthService _grpcHealthService;
    private readonly IWcfHealthService _wcfHealthService;
    private readonly HttpClient _httpClient;
    private readonly IHistoryService _historyService;

    private readonly bool _isDemoMode = false;
    private readonly Random _rnd = new();

    public ItemService(IApiHealthService apiHealthService, IGrpcHealthService grpcHealthService, IWcfHealthService wcfHealthService, HttpClient httpClient, IHistoryService historyService)
    {
        _apiHealthService = apiHealthService;
        _grpcHealthService = grpcHealthService;
        _wcfHealthService = wcfHealthService;
        _httpClient = httpClient;
        _historyService = historyService;
    }

    public async Task<GroupViewModel[]> GetGroupsAsync()
    {
        var groups = await _httpClient.GetFromJsonAsync<GroupViewModel[]>("data/items.json");

        if (groups == null)
            return [];

        return groups;
    }

    public async Task UpdateHealthAsync(ItemViewModel item)
    {
        item.IsChecking = true;

        var time = DateTime.Now;

        if (_isDemoMode)
        {
            var r = _rnd.Next(0, 2);
            item.IsHealthy = (r > 0);
        }
        else
        {
            item.IsHealthy = await GetHealthAsync(item.Type, item.Address);
        }

        item.IsInitialized = true;
        item.LastCheck = time;
        if (item.IsHealthy)
            item.LastHealthy = time;

        item.IsChecking = false;

        _historyService.AddLog(item.Name, time, item.IsHealthy);
    }

    private async Task<bool> GetHealthAsync(ServiceType serviceType, string address)
    {
        switch (serviceType)
        {
            case ServiceType.Api:
                return await _apiHealthService.CheckHealthAsync(address);
            case ServiceType.Grpc:
                return await _grpcHealthService.CheckHealthAsync(address);
            case ServiceType.Wcf:
                return await _wcfHealthService.CheckHealthAsync(address);
            default:
                break;
        }
        return false;
    }
}
