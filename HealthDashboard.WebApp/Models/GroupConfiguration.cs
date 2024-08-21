using HealthDashboard.WebApp.ViewModels;

namespace HealthDashboard.WebApp.Models;

public record GroupConfiguration
{
    public required string Name { get; init; }
    public required string Label { get; init; }
    public int Order { get; init; }
    public List<ItemConfiguration> Items { get; init; } = [];

    public GroupViewModel ToViewModel()
    {
        return new GroupViewModel(Name, Label, Order, Items.Select(x => x.ToViewModel()).ToList());
    }
}
