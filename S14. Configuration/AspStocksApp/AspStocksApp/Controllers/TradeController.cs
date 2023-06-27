using Microsoft.AspNetCore.Mvc;

namespace AspStocksApp.Controllers
{
    public class TradeController : Controller
    {
        [Route("Trade/Index")]
        public IActionResult Index()
        {
            return View();
        }
    }
}
