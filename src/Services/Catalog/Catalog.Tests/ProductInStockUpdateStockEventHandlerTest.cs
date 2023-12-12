using Castle.Core.Logging;
using Catalog.Service.EventHandlers;
using Catalog.Service.EventHandlers.Commands;
using Catalog.Service.EventHandlers.Exceptions;
using Catalog.Tests.Config;
using Microsoft.Extensions.Logging;
using Moq;

namespace Catalog.Tests
{
    [TestClass]
    public class ProductInStockUpdateStockEventHandlerTest
    {
        private readonly ILogger<ProductInStockUpdateStockEventHandler> logger;

        public ProductInStockUpdateStockEventHandlerTest()
        {
            this.logger = new Mock<ILogger<ProductInStockUpdateStockEventHandler>>().Object;
        }

        [TestMethod]
        public async Task TryToSubstractStockWhenProductHasStock()
        {
            var context = ApplicationDbContextInMemory.Get();
            var productId = 1;
            var productInStockId=1;
            context.ProductInStocks.Add(new Domain.ProductInStock
            {
                ProductId = productId,
                ProductInStockId = productInStockId,
                Stock = 1
            });
            context.SaveChanges();
            var handler=new ProductInStockUpdateStockEventHandler(context,logger);
            await handler.Handle(new Service.EventHandlers.Commands.ProductInStockUpdateStokCommand
            {
                Items = new List<ProductInStockUpdateItem>()
                {
                    new ProductInStockUpdateItem
                    {
                        ProductId = productId,
                        Stock= 1,
                        Action=Common.Enums.ProductInStockAction.Substract
                    }
                }
            },CancellationToken.None);
        }
        [TestMethod]
        [ExpectedException(typeof(ProductInStockUpdateStokCommandException))]
        public async Task TryToSubstractStockWhenProductHasntStock()
        {
            var context = ApplicationDbContextInMemory.Get();
            var productId = 2;
            var productInStockId = 2;
            context.ProductInStocks.Add(new Domain.ProductInStock
            {
                ProductId = productId,
                ProductInStockId = productInStockId,
                Stock = 1
            });
            context.SaveChanges();
            var handler = new ProductInStockUpdateStockEventHandler(context, logger);
            //try
            //{
                await handler.Handle(new Service.EventHandlers.Commands.ProductInStockUpdateStokCommand
                {
                    Items = new List<ProductInStockUpdateItem>()
                {
                    new ProductInStockUpdateItem
                    {
                        ProductId = productId,
                        Stock= 2,
                        Action=Common.Enums.ProductInStockAction.Substract
                    }
                }
                }, CancellationToken.None);
        }
        [TestMethod]
        public async Task TryToAddStockWhenProductExists()
        {
            var context = ApplicationDbContextInMemory.Get();
            var productId = 3;
            var productInStockId = 3;
            context.ProductInStocks.Add(new Domain.ProductInStock
            {
                ProductId = productId,
                ProductInStockId = productInStockId,
                Stock = 1
            });
            context.SaveChanges();
            var handler = new ProductInStockUpdateStockEventHandler(context, logger);
            await handler.Handle(new Service.EventHandlers.Commands.ProductInStockUpdateStokCommand
            {
                Items = new List<ProductInStockUpdateItem>()
                {
                    new ProductInStockUpdateItem
                    {
                        ProductId = productId,
                        Stock= 2,
                        Action=Common.Enums.ProductInStockAction.Add
                    }
                }
            }, CancellationToken.None);
            var stockInDb=context.ProductInStocks.Single(c=>c.ProductId==productId).Stock;
            Assert.AreEqual(stockInDb,3);
        }
        [TestMethod]
        public async Task TryToAddStockWhenProductNoExists()
        {
            var context = ApplicationDbContextInMemory.Get();
            var productId = 4;
            
            var handler = new ProductInStockUpdateStockEventHandler(context, logger);
            await handler.Handle(new Service.EventHandlers.Commands.ProductInStockUpdateStokCommand
            {
                Items = new List<ProductInStockUpdateItem>()
                {
                    new ProductInStockUpdateItem
                    {
                        ProductId = productId,
                        Stock= 2,
                        Action=Common.Enums.ProductInStockAction.Add
                    }
                }
            }, CancellationToken.None);
            var stockInDb = context.ProductInStocks.Single(c => c.ProductId == productId).Stock;
            Assert.AreEqual(stockInDb, 2);
        }
    }
}