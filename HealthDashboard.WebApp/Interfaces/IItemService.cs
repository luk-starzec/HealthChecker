using HealthDashboard.WebApp.ViewModels;

namespace HealthDashboard.WebApp.Interfaces;

public interface IItemService
{
    public Task<GroupViewModel[]> GetGroupsAsync();
    public Task UpdateHealthAsync(ItemViewModel item);
    public void InitItemFromHistory(ItemViewModel item);
    public EventHandler<ItemViewModel>? ItemUpdated { get; set; }

}
