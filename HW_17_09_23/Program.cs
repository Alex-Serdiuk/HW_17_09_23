var builder = WebApplication.CreateBuilder(args);

// Seervices ... 
builder.Services.AddLogging();
builder.Services.AddMvc();
builder.Services.AddControllersWithViews();
// builder.Services. ....

var app = builder.Build();


// Middlewares ...
app.UseRouting();
app.MapControllers();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.UseStaticFiles();
app.Run();
