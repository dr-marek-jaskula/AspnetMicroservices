using Ocelot.DependencyInjection;
using Ocelot.Middleware;
using Ocelot.Cache.CacheManager;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration.AddJsonFile($"ocelot.{builder.Environment.EnvironmentName}.json", true, true);

builder.Services.AddLogging(configuration =>
{
    configuration.AddConfiguration(builder.Configuration.GetSection("Logging"));
    configuration.AddConsole();
    configuration.AddDebug();
});

builder.Services
    .AddOcelot()
    .AddCacheManager(configuration => configuration.WithDictionaryHandle());

var app = builder.Build();

await app.UseOcelot();

app.Run();
