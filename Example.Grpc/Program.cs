using Microsoft.Extensions.Diagnostics.HealthChecks;
using Shared.Utils;
using System.Net;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddGrpc();
builder.Services.AddMagicOnion();

builder.Services.AddGrpcHealthChecks()
                .AddCheck("Sample", () => HealthCheckResult.Healthy());

var arguments = Helpers.GetArguments(args);

// port passed as argument
if (arguments.TryGetValue("port", out string? value))
{
    var port = int.Parse(value);
    builder.WebHost.ConfigureKestrel((context, serverOptions) =>
    {
        serverOptions.Listen(IPAddress.Loopback, port, listenOptions => listenOptions.UseHttps());
    });
}

var app = builder.Build();

app.MapMagicOnionService();

app.MapGrpcHealthChecksService();

app.MapGet("/", () => "This is Example.Grpc Service");

app.Run();
