using System.ComponentModel.DataAnnotations;

namespace StocksServiceContracts.DTO
{
    public class SellOrderResponse
    {
        public Guid SellOrderID { get; set; }

        [Display(Name = "Stock Symbol")]
        public string StockSymbol { get; set; }

        [Display(Name = "Stock Name")]
        public string StockName { get; set; }

        [Display(Name = "Order Date")]
        public DateTime DateAndTimeOfOrder { get; set; }

        [Display(Name = "Stock Quantity")]
        public uint Quantity { get; set; }

        [Display(Name = "Stock Price")]
        public double Price { get; set; }

        [Display(Name = "Trade Amount")]
        public double TradeAmount { get; set; }
    }
}
