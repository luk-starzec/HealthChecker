using HealthDashboard.WebApp.Interfaces;
using HealthDashboard.WebApp.ViewModels;

namespace HealthDashboard.WebApp.Services;


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
        _historyService = historyService;
        _httpClient = httpClient;
    }

    public EventHandler<ItemViewModel>? ItemUpdated { get; set; }

    public async Task<GroupViewModel[]> GetGroupsAsync()
    {
        var groups = await _httpClient.GetFromJsonAsync<GroupViewModel[]>("data/items.json");

        if (groups == null)
            return [];

        return groups;
    }

    public void InitItemFromHistory(ItemViewModel item)
    {
        var logs = _historyService.GetLogs(item.Name);

        if (!logs.Any())
            return;

        var last = logs.OrderByDescending(r => r.Key).First();
        item.IsHealthy = last.Value;
        item.LastCheck = last.Key;

        if (logs.Where(r => r.Value).Any())
        {
            var lastHealthy = logs.Where(r => r.Value).OrderByDescending(r => r.Key).First();
            item.LastHealthy = lastHealthy.Key;
        }
    }

    public async Task UpdateHealthAsync(ItemViewModel item)
    {
        item.IsChecking = true;
        ItemUpdated?.Invoke(null, item);

        var time = DateTime.Now;

        await Task.Delay(2000);

        if (_isDemoMode)
        {
            var r = _rnd.Next(0, 2);
            item.IsHealthy = (r > 0);
        }
        else
        {
            item.IsHealthy = await GetHealthAsync(item.Type, item.Address);
        }

        item.LastCheck = time;
        if (item.IsHealthy)
            item.LastHealthy = time;

        item.IsChecking = false;
        ItemUpdated?.Invoke(null, item);

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
                return _wcfHealthService.CheckHealth(address);
            default:
                break;
        }
        return false;
    }
}
