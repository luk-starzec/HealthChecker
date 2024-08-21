using HealthDashboard.WebApp.Models;

namespace HealthDashboard.WebApp.Interfaces;

public interface IHealthService
{
    public Task CheckHealthAsync(string name, EndpointInfo endpoint);
    public void RegisterEndpoint(string name, EndpointInfo endpoint);
}
