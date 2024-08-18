namespace HealthDashboard.WebApp.Interfaces;

public interface IApiHealthService
{
    Task<bool> CheckHealthAsync(string address);
}
