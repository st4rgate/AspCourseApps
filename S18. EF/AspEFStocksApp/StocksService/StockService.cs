using Microsoft.EntityFrameworkCore;
using StocksEntities;
using StocksService.Helpers;
using StocksServiceContracts.DTO;
using StocksServiceContracts.Interfaces;

namespace StocksService
{
    public class StockService : IStockService
    {
        //private readonly List<BuyOrder> _buyOrders = new List<BuyOrder>() { };
        //private readonly List<SellOrder> _sellOrders = new List<SellOrder>() { };

        // Riferimento al DBContext (Dependency Injection)
        private readonly StockMarketDbContext _db;

        public StockService(StockMarketDbContext dbContext)
        {
            _db = dbContext;
        }
        public async Task<BuyOrderResponse> CreateBuyOrder(BuyOrderRequest? buyOrderRequest)
        {
            if (buyOrderRequest == null) throw new ArgumentNullException(nameof(buyOrderRequest));
            ValidationHelper.ModelValidation(buyOrderRequest, true);

            BuyOrder newBuyOrder = buyOrderRequest.ToBuyOrder();

            newBuyOrder.BuyOrderID = Guid.NewGuid();

            //_buyOrders.Add(newBuyOrder);
            _db.BuyOrders.Add(newBuyOrder);
            await _db.SaveChangesAsync();

            return newBuyOrder.ToBuyOrderResponse();
        }

        public async Task<SellOrderResponse> CreateSellOrder(SellOrderRequest? sellOrderRequest)
        {
            if (sellOrderRequest == null) throw new ArgumentNullException(nameof(sellOrderRequest));
            ValidationHelper.ModelValidation(sellOrderRequest, true);

            SellOrder newSellOrder = sellOrderRequest.ToSellOrder();

            newSellOrder.SellOrderID = Guid.NewGuid();

            //_sellOrders.Add(newSellOrder);
            _db.SellOrders.Add(newSellOrder);
            await _db.SaveChangesAsync();

            return newSellOrder.ToSellOrderResponse();
        }


        public async Task<List<BuyOrderResponse>> GetBuyOrders()
        {
            //List<BuyOrderResponse> orderResponse = new List<BuyOrderResponse>();

            //foreach (BuyOrder item in _buyOrders)
            //{
            //    orderResponse.Add(item.ToBuyOrderResponse());
            //}

            // Utilizzo di EF per le query sul db (DbContext.DbSet.[Linq Query])
            return await _db.BuyOrders.Select(order => order.ToBuyOrderResponse()).ToListAsync();

            //return orderResponse;
        }

        public async Task<List<SellOrderResponse>> GetSellOrders()
        {
            //    List<SellOrderResponse> orderResponse = new List<SellOrderResponse>();

            //    foreach (SellOrder item in _sellOrders)
            //    {
            //        orderResponse.Add(item.ToSellOrderResponse());
            //    }

            //    return orderResponse;
            return await _db.SellOrders.Select(order => order.ToSellOrderResponse()).ToListAsync();
        }

    }
}
