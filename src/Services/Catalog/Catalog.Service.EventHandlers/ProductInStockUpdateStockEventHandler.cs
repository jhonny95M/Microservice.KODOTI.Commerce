using Catalog.Persistence.DataBase;
using Catalog.Service.EventHandlers.Commands;
using Catalog.Service.EventHandlers.Exceptions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Catalog.Service.EventHandlers
{
    public sealed class ProductInStockUpdateStockEventHandler : INotificationHandler<ProductInStockUpdateStokCommand>
    {
        private readonly ApplicationDbContext context;
        private readonly ILogger<ProductInStockUpdateStockEventHandler> logger;

        public ProductInStockUpdateStockEventHandler(ApplicationDbContext context, ILogger<ProductInStockUpdateStockEventHandler> logger)
        {
            this.context = context;
            this.logger = logger;
        }
        public async Task Handle(ProductInStockUpdateStokCommand notification, CancellationToken cancellationToken)
        {
            logger.LogInformation("--- ProductInStockUpdateStokCommand started");
            var products = notification.Items.Select(c => c.ProductId);
            var stocks = await context.ProductInStocks.Where(c => products.Contains(c.ProductId)).ToListAsync();
            logger.LogInformation("--- Retrieve products from database");
            foreach (var item in notification.Items)
            {
                var entry = stocks.SingleOrDefault(c => c.ProductId == item.ProductId);
                if (item.Action == Common.Enums.ProductInStockAction.Substract)
                {
                    if (entry == null || item.Stock > entry.Stock)
                    {
                        logger.LogError($"Product {entry?.ProductId} - doenst't have enough stock");
                        throw new ProductInStockUpdateStokCommandException($"Product {entry?.ProductId} - doenst't have enough stock");
                    }
                    entry.Stock -= item.Stock;
                    logger.LogInformation($"--- Product {entry.ProductId} - its stock was substracted and its new stock is {entry.Stock}");
                }
                else
                {
                    if (entry == null)
                    {
                        entry = new Domain.ProductInStock
                        {
                            ProductId = item.ProductId
                        };
                        await context.AddAsync(entry);
                        logger.LogInformation($"--- New stock record was create for {entry.ProductId} because didn't  {entry.Stock}");
                    }
                    entry.Stock += item.Stock;
                    logger.LogInformation($"--- Product {entry.ProductId} - its stock was increment and its new stock is {entry.Stock}");
                }
            }
            await context.SaveChangesAsync();
            logger.LogInformation("--- ProductInStockUpdateStokCommand ended");

        }
    }
}
