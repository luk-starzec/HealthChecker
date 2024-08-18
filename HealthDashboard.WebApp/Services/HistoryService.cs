using HealthDashboard.WebApp.Interfaces;
using HealthDashboard.WebApp.ViewModels;

namespace HealthDashboard.WebApp.Services;

public class HistoryService : IHistoryService
{
    private readonly Dictionary<string, Dictionary<DateTime, bool>> _history = [];

    public void AddLog(string itemName, DateTime time, bool isHealthy)
    {
        if (!_history.ContainsKey(itemName))
            _history.Add(itemName, new Dictionary<DateTime, bool>());

        var history = _history[itemName];
        history.Add(time, isHealthy);
    }

    public Dictionary<DateTime, bool> GetLogs(string itemName)
    {
        return _history[itemName];
    }
}
