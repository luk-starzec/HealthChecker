using HealthDashboard.WebApp.Interfaces;
using System.Collections.Concurrent;

namespace HealthDashboard.WebApp.Services;

public class HistoryService : IHistoryService, IDisposable
{
    private readonly ConcurrentDictionary<string, Dictionary<DateTime, bool>> _history = [];
    private readonly EventBus _eventBus;

    public HistoryService(EventBus eventBus)
    {
        _eventBus = eventBus;
        _eventBus.OnHealthChecked += HandleHealthChecked;
    }

    public void Dispose()
    {
        _eventBus.OnHealthChecked -= HandleHealthChecked;
    }

    public void AddLog(string itemName, DateTime time, bool isHealthy)
    {
        if (!_history.ContainsKey(itemName))
            _history.TryAdd(itemName, []);

        if (_history.TryGetValue(itemName, out var history))
        {
            history.Add(time, isHealthy);

            if (history.Count > 15)
                CleanUpItemLogs(itemName);
        }
    }

    public Dictionary<DateTime, bool> GetLogs(string itemName)
    {
        return _history.TryGetValue(itemName, out var history) ? history : [];
    }

    private void CleanUpItemLogs(string itemName)
    {
        var logs = GetLogs(itemName);
        var keysToRemove = logs.OrderByDescending(r => r.Key).Select(r => r.Key).Skip(10);

        foreach (var key in keysToRemove)
        {
            logs.Remove(key);
        }
    }

    private void HandleHealthChecked(string name, bool isHealthy, DateTime time)
    {
        AddLog(name, time, isHealthy);
    }
}
