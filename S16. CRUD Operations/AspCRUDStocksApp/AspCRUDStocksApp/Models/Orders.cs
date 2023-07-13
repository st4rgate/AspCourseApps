using StocksServiceContracts.DTO;

namespace AspCRUDStocksApp.Models
{
    public class Orders
    {
        public List<BuyOrderResponse>? BuyOrders { get; set; }
        public List<SellOrderResponse>? SellOrders { get; set; }
    }
}
