namespace HealthDashboard.WebApp.Services;

internal sealed class EventBus
{
    public Func<Task>? OnElapsed { get; set; }
    public Action<int>? OnIntervalChanged { get; set; }
    public Action<bool>? OnEnabledChanged { get; set; }
    public Action<string, bool, DateTime>? OnHealthChecked { get; set; }
    public Action<string>? OnHealthChecking { get; set; }
    public Action<string>? OnHistoryChanged { get; set; }
}
