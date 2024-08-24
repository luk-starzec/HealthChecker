namespace HealthDashboard.WebApp.Interfaces;

internal interface IWcfHealthService 
{
    bool CheckHealth(string address);
}
