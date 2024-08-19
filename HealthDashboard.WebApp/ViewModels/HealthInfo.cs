namespace HealthDashboard.WebApp.ViewModels;

public record HealthInfo(bool IsHealthy = false, DateTime? LastCheck = null, DateTime? LastHealthy = null);
