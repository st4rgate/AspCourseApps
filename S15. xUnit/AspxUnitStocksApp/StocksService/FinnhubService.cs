using Microsoft.Extensions.Configuration;
using StocksServiceContracts.Interfaces;
using System.Text.Json;

namespace StocksService
{
    public class FinnhubService : IFinnhubService
    {
        private readonly IHttpClientFactory _httpClient;
        private readonly IConfiguration _configuration;

        public FinnhubService(IHttpClientFactory httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _configuration = configuration;
        }

        public async Task<Dictionary<string, object>?> GetCompanyProfile(string stockSymbol)
        {
            using (HttpClient finnhubClient = _httpClient.CreateClient())
            {
                HttpRequestMessage requestMessage = new HttpRequestMessage()
                {
                    RequestUri = new Uri($"https://finnhub.io/api/v1/stock/profile2?" +
                    $"symbol={stockSymbol}&token={_configuration["finnhubToken"]}"),
                    Method = HttpMethod.Get
                };

                HttpResponseMessage responseMessage = await finnhubClient.SendAsync(requestMessage);

                Stream stream = responseMessage.Content.ReadAsStream();
                StreamReader reader = new StreamReader(stream);
                string response = reader.ReadToEnd();
                Dictionary<string, object>? result = JsonSerializer.Deserialize<Dictionary<string, object>>(response);
                return result;
            }
        }
        public async Task<Dictionary<string, object>?> GetStockPriceQuote(string stockSymbol)
        {
            using (HttpClient finnhubClient = _httpClient.CreateClient())
            {
                HttpRequestMessage requestMessage = new HttpRequestMessage()
                {
                    RequestUri = new Uri($"https://finnhub.io/api/v1/quote?" +
                    $"symbol={stockSymbol}&token={_configuration["finnhubToken"]}"),
                    Method = HttpMethod.Get
                };

                HttpResponseMessage responseMessage = await finnhubClient.SendAsync(requestMessage);

                Stream stream = responseMessage.Content.ReadAsStream();
                StreamReader reader = new StreamReader(stream);
                string response = reader.ReadToEnd();
                Dictionary<string, object>? result = JsonSerializer.Deserialize<Dictionary<string, object>>(response);
                return result;
            }
        }
    }
}
