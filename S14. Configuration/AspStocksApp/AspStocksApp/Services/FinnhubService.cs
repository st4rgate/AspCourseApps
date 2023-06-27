using AspStocksApp.Contracts;

namespace AspStocksApp.Services
{
    public class FinnhubService : IFinnhubService
    {
        private readonly IFinnhubService _finnhubService;
        private readonly IHttpClientFactory _httpClient;

        public FinnhubService(IFinnhubService finnhubService, IHttpClientFactory httpClient)
        {
            _finnhubService = finnhubService;
            _httpClient = httpClient;
        }

        public Task<Dictionary<string, object>?> GetCompanyProfile(string stockSymbol)
        {
            throw new NotImplementedException();
        }

        public Task<Dictionary<string, object>?> GetStockPriceQuote(string stockSymbol)
        {
            throw new NotImplementedException();
        }
    }
}
