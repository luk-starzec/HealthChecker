namespace HealthDashboard.WebApp.Interfaces;

public interface IGrpcHealthService
{
    Task<bool> CheckHealthAsync(string address);
}
