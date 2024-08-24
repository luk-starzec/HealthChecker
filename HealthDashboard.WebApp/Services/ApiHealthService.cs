using HealthDashboard.WebApp.Interfaces;

namespace HealthDashboard.WebApp.Services;

internal sealed class ApiHealthService : IApiHealthService
{
    public async Task<bool> CheckHealthAsync(string address)
    {
        using HttpClient client = new();
        try
        {
            var result = await client.GetAsync(address);
            return result.IsSuccessStatusCode;
        }
        catch (Exception)
        {
            return false;
        }
    }
}
