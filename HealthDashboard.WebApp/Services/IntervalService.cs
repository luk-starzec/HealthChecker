using HealthDashboard.WebApp.Interfaces;
using Timer = System.Timers.Timer;

namespace HealthDashboard.WebApp.Services;

public class IntervalService : IIntervalService, IDisposable
{
    private readonly Timer _timer = new();
    private readonly EventBus _eventBus;

    public IntervalService(EventBus eventBus)
    {
        _eventBus = eventBus;

        _timer = new()
        {
            Interval = 10_000,
            AutoReset = true,
        };
        _timer.Elapsed += TimerElapsed;
    }

    public void Dispose()
    {
        _timer.Elapsed -= TimerElapsed;
        _timer.Dispose();
    }

    public void Start()
    {
        _timer.Start();
        _eventBus.OnEnabledChanged?.Invoke(true);
    }

    public void Stop()
    {
        _timer.Stop();
        _eventBus.OnEnabledChanged?.Invoke(false);
    }

    public void SetInterval(int seconds)
    {
        if (seconds <= 0)
            throw new ArgumentException("Interval must be grater then 0", nameof(seconds));

        _timer.Interval = seconds * 1000;
        _eventBus.OnIntervalChanged?.Invoke(seconds);
    }

    public int GetInterval()
    {
        return (int)(_timer.Interval / 1000);
    }

    public bool GetEnabled()
    {
        return _timer.Enabled;
    }

    private void TimerElapsed(object? sender, EventArgs e)
    {
        _eventBus.OnElapsed?.Invoke();
    }
}