using Shared.Utils;
using System.Net;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddHealthChecks();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(builder =>
    {
        builder.AllowAnyOrigin()
            .AllowAnyHeader()
            .AllowAnyMethod();
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

app.MapHealthChecks("/hc");

app.UseSwagger();
app.UseSwaggerUI();

app.UseCors();

app.UseHttpsRedirection();

app.MapControllers();

app.Run();
