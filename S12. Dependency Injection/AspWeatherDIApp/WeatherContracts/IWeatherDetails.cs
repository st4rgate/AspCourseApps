using ModelsLibrary;

namespace WeatherContracts
{
    public interface IWeatherDetails
    {
        List<CityWeather> GetAllWeather();
        CityWeather? GetCityWeather(string cityCode);
    }
}