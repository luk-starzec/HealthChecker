namespace HealthDashboard.Web.ViewModel;

public record GroupViewModel
{
    public required string Name { get; init; }
    public required string Label { get; init; }
    public int Order { get; init; }
    public List<ItemViewModel> Items { get; init; } = [];
}
