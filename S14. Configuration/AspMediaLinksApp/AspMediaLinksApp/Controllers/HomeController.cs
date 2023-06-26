using AspMediaLinksApp.Option_Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace AspMediaLinksApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly SocialMediaLinksOptions _options;

        public HomeController(IOptions<SocialMediaLinksOptions> options)
        {
            _options = options.Value;
        }
        [method: HttpGet]
        [Route("/")]
        public IActionResult Index()
        {
            ViewBag.Facebook = _options.Facebook;
            ViewBag.Instagram = _options.Instagram;
            ViewBag.Twitter = _options.Twitter;
            ViewBag.Youtube = _options.Youtube;

            return View();
        }
    }
}
