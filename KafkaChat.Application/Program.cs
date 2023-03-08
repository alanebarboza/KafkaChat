using KafkaChat.Application.Filters;
using KafkaChat.Domain.Entities.Config;
using KafkaChat.Domain.Interfaces.Services;
using KafkaChat.Domain.Utils;
using KafkaChat.Infraestructure.Context;
using KafkaChat.Services.HostedServices;
using KafkaChat.Services.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddScoped<IProducerService, ProducerService>();
builder.Services.AddScoped<IConsumerService, ConsumerService>();
builder.Services.AddHostedService<ConsumerHostedService>();
builder.Services.AddDbContext<DefaultDbContext>(options => options.UseInMemoryDatabase("DefaultDataBaseMemory"), ServiceLifetime.Singleton);

builder.Services.AddControllers(options => options.Filters.Add(new ExceptionFilter()));
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddCors(options => options.AddPolicy("AllowAll", builder => builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader()));

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.UseCors("AllowAll");

var config = new ConfigurationBuilder()
    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
    .AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", optional: true, reloadOnChange: true)
    .AddEnvironmentVariables()
.Build();
var commandStartService = config.GetSection("CommandStartService").Get<CommandStartService>();

Utils.DeleteAllDataLogKafkaZookeeper(commandStartService);
Utils.StartZookeeperAndKafka(commandStartService);
Utils.OpenIndexPage();

app.Run();