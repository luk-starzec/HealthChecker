using HealthDashboard.WebApp.Models;
using HealthDashboard.WebApp.ViewModels;

namespace HealthDashboard.WebApp.Interfaces;

internal interface IItemService
{
    public Task<GroupViewModel[]> GetGroupsAsync();
    public HealthInfo? GetHealthFromHistory(string name);
    public Task CheckHealthAsync(string Name, EndpointInfo Endpoint);
}
