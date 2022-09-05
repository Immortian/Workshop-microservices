using Identity.Microservice;
using Identity.Microservice.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var seed = args.Contains("/seed");
if (seed)
{
    args = args.Except(new[] { "/seed" }).ToArray();
}

var builder = WebApplication.CreateBuilder(args);

var assembly = typeof(Program).Assembly.GetName().Name;
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

if (seed)
{
    SeedData.EnsureSeedData(connectionString);
}

builder.Services.AddControllers();

builder.Services.AddMvc();

builder.Services.AddDbContext<AspNetIdentityDbContext>(opts =>
    opts.UseNpgsql(connectionString,
    b => b.MigrationsAssembly(assembly)));

builder.Services.AddIdentity<IdentityUser, IdentityRole>()
    .AddEntityFrameworkStores<AspNetIdentityDbContext>();

builder.Services.AddIdentityServer()
    .AddConfigurationStore(opt =>
    {
        opt.ConfigureDbContext = b =>
        b.UseNpgsql(connectionString, opta => opta.MigrationsAssembly(assembly));
    })
    .AddOperationalStore(opt =>
    {
        opt.ConfigureDbContext = b =>
        b.UseNpgsql(connectionString, opta => opta.MigrationsAssembly(assembly));
    })
    .AddDeveloperSigningCredential();

var app = builder.Build();

//app.UseHttpsRedirection();

app.UseStaticFiles();


app.UseRouting();

app.UseIdentityServer();

app.UseAuthentication();

app.UseEndpoints(endpoints =>
{
    endpoints.MapDefaultControllerRoute();
});
app.Run();
