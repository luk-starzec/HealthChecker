namespace HealthDashboard.WebApp.ViewModels;

public record GroupViewModel(string Name, string Label, int Order, List<ItemViewModel> Items);
