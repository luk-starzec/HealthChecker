namespace HealthDashboard.WebApp.Interfaces;

internal interface IApiHealthService
{
    Task<bool> CheckHealthAsync(string address);
}
