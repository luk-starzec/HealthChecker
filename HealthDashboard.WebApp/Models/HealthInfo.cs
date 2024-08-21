namespace HealthDashboard.WebApp.Models;

public record HealthInfo(bool IsHealthy = false, DateTime? LastCheck = null, DateTime? LastHealthy = null);
