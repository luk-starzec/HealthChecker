namespace HealthDashboard.WebApp.ViewModels;

public record GroupConfiguration
{
    public required string Name { get; init; }
    public required string Label { get; init; }
    public int Order { get; init; }
    public List<ItemConfiguration> Items { get; init; } = [];

    public GroupViewModel ToViewModel()
    {
        return new GroupViewModel
        {
            Name = Name,
            Label = Label,
            Order = Order,
            Items = Items.Select(x => x.ToViewModel()).ToList(),
        };
    }
}
