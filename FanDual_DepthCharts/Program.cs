using FanDual_Data.Interfaces;
using FanDual_Data.Models;
using FanDual_Data.Repository;
using FanDual_DepthCharts.Business;
using FanDual_DepthCharts.Interfaces;
using FanDual_DepthCharts.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);


var connectionString=builder.Configuration.GetSection("ConnectionStrings").GetSection("DefaultConnection");

builder.Services.AddDbContext<FanDualContext>(options => options.UseSqlite(connectionString:connectionString.Value));
builder.Services.AddTransient<IDepthChartManager,DepthChartManager>();
builder.Services.AddScoped<IRepository, DepthChartRepository>();

// Add services to the container.
builder.Services.AddGrpc();

var app = builder.Build();


var lifetime = app.Services.GetRequiredService<IHostApplicationLifetime>();

lifetime.ApplicationStopping.Register(() =>
{
    // include clean up actions here
    // Any long-running task needs to periodically check for this token if it can be cancelled
    Console.WriteLine("Application is shutting down...");
});


app.UseWhen(context => context.Connection.LocalPort == 53305,
    build =>
    {
        build.UseRouting();
        build.UseEndpoints(endpoints =>
        {
            endpoints.MapGrpcService<DepthChartService>();
        });
    });


// Configure the HTTP request pipeline.
//app.MapGrpcService<DepthChartService>().RequireHost("*:5001");
app.MapGet("/", () => "");

app.Run();

