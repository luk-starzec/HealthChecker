using HealthDashboard.Web;
using HealthDashboard.Web.Interfaces;
using HealthDashboard.Web.Services;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

builder.Services.AddScoped<IApiHealthService, ApiHealthService>();
builder.Services.AddScoped<IGrpcHealthService, GrpcHealthService>();
builder.Services.AddScoped<IWcfHealthService, WcfHealthService>();
builder.Services.AddScoped<IItemService, ItemService>();
builder.Services.AddSingleton<IHistoryService, HistoryService>();

await builder.Build().RunAsync();
