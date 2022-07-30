using MediatR;
using Microsoft.Extensions.Configuration;
using System.Reflection;
using ConfirmTransactions.Microservice;
using ConfirmTransactions.Microservice.Commands;
using ConfirmTransactions.Microservice.RabbirListener;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddMediatR(Assembly.GetExecutingAssembly());
builder.Services.AddInMemoryDB();
builder.Services.AddScoped<transactionconfirmationdbContext>();
builder.Services.AddSwaggerGen();
builder.Services.AddOptions();
builder.Services.AddSingleton<RabbitListener>();
builder.Services.AddSingleton<RabbitSender>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
using (var scope = app.Services.CreateScope())
{
    var serviceProvider = scope.ServiceProvider;
    try
    {
        var context = serviceProvider.GetRequiredService<transactionconfirmationdbContext>();
        DBInitializer.Initialize(context);
    }
    catch (Exception e) { }
}
app.UseRabbitListener();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
