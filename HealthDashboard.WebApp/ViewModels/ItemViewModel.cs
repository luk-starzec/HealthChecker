namespace HealthDashboard.WebApp.ViewModels;

public class ItemViewModel
{
    public required string Name { get; set; }
    public required string Label { get; set; }
    public int Order { get; set; }
    public required string Address { get; set; }
    public ServiceType Type { get; set; }
    public bool IsHealthy { get; set; }
    public bool IsChecking { get; set; }
    public DateTime? LastCheck { get; set; }
    public DateTime? LastHealthy { get; set; }

    public string LastCheckText => LastCheck.HasValue ? DateTimeToText(LastCheck.Value) : "";
    public string LastHealthyText => LastHealthy.HasValue ? DateTimeToText(LastHealthy.Value) : "";

    private static string DateTimeToText(DateTime dateTime)
    {
        var format = dateTime.Date == DateTime.Today ? "HH:mm:ss" : "yyyy-MM-dd HH:mm";
        return dateTime.ToString(format);
    }

}
