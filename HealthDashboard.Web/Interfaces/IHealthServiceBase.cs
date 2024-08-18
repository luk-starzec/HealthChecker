namespace HealthDashboard.Web.Interfaces;

public interface IHealthServiceBase
{
    Task<bool> CheckHealthAsync(string address);
}