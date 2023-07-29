using System.ComponentModel.DataAnnotations;

namespace StocksEntities
{
    /// <summary>
    /// DOM model class for stock buy orders
    /// </summary>
    public class BuyOrder
    {
        [Key]
        public Guid BuyOrderID { get; set; }

        [Display(Name = "Stock Symbol")]
        [Required(ErrorMessage = "Stock symbol can't be blank")]
        [StringLength(10)]
        public string? StockSymbol { get; set; }

        [Display(Name = "Stock Name")]
        [Required(ErrorMessage = "Stock name can't be blank")]
        [StringLength(50)]
        public string? StockName { get; set; }

        [Display(Name = "Order Date")]
        public DateTime DateAndTimeOfOrder { get; set; }

        [Display(Name = "Stock Quantity")]
        [Range(1, 100000, ErrorMessage = "Stock quantity must be between 1 and 100000")]
        public uint Quantity { get; set; }

        [Display(Name = "Stock Price")]
        [Range(1, 10000, ErrorMessage = "Stock price must be between 1 and 10000")]
        public double Price { get; set; }
    }
}