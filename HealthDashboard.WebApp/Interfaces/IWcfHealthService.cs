namespace HealthDashboard.WebApp.Interfaces;

public interface IWcfHealthService 
{
    bool CheckHealth(string address);
}
