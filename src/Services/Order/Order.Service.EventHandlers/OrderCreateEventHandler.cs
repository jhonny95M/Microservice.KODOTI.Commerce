using MediatR;
using Microsoft.Extensions.Logging;
using Order.Persistence.DataBase;
using Order.Service.EventHandlers.Commands;
using Order.Service.Proxies.Catalog;
using Order.Service.Proxies.Catalog.Commands;

namespace Order.Service.EventHandlers
{
    public class OrderCreateEventHandler : INotificationHandler<OrderCreateCommand>
    {
        private readonly ApplicationDbContext context;
        private readonly ILogger<OrderCreateEventHandler>logger;
        private readonly ICatalogProxy catalogProxy;

        public OrderCreateEventHandler(ApplicationDbContext context, ILogger<OrderCreateEventHandler> logger, ICatalogProxy catalogProxy)
        {
            this.context = context;
            this.logger = logger;
            this.catalogProxy = catalogProxy;
        }

        public async Task Handle(OrderCreateCommand notification, CancellationToken cancellationToken)
        {
            logger.LogInformation("--- new order creation started");
            var entry = new Domain.Order();
            using(var ts=await context.Database.BeginTransactionAsync(cancellationToken))
            {
                logger.LogInformation("--- preparing detail");
                PrepareDetail(entry,notification);
                logger.LogInformation("--- preparin header");
                PrepareHeader(entry,notification);
                logger.LogInformation("--- creating order");
                await context.Orders.AddAsync(entry,cancellationToken);
                await context.SaveChangesAsync(cancellationToken);
                logger.LogInformation($"--- order {entry.OrderId} was created");
                logger.LogInformation("--- updating stock");

                await catalogProxy.UpdateStockAsync(new Proxies.Catalog.Commands.ProductInStockUpdateCommand
                {
                    Items = notification.Items.Select(c => new ProductInStockUpdateItem
                    {
                        Action=ProductInStockAction.Substract,
                        ProductId = c.ProductId,
                        Stock=c.Quantity
                    })
                });
                await ts.CommitAsync(cancellationToken);
            }
        }

        private void PrepareHeader(Domain.Order entry, OrderCreateCommand notification)
        {
            entry.Status = Common.Enums.OrderStatus.Pending;
            entry.PaymentType=notification.PaymentType;
            entry.ClientId=notification.ClientId;
            entry.CreatedAt = DateTime.Now;
            entry.Total=entry.Items.Sum(x => x.Total);
        }

        private void PrepareDetail(Domain.Order entry, OrderCreateCommand notification)
        {
            entry.Items = notification.Items.Select(c => new Domain.OrderDetail
            {
                ProductId = c.ProductId,
                Quantity = c.Quantity,
                UnitPrice=c.Price,
                Total=c.Price*c.Quantity
            }).ToList();
        }
    }
}