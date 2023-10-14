using Microsoft.AspNetCore.Mvc;
using HW_17_09_23.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

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

builder.Services.AddIdentity<User, IdentityRole<int>>(options =>
{
    options.SignIn.RequireConfirmedAccount = false;
    options.SignIn.RequireConfirmedEmail = false;
    options.SignIn.RequireConfirmedPhoneNumber = false;

    options.Password.RequiredLength = 3;
    options.Password.RequiredUniqueChars = 0;
    options.Password.RequireDigit = false;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireLowercase = false;
    options.Password.RequireUppercase = false;

})
    .AddRoles<IdentityRole<int>>()
    .AddEntityFrameworkStores<SiteDbContext>();
 

var app = builder.Build();


// Middlewares ...
app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=AboutMe}/{action=Index}/{id?}");

app.UseStaticFiles();
app.Run();
