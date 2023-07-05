using StocksServiceContracts.Validators;
using System.ComponentModel.DataAnnotations;


namespace StocksServiceContracts.DTO
{
    /// <summary>
    /// DTO for sell order requests
    /// </summary>
    public class SellOrderRequest
    {
        [Display(Name = "Stock Symbol")]
        [Required(ErrorMessage = "Stock symbol can't be blank")]
        public string StockSymbol { get; set; }

        [Display(Name = "Stock Name")]
        [Required(ErrorMessage = "Stock name can't be blank")]
        public string StockName { get; set; }
        [Display(Name = "Order Date")]
        [MinDate("2000-01-01", ErrorMessage = "Order Date must be after 2000-01-01")]
        public DateTime DateAndTimeOfOrder { get; set; }

        [Display(Name = "Stock Quantity")]
        [Range(1, 100000, ErrorMessage = "Stock quantity must be between 1 and 100000")]
        public uint Quantity { get; set; }

        [Display(Name = "Stock Price")]
        [Range(1, 10000, ErrorMessage = "Stock price must be between 1 and 10000")]
        public double Price { get; set; }
    }
}
