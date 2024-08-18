using HealthDashboard.Web.ViewModel;

namespace HealthDashboard.Web.Interfaces;

public interface IItemService
{
    public Task UpdateHealthAsync(ItemViewModel item);
    public Task<GroupViewModel[]> GetGroupsAsync();
}
