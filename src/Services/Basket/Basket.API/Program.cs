using Basket.API.GrpcServices;
using Basket.API.Repositories;
using Discount.Grpc.Protos;
using MassTransit;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

//Redis Configuration
builder.Services.AddStackExchangeRedisCache(options =>
{
    options.Configuration = builder.Configuration.GetValue<string>("CacheSettings:ConnectionString");
});

//General Configuration
builder.Services.AddScoped<IBasketRepository, BasketRepository>();

//Grpc Configuration
builder.Services.AddScoped<DiscountGrpcService>();
builder.Services.AddGrpcClient<DiscountProtoService.DiscountProtoServiceClient>(options =>
{
    options.Address = new Uri(builder.Configuration["GrpcSettings:DiscountUrl"]!);
});

//MassTransit-RabbitMq Configuration
builder.Services.AddMassTransit(configuration =>
{
    configuration.UsingRabbitMq((context, rabbitMqConfiguration) =>
    {
        rabbitMqConfiguration.Host(builder.Configuration["EventBusSettings:HostAddress"]);
    });
});

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
