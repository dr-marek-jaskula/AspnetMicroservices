using Ordering.Application;
using Ordering.Infrastructure;
using Ordering.Infrastructure.Persistance;
using Ordering.API.Utilities;
using MassTransit;
using EventBus.Messages.Common;
using Ordering.API.EventBusConsumers;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddApplicationServices();
builder.Services.AddInfrastructureServices(builder.Configuration);

//MassTransit-RabbitMq Configuration
builder.Services.AddScoped<BasketCheckoutConsumer>();

builder.Services.AddMassTransit(configuration =>
{
    configuration.AddConsumer<BasketCheckoutConsumer>();

    configuration.UsingRabbitMq((context, rabbitMqConfiguration) =>
    {
        rabbitMqConfiguration.Host(builder.Configuration["EventBusSettings:HostAddress"]);
        //Configure to which queue we subscribe 
        rabbitMqConfiguration.ReceiveEndpoint(EventBusConstants.BasketCheckoutQueue, endpointConfiguration =>
        {
            endpointConfiguration.ConfigureConsumer<BasketCheckoutConsumer>(context);
        });
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.ApplyMigrations<OrderContext>((context, service) =>
{
    var logger = service.GetRequiredService<ILogger<OrderContextSeed>>();
    OrderContextSeed.SeedAsync(context, logger).Wait();
});

app.Run();
