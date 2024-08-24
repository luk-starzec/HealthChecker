using HealthDashboard.WebApp.Models;

namespace HealthDashboard.WebApp.Interfaces;

internal interface IHealthService
{
    public Task CheckHealthAsync(string name, EndpointInfo endpoint);
    public void RegisterEndpoint(string name, EndpointInfo endpoint);
}
