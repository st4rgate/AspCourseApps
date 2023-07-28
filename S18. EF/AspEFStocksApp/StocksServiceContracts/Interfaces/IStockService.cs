using StocksServiceContracts.DTO;

namespace StocksServiceContracts.Interfaces
{
    public interface IStockService
    {
        Task<BuyOrderResponse> CreateBuyOrder(BuyOrderRequest? buyOrderRequest);
        Task<SellOrderResponse> CreateSellOrder(SellOrderRequest? sellOrderRequest);
        Task<List<BuyOrderResponse>> GetBuyOrders();
        Task<List<SellOrderResponse>> GetSellOrders();
    }
}
