using MediatR;
using System.Reflection;
using Transactions.Microservice;
using Transactions.Microservice.Commands;
using Transactions.Microservice.RabbirListener;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddOptions();
builder.Services.AddDatabase(builder.Configuration);
builder.Services.AddScoped<workshoptransactionsdbContext>();
builder.Services.AddMediatR(Assembly.GetExecutingAssembly());
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
        var context = serviceProvider.GetRequiredService<workshoptransactionsdbContext>();
        DBInitializer.Initialize(context);
    }
    catch (Exception e) { }
}
app.UseRabbitListener();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
