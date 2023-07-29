using AspTagHelpersStocksApp.Models;
using AspTagHelpersStocksApp.Models.Options;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Rotativa.AspNetCore;
using StocksServiceContracts.DTO;
using StocksServiceContracts.Interfaces;
using System.Globalization;

namespace AspTagHelpersStocksApp.Controllers
{
    [Route("[Controller]")]
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
        [Route("[Action]")]
        public async Task<IActionResult> Index()
        {
            if (_tradingOptions.DefaultStockSymbol == null)
            {
                _tradingOptions.DefaultStockSymbol = "MSFT";
                _tradingOptions.DefaultQuantity = 100;
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
                    Quantity = _tradingOptions.DefaultQuantity
                };

                ViewBag.Token = _configuration["finnhubToken"];
                return View(stockTrade);
            }

            return View();
        }

        [HttpPost]
        [Route("[Action]")]
        public async Task<IActionResult> BuyOrder(BuyOrderRequest buyRequest)
        {
            buyRequest.DateAndTimeOfOrder = DateTime.Now;

            ModelState.Clear();
            TryValidateModel(buyRequest);

            if (!ModelState.IsValid)
            {
                ViewBag.Errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToList();
                StockTrade stockTrade = new StockTrade() { StockName = buyRequest.StockName, Quantity = buyRequest.Quantity, StockSymbol = buyRequest.StockSymbol };
                return RedirectToAction("Index", "Trade");
            }

            BuyOrderResponse buyResponse = await _stockService.CreateBuyOrder(buyRequest);

            return RedirectToAction(nameof(Orders));
        }

        [HttpPost]
        [Route("[Action]")]
        public async Task<IActionResult> SellOrder(SellOrderRequest sellRequest)
        {
            sellRequest.DateAndTimeOfOrder = DateTime.Now;

            ModelState.Clear();
            TryValidateModel(sellRequest);

            if (!ModelState.IsValid)
            {
                ViewBag.Errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToList();
                StockTrade stockTrade = new StockTrade() { StockName = sellRequest.StockName, Quantity = sellRequest.Quantity, StockSymbol = sellRequest.StockSymbol };
                return RedirectToAction("Index", "Trade");
            }

            SellOrderResponse sellResponse = await _stockService.CreateSellOrder(sellRequest);

            return RedirectToAction(nameof(Orders));
        }

        [HttpGet]
        [Route("[Action]")]
        public async Task<IActionResult> Orders()
        {
            List<BuyOrderResponse> buyOrders = await _stockService.GetBuyOrders();
            List<SellOrderResponse> sellOrders = await _stockService.GetSellOrders();

            Orders orders = new Orders()
            {
                BuyOrders = buyOrders,
                SellOrders = sellOrders
            };

            return View(orders);
        }

        [Route("[action]")]
        public async Task<IActionResult> OrdersPDF()
        {
            List<BuyOrderResponse> buyOrders = await _stockService.GetBuyOrders();
            List<SellOrderResponse> sellOrders = await _stockService.GetSellOrders();

            Orders orders = new Orders()
            {
                BuyOrders = buyOrders,
                SellOrders = sellOrders
            };

            // Metodo contenuto in Rotativa
            return new ViewAsPdf("OrdersPDF", orders, ViewData)
            {
                PageMargins = new Rotativa.AspNetCore.Options.Margins() { Top = 10, Bottom = 10, Left = 10, Right = 10 },
                PageOrientation = Rotativa.AspNetCore.Options.Orientation.Landscape
            };

        }

    }
}
