using Microsoft.EntityFrameworkCore;
using ServerService.Hubs;
using ServerService.Models;
using ServerService.SubscribeTableDependencies;
using ServerService.MiddlewareExtensions;

var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("ConnectionString");

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddSignalR();
builder.Services.AddDbContext<CarHubContext>(o => o.UseSqlServer(connectionString), ServiceLifetime.Singleton);

//DI
builder.Services.AddSingleton<NotificationHub>();
builder.Services.AddSingleton<SubscribeNotificationTableDependency>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
    app.UseHttpsRedirection();
}

app.UseStaticFiles();

app.UseRouting();

app.MapHub<NotificationHub>("/notificationHub");

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

if (!string.IsNullOrEmpty(connectionString))
{
    app.UseSqlTableDependency<SubscribeNotificationTableDependency>(connectionString);
}

app.Run();
