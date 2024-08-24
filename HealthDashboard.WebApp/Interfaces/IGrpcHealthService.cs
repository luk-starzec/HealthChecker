namespace HealthDashboard.WebApp.Interfaces;

internal interface IGrpcHealthService
{
    Task<bool> CheckHealthAsync(string address);
}
