using HealthDashboard.WebApp.ViewModels;

namespace HealthDashboard.WebApp.Models;

public record ItemConfiguration
{
    public required string Name { get; init; }
    public required string Label { get; init; }
    public int Order { get; init; }
    public required string Address { get; init; }
    public ServiceType Type { get; init; }

    public ItemViewModel ToViewModel()
    {
        return new ItemViewModel(Name, Label, Order, new EndpointInfo(Type, Address));
    }
}
