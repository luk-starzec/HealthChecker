using Microsoft.Extensions.Diagnostics.HealthChecks;
using Shared.Utils;
using System.Net;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddGrpc();
builder.Services.AddMagicOnion();

builder.Services.AddGrpcHealthChecks()
                .AddCheck("Sample", () => HealthCheckResult.Healthy());

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(builder =>
    {
        builder.AllowAnyOrigin()
            .AllowAnyHeader()
            .AllowAnyMethod()
            .WithExposedHeaders("Grpc-Status", "Grpc-Message", "Grpc-Encoding", "Grpc-Accept-Encoding");
    });
});

var arguments = Helpers.GetArguments(args);

// port passed as argument
if (arguments.ContainsKey("port"))
{
    var port = int.Parse(arguments["port"]);
    builder.WebHost.ConfigureKestrel((context, serverOptions) =>
    {
        serverOptions.Listen(IPAddress.Loopback, port, listenOptions =>
        {
            listenOptions.UseHttps();
        });
    });
}

var app = builder.Build();

app.MapMagicOnionService();
app.MapGrpcHealthChecksService();

app.UseCors();
app.UseGrpcWeb(new GrpcWebOptions
{
    DefaultEnabled = true
});

app.MapGet("/", () => "This is Example.Grpc Service");

app.Run();
