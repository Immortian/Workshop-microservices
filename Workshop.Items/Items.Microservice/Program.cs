using Items.Microservice;
using Items.Microservice.Commands;
using Items.Microservice.RabbirListener;
using MediatR;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddAuthentication("Bearer")
    .AddIdentityServerAuthentication("Bearer", opts =>
    {
        opts.Authority = builder.Configuration["IdentityServer"];
        opts.ApiName = "Items";
    });

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddOptions();
builder.Services.AddDatabase(builder.Configuration);
builder.Services.AddScoped<workshopitemsdbContext>();
builder.Services.AddMediatR(Assembly.GetExecutingAssembly());
builder.Services.AddSingleton<RabbitListener>();
builder.Services.AddSingleton<RabbitSender>();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment() || app.Environment.IsProduction())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthentication();
app.UseAuthorization();

using (var scope = app.Services.CreateScope())
{
    var serviceProvider = scope.ServiceProvider;
    try
    {
        var context = serviceProvider.GetRequiredService<workshopitemsdbContext>();
        DBInitializer.Initialize(context);
    }
    catch (Exception e) { }
}

app.UseRabbitListener();

app.UseHttpsRedirection();

app.MapControllers();

app.Run();
