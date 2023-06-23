using Microsoft.AspNetCore.Mvc;
using ModelsLibrary;
using System.Text.RegularExpressions;
using WeatherContracts;

namespace AspWeatherDIApp.Controllers
{
    public class WeatherController : Controller
    {
        private readonly IWeatherDetails _weatherDetails;
        public WeatherController(IWeatherDetails weatherDetails)
        {
            _weatherDetails = weatherDetails;
        }

        [Route("/")]
        public IActionResult GetAllWeatherService()
        {
            return View("AllWeatherPage", _weatherDetails.GetAllWeather());
        }

        [Route("/weather/{cityCode}")]
        public IActionResult GetCityWeatherService(string cityCode)
        {
            if (Regex.IsMatch(cityCode, @"^[A-Za-z]{3}$"))
            {

                if (_weatherDetails.GetCityWeather(cityCode) != null)
                {
                    CityWeather? City = _weatherDetails.GetCityWeather(cityCode);
                    return View("CityWeatherPage",City);
                }

                else
                {
                    ViewBag.titlePage = "No Result";
                    ViewBag.errCode = 0;
                    return View("ErrorPage");
                }
            }
            else
            {
                ViewBag.titlePage = "Request Error";
                ViewBag.errCode = 1;
                return View("ErrorPage");
            }
        }
    }
}
