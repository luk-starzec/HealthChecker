using Shared.Utils;
using System.Net;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddHealthChecks();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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

app.MapHealthChecks("/hc");

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.MapControllers();

app.Run();
