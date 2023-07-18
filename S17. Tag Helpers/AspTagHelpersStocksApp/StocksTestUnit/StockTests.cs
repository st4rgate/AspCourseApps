using StocksService;
using StocksServiceContracts.DTO;
using Xunit.Abstractions;

namespace StockTestUnit
{
    public class StockTests
    {
        private readonly StockService _stockService;
        // Bult-in service for test results output
        private readonly ITestOutputHelper _outputHelper;

        private List<BuyOrderRequest> _buyOrderRequestList = new List<BuyOrderRequest>()
        {
            new BuyOrderRequest
            {
                StockSymbol = "MSFT",
                StockName = "Microsoft",
                DateAndTimeOfOrder = new DateTime(2023, 01, 01),
                Quantity = 10,
                Price = 338
            },
                        new BuyOrderRequest
            {
                StockSymbol = "AAPL",
                StockName = "Apple",
                DateAndTimeOfOrder = new DateTime(2023, 01, 02),
                Quantity = 5,
                Price = 176
            }
        };

        private List<SellOrderRequest> _sellOrderRequestList = new List<SellOrderRequest>()
        {
            new SellOrderRequest
            {
                StockSymbol = "MSFT",
                StockName = "Microsoft",
                DateAndTimeOfOrder = new DateTime(2023, 01, 01),
                Quantity = 10,
                Price = 338
            },
                        new SellOrderRequest
            {
                StockSymbol = "AAPL",
                StockName = "Apple",
                DateAndTimeOfOrder = new DateTime(2023, 01, 02),
                Quantity = 5,
                Price = 176
            }
        };

        public StockTests(ITestOutputHelper outputHelper)
        {
            _stockService = new StockService();
            _outputHelper = outputHelper;
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

        // Quantity as 0 (as per the specification, minimum is 1) > ArgumentException.
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

        // Quantity as 100001 (as per the specification, maximum is 100000) > ArgumentException.
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

        // Price as 0 (as per the specification, minimum is 1) > ArgumentException
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

        // Price as 10001 (as per the specification, maximum is 10000) > ArgumentException
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

            _outputHelper.WriteLine("Expected BuyOrderResponse:");
            BuyOrderResponse buyOrderResponseCreated = await _stockService.CreateBuyOrder(buyRequest);
            _outputHelper.WriteLine(buyOrderResponseCreated.ToString());

            _outputHelper.WriteLine("Actual BuyOrderResponse:");
            List<BuyOrderResponse> buyOrderResponseFromGet = await _stockService.GetBuyOrders();
            foreach (BuyOrderResponse item in buyOrderResponseFromGet)
            {
                _outputHelper.WriteLine(item.ToString());
            }

            // Assert
            Assert.True(buyOrderResponseCreated.BuyOrderID != Guid.Empty);
            Assert.Contains(buyOrderResponseCreated, buyOrderResponseFromGet);
        }
        #endregion

        #region GetBuyOrder()

        // Invoke this method by default > List empty
        [Fact]
        public async void GetBuyOrder_NullBuyOrderRequest()
        {
            // Arrange + Act
            List<BuyOrderResponse>? buyOrderResponse = await _stockService.GetBuyOrders();

            // Assert
            Assert.Empty(buyOrderResponse);
        }

        // Add few BuyOrders using CreateBuyOrder() method and then GetBuyOrder() =>
        // return list contains all the same buy orders
        [Fact]
        public async void GetBuyOrder_SomeBuyOrder()
        {
            // Arrange
            // List of responses from CreateBuyOrder()
            List<BuyOrderResponse> buyOrderResponse = new List<BuyOrderResponse>();

            foreach (BuyOrderRequest item in _buyOrderRequestList)
            {
                buyOrderResponse.Add(await _stockService.CreateBuyOrder(item));
            }

            _outputHelper.WriteLine("Expected responses list:");

            foreach (BuyOrderResponse item in buyOrderResponse)
            {
                _outputHelper.WriteLine(item.ToString());
            }

            // Act
            // List of responses from GetBuyOrder()
            List<BuyOrderResponse> buyOrderResponseFromGet = await _stockService.GetBuyOrders();

            _outputHelper.WriteLine("Actual responses list:");

            foreach (BuyOrderResponse item in buyOrderResponseFromGet)
            {
                _outputHelper.WriteLine(item.ToString());
            }

            // Assert
            foreach (BuyOrderResponse item in buyOrderResponse)
            {
                Assert.Contains(item, buyOrderResponseFromGet);
            }
        }

        #endregion

        #region CreateSellOrder()

        // SellOrderRequest as null > ArgumentNullException
        [Fact]
        public async void CreateSellOrder_NullSellOrderRequest()
        {
            // Arrange
            SellOrderRequest? sellRequest = null;

            // Assert
            await Assert.ThrowsAsync<ArgumentNullException>(
                // Act
                () => _stockService.CreateSellOrder(sellRequest));
        }

