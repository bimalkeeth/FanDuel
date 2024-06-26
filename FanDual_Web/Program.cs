using FanDual_DepthCharts;
using FanDual_Web.Interfaces;
using FanDual_Web.MiddleWare;
using FanDual_Web.Services;
using Grpc.Net.Client;
using Grpc.Net.Client.Web;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


var depthChartServiceUrl=builder.Configuration.GetSection("ServicesUrls").GetSection("DepthCharts");

var chartServiceUrl = depthChartServiceUrl.Value ?? "localhost:53305";

var channel = GrpcChannel.ForAddress(chartServiceUrl, new GrpcChannelOptions
{
    HttpHandler = new GrpcWebHandler(new HttpClientHandler())
});

var client = new DepthChart.DepthChartClient(channel);

builder.Services.AddScoped<DepthChart.DepthChartClient>(s => new DepthChart.DepthChartClient(channel));
builder.Services.AddTransient<IDataService, DataService>();

builder.Services.AddControllers(options =>
{
    options.Filters.Add<ErrorHandler>();
}); ;

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "DepthChartPro.API", Version = "v1" });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    
}

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "DepthChartPro.API v1"));
}


app.UseHttpsRedirection();

app.UseRouting();

app.UseAuthorization();
app.MapControllers();
app.UseExceptionHandler("/Error");



app.Run();

