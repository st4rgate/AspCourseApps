using StocksEntities;
using StocksService.Helpers;
using StocksServiceContracts.DTO;
using StocksServiceContracts.Interfaces;

namespace StocksService
{
    public class StockService : IStockService
    {
        private readonly List<BuyOrder> _buyOrders = new List<BuyOrder>();
        private readonly List<SellOrder> _sellOrders = new List<SellOrder>();
        public async Task<BuyOrderResponse> CreateBuyOrder(BuyOrderRequest? buyOrderRequest)
        {
            if (buyOrderRequest == null) throw new ArgumentNullException(nameof(buyOrderRequest));
            ValidationHelper.ModelValidation(buyOrderRequest, true);

            BuyOrder newBuyOrder = buyOrderRequest.ToBuyOrder();

            newBuyOrder.BuyOrderID = Guid.NewGuid();

            _buyOrders.Add(newBuyOrder);

            return newBuyOrder.ToBuyOrderResponse();
        }

        public async Task<SellOrderResponse> CreateSellOrder(SellOrderRequest? sellOrderRequest)
        {
            if (sellOrderRequest == null) throw new ArgumentNullException(nameof(sellOrderRequest));
            ValidationHelper.ModelValidation(sellOrderRequest, true);

            SellOrder newSellOrder = sellOrderRequest.ToSellOrder();

            newSellOrder.SellOrderID = Guid.NewGuid();

            _sellOrders.Add(newSellOrder);

            return newSellOrder.ToSellOrderResponse();
        }


        public async Task<List<BuyOrderResponse>> GetBuyOrders()
        {
            List<BuyOrderResponse> orderResponse = new List<BuyOrderResponse>();

            foreach (BuyOrder item in _buyOrders)
            {
                orderResponse.Add(item.ToBuyOrderResponse());
            }

            return orderResponse;
        }

        public async Task<List<SellOrderResponse>> GetSellOrders()
        {
            List<SellOrderResponse> orderResponse = new List<SellOrderResponse>();

            foreach (SellOrder item in _sellOrders)
            {
                orderResponse.Add(item.ToSellOrderResponse());
            }

            return orderResponse;
        }
    }
}
