using WeatherContracts;
using WeatherService;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddScoped<IWeatherDetails,WeatherDetailsService>();

builder.Services.AddControllersWithViews();

var app = builder.Build();

app.MapControllers();
app.UseStaticFiles();

app.Run();
