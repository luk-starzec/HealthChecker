using HealthDashboard.WebApp.Interfaces;

namespace HealthDashboard.WebApp.Services;

public class WatchDogBackgroundService : BackgroundService
{
    private readonly IIntervalService _intervalService;

    public WatchDogBackgroundService(IIntervalService intervalService)
    {
        _intervalService = intervalService;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        await Task.Run(_intervalService.Start, stoppingToken);
    }
}
