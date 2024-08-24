using HealthDashboard.WebApp.Interfaces;
using HealthDashboard.WebApp.Services;
using Microsoft.AspNetCore.Components;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddHttpClient();
builder.Services.AddScoped(s => new HttpClient { BaseAddress = new Uri(s.GetRequiredService<NavigationManager>().BaseUri) });

builder.Services.AddScoped<IItemService, ItemService>();
builder.Services.AddSingleton<IApiHealthService, ApiHealthService>();
builder.Services.AddSingleton<IGrpcHealthService, GrpcHealthService>();
builder.Services.AddSingleton<IWcfHealthService, WcfHealthService>();
builder.Services.AddSingleton<IHealthService, HealthService>();
builder.Services.AddSingleton<IHistoryService, HistoryService>();
builder.Services.AddSingleton<IIntervalService, IntervalService>();
builder.Services.AddSingleton<EventBus>();

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