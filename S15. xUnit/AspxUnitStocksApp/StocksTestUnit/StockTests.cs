using StocksService;
using StocksServiceContracts.DTO;

namespace StocksTestUnit
{
    public class StockTests
    {
        private readonly StockService _stockService;

        public StockTests()
        {
            _stockService = new StockService();
        }

        #region CreateBuyOrder()

        // BuyOrderRequest as null > ArgumentNullException
        [Fact]
        public async void CreateBuyOrder_NullBuyOrderRequest()
        {
            // Arrange
            BuyOrderRequest? buyRequest = null;

            // Assert
            await Assert.ThrowsAsync<ArgumentNullException>(
                // Act
                () => _stockService.CreateBuyOrder(buyRequest));
        }

        // BuyOrderQuantity as 0 (as per the specification, minimum is 1) > ArgumentException.
        [Fact]
        public async void CreateBuyOrder_MinBuyOrderQuantity()
        {
            // Arrange
            BuyOrderRequest buyRequest = new BuyOrderRequest()
            {
                StockSymbol = "MSFT",
                StockName = "Microsoft",
                DateAndTimeOfOrder = new DateTime(2023, 01, 01),
                Quantity = 0,
                Price = 338
            };

            // Assert
            await Assert.ThrowsAsync<ArgumentException>(
                // Act
                () => _stockService.CreateBuyOrder(buyRequest));
        }

        // BuyOrderQuantity as 100001 (as per the specification, maximum is 100000) > ArgumentException.
        [Fact]
        public async void CreateBuyOrder_MaxBuyOrderQuantity()
        {
            // Arrange
            BuyOrderRequest buyRequest = new BuyOrderRequest()
            {
                StockSymbol = "MSFT",
                StockName = "Microsoft",
                DateAndTimeOfOrder = new DateTime(2023, 01, 01),
                Quantity = 100001,
                Price = 338
            };

            // Assert
            await Assert.ThrowsAsync<ArgumentException>(
                // Act
                () => _stockService.CreateBuyOrder(buyRequest));
        }

        // BuyOrderPrice as 0 (as per the specification, minimum is 1) > ArgumentException
        [Fact]
        public async void CreateBuyOrder_MinBuyOrderPrice()
        {
            // Arrange
            BuyOrderRequest buyRequest = new BuyOrderRequest()
            {
                StockSymbol = "MSFT",
                StockName = "Microsoft",
                DateAndTimeOfOrder = new DateTime(2023, 01, 01),
                Quantity = 10,
                Price = 0
            };

            // Assert
            await Assert.ThrowsAsync<ArgumentException>(
                // Act
                () => _stockService.CreateBuyOrder(buyRequest));
        }

        // BuyOrderPrice as 10001 (as per the specification, maximum is 10000) > ArgumentException
        [Fact]
        public async void CreateBuyOrder_MaxBuyOrderPrice()
        {
            // Arrange
            BuyOrderRequest buyRequest = new BuyOrderRequest()
            {
                StockSymbol = "MSFT",
                StockName = "Microsoft",
                DateAndTimeOfOrder = new DateTime(2023, 01, 01),
                Quantity = 10,
                Price = 10001
            };

            // Assert
            await Assert.ThrowsAsync<ArgumentException>(
                // Act
                () => _stockService.CreateBuyOrder(buyRequest));
        }

        // StockSymbol=null (as per the specification, StockSymbol can't be null) > ArgumentException
        [Fact]
        public async void CreateBuyOrder_NullStockSymbol()
        {
            // Arrange
            BuyOrderRequest buyRequest = new BuyOrderRequest()
            {
                StockSymbol = null,
                StockName = "Microsoft",
                DateAndTimeOfOrder = new DateTime(2023, 01, 01),
                Quantity = 10,
                Price = 338
            };

            // Assert
            await Assert.ThrowsAsync<ArgumentException>(
                // Act
                () => _stockService.CreateBuyOrder(buyRequest));
        }

        // DateAndTimeOfOrder as "1999-12-31"
        // (as per the specification, it should be equal or newer date than 2000-01-01) > ArgumentException
        [Fact]
        public async void CreateBuyOrder_MinDateAndTimeOfOrder()
        {
            // Arrange
            BuyOrderRequest buyRequest = new BuyOrderRequest()
            {
                StockSymbol = "MSFT",
                StockName = "Microsoft",
                DateAndTimeOfOrder = new DateTime(1999, 12, 31),
                Quantity = 10,
                Price = 338
            };

            // Assert
            await Assert.ThrowsAsync<ArgumentException>(
                // Act
                () => _stockService.CreateBuyOrder(buyRequest));
        }

        // All valid values > successful return an object of BuyOrderResponse type
        // with auto-generated BuyOrderID(guid)
        [Fact]
        public async void CreateBuyOrder_ValidBuyOrderRequest()
        {
            // Arrange
            BuyOrderRequest buyRequest = new BuyOrderRequest()
            {
                StockSymbol = "MSFT",
                StockName = "Microsoft",
                DateAndTimeOfOrder = new DateTime(2023, 07, 06),
                Quantity = 10,
                Price = 338
            };
            BuyOrderResponse buyOrderResponseCreated = await _stockService.CreateBuyOrder(buyRequest);
            List<BuyOrderResponse> buyOrderResponseFromGet = await _stockService.GetBuyOrders();

            // Assert
            Assert.True(buyOrderResponseCreated.BuyOrderID != Guid.Empty);
            Assert.Contains(buyOrderResponseCreated, buyOrderResponseFromGet);

            #endregion
        }
    }
}