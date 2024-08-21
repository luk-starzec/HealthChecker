using HealthDashboard.WebApp.Interfaces;
using HealthDashboard.WebApp.Models;
using HealthDashboard.WebApp.ViewModels;

namespace HealthDashboard.WebApp.Services;

public class ItemService : IItemService
{
    private readonly IHealthService _healthService;
    private readonly IHistoryService _historyService;
    private readonly HttpClient _httpClient;

    public ItemService(IHealthService healthService, HttpClient httpClient, IHistoryService historyService)
    {
        _historyService = historyService;
        _healthService = healthService;
        _httpClient = httpClient;
    }

    public async Task<GroupViewModel[]> GetGroupsAsync()
    {
        var configurations = await _httpClient.GetFromJsonAsync<GroupConfiguration[]>("data/items1.json");

        if (configurations == null)
            return [];

        foreach (var item in configurations.SelectMany(r => r.Items))
        {
            _healthService.RegisterEndpoint(item.Name, new EndpointInfo(item.Type, item.Address));
        }

        var viewModels = configurations.Select(r => r.ToViewModel()).ToArray();
        return viewModels;
    }

    public async Task CheckHealthAsync(string name, EndpointInfo endpoint)
    {
        await _healthService.CheckHealthAsync(name, endpoint);
    }

    public HealthInfo? GetHealthFromHistory(string name)
    {
        var logs = _historyService.GetLogs(name);

        if (!logs.Any())
            return null;

        var lastLog = logs.OrderByDescending(r => r.Key).First();
        var isHealthy = lastLog.Value;
        var lastCheck = lastLog.Key;

        DateTime? lastHealthy = null;
        if (logs.Where(r => r.Value).Any())
        {
            var lastHealthyLog = logs.Where(r => r.Value).OrderByDescending(r => r.Key).First();
            lastHealthy = lastHealthyLog.Key;
        }

        return new HealthInfo(isHealthy, lastCheck, lastHealthy);
    }
}
