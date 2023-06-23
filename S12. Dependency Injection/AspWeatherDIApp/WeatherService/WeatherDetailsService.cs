using ModelsLibrary;
using System.Text.RegularExpressions;
using WeatherContracts;

namespace WeatherService
{
    public class WeatherDetailsService : IWeatherDetails
    {
        public List<CityWeather> GetAllWeather()
        {
            List<CityWeather> cityList = new List<CityWeather>()
            {
                new CityWeather() {CityUniqueCode = "LDN", CityName = "London",
                    DateAndTime = Convert.ToDateTime("2030-01-01 8:00"), TemperatureFahrenheit = 33},
                new CityWeather() {CityUniqueCode = "NYC", CityName = "New York",
                    DateAndTime = Convert.ToDateTime("2030-01-01 3:00"), TemperatureFahrenheit = 60},
                new CityWeather() {CityUniqueCode = "PAR", CityName = "Paris",
                    DateAndTime = Convert.ToDateTime("2030-01-01 9:00"), TemperatureFahrenheit = 82}
            };

            return cityList;
        }

        public CityWeather? GetCityWeather(string cityCode)
        {
            List<CityWeather> cityList = new List<CityWeather>()
            {
                new CityWeather() {CityUniqueCode = "LDN", CityName = "London",
                    DateAndTime = Convert.ToDateTime("2030-01-01 8:00"), TemperatureFahrenheit = 33},
                new CityWeather() {CityUniqueCode = "NYC", CityName = "London",
                    DateAndTime = Convert.ToDateTime("2030-01-01 3:00"), TemperatureFahrenheit = 60},
                new CityWeather() {CityUniqueCode = "PAR", CityName = "Paris",
                    DateAndTime = Convert.ToDateTime("2030-01-01 9:00"), TemperatureFahrenheit = 82}
            };

            CityWeather? City = cityList.Where(c => c.CityUniqueCode == cityCode.ToUpper()).FirstOrDefault();

            return City;
        }
    }
}