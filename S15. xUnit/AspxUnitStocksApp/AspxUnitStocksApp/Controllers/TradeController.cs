using AspxUnitStocksApp.Models;
using AspxUnitStocksApp.Models.Options;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System.Globalization;

namespace AspxUnitStocksApp.Controllers
{
    public class TradeController : Controller
    {
        private readonly IFinnhubService _finnhubService;
        private readonly TradingOptions _tradingOptions;
        private readonly IConfiguration _configuration;

        public TradeController(IFinnhubService finnhubService,
            IOptions<TradingOptions> tradingOptions,
            IConfiguration configuration)
        {
            _finnhubService = finnhubService;
            _tradingOptions = tradingOptions.Value;
            _configuration = configuration;
        }

        [Route("/")]
        [Route("Trade/Index")]
        public async Task<IActionResult> Index()
        {
            if (_tradingOptions.DefaultStockSymbol == null)
            {
                _tradingOptions.DefaultStockSymbol = "MSFT";
            }

            Dictionary<string, object>? stockDetails =
                await _finnhubService.GetCompanyProfile(_tradingOptions.DefaultStockSymbol);
            Dictionary<string, object>? quoteDetails =
                await _finnhubService.GetStockPriceQuote(_tradingOptions.DefaultStockSymbol);

            NumberFormatInfo provider = new NumberFormatInfo();
            provider.NumberGroupSeparator = ".";

            if (stockDetails != null && quoteDetails != null)
            {
                StockTrade stockTrade = new StockTrade()
                {
                    StockSymbol = _tradingOptions.DefaultStockSymbol,
                    StockName = Convert.ToString(stockDetails["name"]),
                    Currency = Convert.ToString(stockDetails["currency"]),
                    Price = Convert.ToDouble(quoteDetails["c"].ToString(), provider)
                };

                ViewBag.Token = _configuration["finnhubToken"];
                return View(stockTrade);
            }

            return View();
        }
    }
}
