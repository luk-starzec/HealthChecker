using HealthDashboard.Web.Interfaces;

namespace HealthDashboard.Web.Services;

public class ApiHealthService : IApiHealthService
{
    public async Task<bool> CheckHealthAsync(string address)
    {
        using HttpClient client = new();
        try
        {
            var result = await client.GetAsync(address);
            return result.IsSuccessStatusCode;
        }
        catch (Exception e)
        {
            return false;
        }
    }
}
