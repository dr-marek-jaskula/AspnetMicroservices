using Ocelot.DependencyInjection;
using Ocelot.Middleware;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddLogging(configuration =>
{
    configuration.AddConfiguration(builder.Configuration.GetSection("Logging"));
    configuration.AddConsole();
    configuration.AddDebug();
});

builder.Services.AddOcelot();

var app = builder.Build();

await app.UseOcelot();

app.MapGet("/", () => "Hello World!");

app.Run();
