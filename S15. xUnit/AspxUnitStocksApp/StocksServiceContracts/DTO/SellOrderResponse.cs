using System.ComponentModel.DataAnnotations;

namespace StocksServiceContracts.DTO
{
    public class SellOrderResponse
    {
        public Guid SellOrderID { get; set; }

        [Display(Name = "Stock Symbol")]
        public string? StockSymbol { get; set; }

        [Display(Name = "Stock Name")]
        public string? StockName { get; set; }

        [Display(Name = "Order Date")]
        public DateTime DateAndTimeOfOrder { get; set; }

        [Display(Name = "Stock Quantity")]
        public uint Quantity { get; set; }

        [Display(Name = "Stock Price")]
        public double Price { get; set; }

        [Display(Name = "Trade Amount")]
        public double TradeAmount { get; set; }

        /// <summary>
        /// Override del metodo Equals() per effettuare il confronto delle singole proprietà.
        /// Obj rappresenta l'oggetto con il quale effettuare il confronto delle proprietà di questo oggetto
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public override bool Equals(object? obj)
        {
            if (obj == null)
            {
                return false;
            }

            if (obj.GetType() != typeof(BuyOrderResponse))
            {
                return false;
            }
            // Cast di obj per poter accedere alle sue proprietà
            BuyOrderResponse objToMatch = (BuyOrderResponse)obj;

            // Restituisce true se tutte le proprietà corrispondono
            return SellOrderID == objToMatch.BuyOrderID && StockSymbol == objToMatch.StockSymbol &&
                StockName == objToMatch.StockName && DateAndTimeOfOrder == objToMatch.DateAndTimeOfOrder &&
                Quantity == objToMatch.Quantity && Price == objToMatch.Price && TradeAmount == objToMatch.TradeAmount;
        }

        public override int GetHashCode()
        {
            return SellOrderID.GetHashCode();
        }
    }
}
