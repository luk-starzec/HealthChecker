using HealthDashboard.WebApp.ViewModels;

namespace HealthDashboard.WebApp.Interfaces;

public interface IHealthService
{
    public Task<bool> CheckHealthAsync(EndpointInfo endpoint);
}
