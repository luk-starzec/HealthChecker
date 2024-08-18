using HealthDashboard.WebApp.ViewModels;

namespace HealthDashboard.WebApp.Interfaces;

public interface IItemService
{
    public Task UpdateHealthAsync(ItemViewModel item);
    public Task<GroupViewModel[]> GetGroupsAsync();
}
