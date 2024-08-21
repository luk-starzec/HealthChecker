namespace HealthDashboard.WebApp.Interfaces;

public interface IIntervalService
{
    public void Start();
    public void Stop();
    public void SetInterval(int seconds);
    public int GetInterval();
    public bool GetEnabled();
}
