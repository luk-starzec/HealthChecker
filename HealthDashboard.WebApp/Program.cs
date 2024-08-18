using HealthDashboard.WebApp.Data;
using HealthDashboard.WebApp.Interfaces;
using HealthDashboard.WebApp.Services;
using Microsoft.AspNetCore.Components;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddHttpClient();

builder.Services.AddScoped(s => new HttpClient { BaseAddress = new Uri(s.GetRequiredService<NavigationManager>().BaseUri) });

builder.Services.AddSingleton<WeatherForecastService>();

builder.Services.AddScoped<IApiHealthService, ApiHealthService>();
builder.Services.AddScoped<IGrpcHealthService, GrpcHealthService>();
builder.Services.AddScoped<IWcfHealthService, WcfHealthService>();
builder.Services.AddScoped<IItemService, ItemService>();
builder.Services.AddSingleton<IHistoryService, HistoryService>();


var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();