        // Quantity as 0 (as per the specification, minimum is 1) > ArgumenException
        [Fact]
        public async void CreateSellOrder_MinSellOrderQuantity()
        {
            // Arrange
            SellOrderRequest sellRequest = new SellOrderRequest()
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
                () => _stockService.CreateSellOrder(sellRequest));
        }

        // Quantity as 100001 (as per the specification, maximum is 100000) > ArgumentException.
        [Fact]
        public async void CreateSellOrder_MaxSellOrderQuantity()
        {
            // Arrange
            SellOrderRequest sellRequest = new SellOrderRequest()
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
                () => _stockService.CreateSellOrder(sellRequest));
        }

        // Price as 0 (as per the specification, minimum is 1) > ArgumentException.
        [Fact]
        public async void CreateSellOrder_MinSellOrderPrice()
        {
            // Arrange
            SellOrderRequest sellRequest = new SellOrderRequest()
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
                () => _stockService.CreateSellOrder(sellRequest));
        }

        // Price as 10001 (as per the specification, maximum is 10000) > ArgumentException.
        [Fact]
        public async void CreateSellOrder_MaxSellOrderPrice()
        {
            // Arrange
            SellOrderRequest sellRequest = new SellOrderRequest()
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
                () => _stockService.CreateSellOrder(sellRequest));
        }

        // StockSymbol=null (as per the specification, StockSymbol can't be null) > ArgumentException
        [Fact]
        public async void CreateSellOrder_NullStockSymbol()
        {
            // Arrange
            SellOrderRequest sellRequest = new SellOrderRequest()
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
                () => _stockService.CreateSellOrder(sellRequest));
        }

        // DateAndTimeOfOrder as "1999-12-31"
        // (as per the specification, it should be equal or newer date than 2000-01-01) > ArgumentException
        [Fact]
        public async void CreateSellOrder_MinDateAndTimeOfOrder()
        {
            // Arrange
            SellOrderRequest sellRequest = new SellOrderRequest()
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
                () => _stockService.CreateSellOrder(sellRequest));
        }

        // All valid values > successful return an object of SellOrderResponse type
        // with auto-generated SellOrderID(guid)
        [Fact]
        public async void CreateSellOrder_ValidSellOrderRequest()
        {
            // Arrange
            SellOrderRequest sellRequest = new SellOrderRequest()
            {
                StockSymbol = "MSFT",
                StockName = "Microsoft",
                DateAndTimeOfOrder = new DateTime(2023, 07, 06),
                Quantity = 10,
                Price = 338
            };

            _outputHelper.WriteLine("Expected SellOrderResponse:");
            SellOrderResponse sellOrderResponseCreated = await _stockService.CreateSellOrder(sellRequest);
            _outputHelper.WriteLine(sellOrderResponseCreated.ToString());

            _outputHelper.WriteLine("Actual SellOrderResponse:");
            List<SellOrderResponse> sellOrderResponseFromGet = await _stockService.GetSellOrders();
            foreach (SellOrderResponse item in sellOrderResponseFromGet)
            {
                _outputHelper.WriteLine(item.ToString());
            }

            // Assert
            Assert.True(sellOrderResponseCreated.SellOrderID != Guid.Empty);
            Assert.Contains(sellOrderResponseCreated, sellOrderResponseFromGet);
        }

        #endregion

        #region GetSellOrder()

        // Invoke this method by default > List empty
        [Fact]
        public async void GetSellOrder_NullSellOrderRequest()
        {
            // Arrange + Act
            List<SellOrderResponse>? sellOrderResponse = await _stockService.GetSellOrders();

            // Assert
            Assert.Empty(sellOrderResponse);
        }

        // Add few SellOrders using CreateSellOrder() method and then GetSellOrder() =>
        // return list contains all the same sell orders
        [Fact]
        public async void GetSellOrder_SomeSellOrder()
        {
            // Arrange
            // List of responses from CreateSellOrder()
            List<SellOrderResponse> sellOrderResponse = new List<SellOrderResponse>();

            foreach (SellOrderRequest item in _sellOrderRequestList)
            {
                sellOrderResponse.Add(await _stockService.CreateSellOrder(item));
            }

            _outputHelper.WriteLine("Expected responses list:");

            foreach (SellOrderResponse item in sellOrderResponse)
            {
                _outputHelper.WriteLine(item.ToString());
            }

            // Act
            // List of responses from GetSellOrder()
            List<SellOrderResponse> sellOrderResponseFromGet = await _stockService.GetSellOrders();

            _outputHelper.WriteLine("Actual responses list:");

            foreach (SellOrderResponse item in sellOrderResponseFromGet)
            {
                _outputHelper.WriteLine(item.ToString());
            }

            // Assert
            foreach (SellOrderResponse item in sellOrderResponse)
            {
                Assert.Contains(item, sellOrderResponseFromGet);
            }
        }

        #endregion
    }
}