using HealthDashboard.WebApp.Models;

namespace HealthDashboard.WebApp.ViewModels;

public record ItemViewModel(string Name, string Label, int Order, EndpointInfo Endpoint);
