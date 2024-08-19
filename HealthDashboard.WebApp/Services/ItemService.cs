using HealthDashboard.WebApp.Interfaces;
using HealthDashboard.WebApp.ViewModels;

namespace HealthDashboard.WebApp.Services;


public class ItemService : IItemService
{
    private readonly IHealthService _healthService;
    private readonly HttpClient _httpClient;
    private readonly IHistoryService _historyService;

    private readonly bool _isDemoMode = false;
    private readonly Random _rnd = new();

    public ItemService(IHealthService healthService, HttpClient httpClient, IHistoryService historyService)
    {
        _historyService = historyService;
        _healthService = healthService;
        _httpClient = httpClient;
    }

    public Action<string, bool, DateTime>? OnHealthChecked { get; set; }
    public Action<string>? OnHealthChecking { get; set; }

    public async Task<GroupViewModel[]> GetGroupsAsync()
    {
        var configurations = await _httpClient.GetFromJsonAsync<GroupConfiguration[]>("data/items.json");

        if (configurations == null)
            return [];

        var viewModels = configurations.Select(r => r.ToViewModel()).ToArray();
        return viewModels;
    }

    public async Task CheckHealthAsync(string name, EndpointInfo endpoint)
    {
        OnHealthChecking?.Invoke(name);

        var isHealthy = await _healthService.CheckHealthAsync(endpoint);
        var time = DateTime.Now;
        OnHealthChecked?.Invoke(name, isHealthy, time);

        _historyService.AddLog(name, time, isHealthy);
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
