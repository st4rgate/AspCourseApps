using AspCRUDStocksApp.Models;
using AspCRUDStocksApp.Models.Options;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using StocksService;
using StocksServiceContracts.DTO;
using StocksServiceContracts.Interfaces;
using System.Globalization;

namespace AspCRUDStocksApp.Controllers
{
    [Route("Trade")]
    public class TradeController : Controller
    {
        private readonly IFinnhubService _finnhubService;
        private readonly IStockService _stockService;
        private readonly TradingOptions _tradingOptions;
        private readonly IConfiguration _configuration;

        public TradeController(IFinnhubService finnhubService,
            IStockService stockService,
            IOptions<TradingOptions> tradingOptions,
            IConfiguration configuration)
        {
            _finnhubService = finnhubService;
            _stockService = stockService;
            _tradingOptions = tradingOptions.Value;
            _configuration = configuration;
        }

        [HttpGet]
        [Route("/")]
        [Route("Index")]
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
                    Price = Convert.ToDouble(quoteDetails["c"].ToString(), provider),
                };

                ViewBag.Token = _configuration["finnhubToken"];
                return View(stockTrade);
            }

            return View();
        }

        [HttpPost]
        [Route("BuyOrder")]
        public async Task<IActionResult> BuyOrder(BuyOrderRequest buyRequest)
        {
            buyRequest.DateAndTimeOfOrder = DateTime.Now;

            ModelState.Clear();
            TryValidateModel(buyRequest);

            if (!ModelState.IsValid)
            {
                ViewBag.Errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToList();
                StockTrade stockTrade = new StockTrade() { StockName = buyRequest.StockName, Quantity = buyRequest.Quantity, StockSymbol = buyRequest.StockSymbol };
                return View("Index", stockTrade);
            }

            BuyOrderResponse buyResponse = await _stockService.CreateBuyOrder(buyRequest);

            return RedirectToAction("Orders", "Trade");
        }

        [HttpPost]
        [Route("SellOrder")]
        public async Task<IActionResult> SellOrder(SellOrderRequest sellRequest)
        {
            return RedirectToAction("Orders","Trade");
        }

        [HttpGet]
        [Route("Orders")]
        public async Task<IActionResult> Orders()
        {
            return View();
        }
    }
}
