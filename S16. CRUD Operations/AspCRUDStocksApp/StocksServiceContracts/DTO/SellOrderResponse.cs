using StocksEntities;
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

            if (obj.GetType() != typeof(SellOrderResponse))
            {
                return false;
            }
            // Cast di obj per poter accedere alle sue proprietà
            SellOrderResponse objToMatch = (SellOrderResponse)obj;

            // Restituisce true se tutte le proprietà corrispondono
            return SellOrderID == objToMatch.SellOrderID && StockSymbol == objToMatch.StockSymbol &&
                StockName == objToMatch.StockName && DateAndTimeOfOrder == objToMatch.DateAndTimeOfOrder &&
                Quantity == objToMatch.Quantity && Price == objToMatch.Price && TradeAmount == objToMatch.TradeAmount;
        }

        public override int GetHashCode()
        {
            return SellOrderID.GetHashCode();
        }

        /// <summary>
        /// Override ToString() method to obtain per ottenere model properties list
        /// </summary>
        /// <returns>String with model properties</returns>
        public override string ToString()
        {
            return $"SellOrderID:{SellOrderID}, StockSymbol:{StockSymbol}, StockName:{StockName}," +
                $"DateAndTimeOfOrder:{DateAndTimeOfOrder.ToString("dd MMMM yyyy")}, Quantity:{Quantity}," +
                $" Price:{Price}, TradeAmount:{TradeAmount}";
        }
    }

    public static class SellOrderExtensions
    {
        /// <summary>
        /// An extension method to convert an object of BuyOrder into SellOrderResponse
        /// </summary>
        /// <param name="sellOrder">The SellOrder object to convert</param>
        /// <returns>SellOrderResponse</returns>
        public static SellOrderResponse ToSellOrderResponse(this SellOrder sellOrder)
        {
            return new SellOrderResponse()
            {
                SellOrderID = sellOrder.SellOrderID,
                StockSymbol = sellOrder.StockSymbol,
                StockName = sellOrder.StockName,
                Price = sellOrder.Price,
                DateAndTimeOfOrder = sellOrder.DateAndTimeOfOrder,
                Quantity = sellOrder.Quantity,
                TradeAmount = sellOrder.Price * sellOrder.Quantity
            };
        }
    }
}
