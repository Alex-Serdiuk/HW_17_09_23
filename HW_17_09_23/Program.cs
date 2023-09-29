using Microsoft.AspNetCore.Mvc;
using HW_17_09_23.Models;
using Microsoft.EntityFrameworkCore;

IConfigurationRoot configuration = new ConfigurationBuilder()
    .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
    .AddJsonFile("appsettings.json")
    .Build();

var builder = WebApplication.CreateBuilder(args);

// Seervices ... 
builder.Services.AddLogging();
builder.Services.AddMvc();
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<SiteDbContext>(options =>
{
    options.UseSqlite(configuration.GetConnectionString("Default"));
});

var app = builder.Build();


// Middlewares ...
app.UseRouting();
app.MapControllers();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.UseStaticFiles();
app.Run();
