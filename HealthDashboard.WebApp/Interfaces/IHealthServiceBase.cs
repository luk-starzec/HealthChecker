namespace HealthDashboard.WebApp.Interfaces;

public interface IHealthServiceBase
{
    Task<bool> CheckHealthAsync(string address);
}