using AspTagHelpersStocksApp.Models.Options;
using Microsoft.EntityFrameworkCore;
using StocksEntities;
using StocksService;
using StocksServiceContracts.Interfaces;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();

builder.Services.AddHttpClient();

builder.Services.AddScoped<IFinnhubService, FinnhubService>();
builder.Services.AddScoped<IStockService, StockService>();

// Di default il servizio è aggiunto come "Scoped", quindi FinnhubService e StockService devono essere aggiunti come "Scoped"
builder.Services.AddDbContext<StockMarketDbContext>(option =>
{
    option.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});

builder.Services.Configure<TradingOptions>(builder.Configuration.GetSection("TradingOptions"));

// Riferimento al file wkhtmltopdf.exe necessario per l'utilizzo di Rotativa
Rotativa.AspNetCore.RotativaConfiguration.Setup("wwwroot/assets/vendor", wkhtmltopdfRelativePath: "rotativa");

var app = builder.Build();

app.UseStaticFiles();
app.UseRouting();
app.MapControllers();

app.Run();
