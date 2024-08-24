namespace HealthDashboard.WebApp.Interfaces;

internal interface IHistoryService
{
    public void AddLog(string itemName, DateTime time, bool isHealthy);
    public Dictionary<DateTime, bool> GetLogs(string itemName);
}
