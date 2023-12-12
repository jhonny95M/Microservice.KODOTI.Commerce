using Catalog.Domain;
using Catalog.Persistence.DataBase;
using Catalog.Service.EventHandlers.Commands;
using MediatR;

namespace Catalog.Service.EventHandlers;

public sealed class ProductCreateEventHandler : INotificationHandler<ProductCreateCommand>
{
    private readonly ApplicationDbContext context;

    public ProductCreateEventHandler(ApplicationDbContext context)
    {
        this.context = context;
    }
    public async Task Handle(ProductCreateCommand command, CancellationToken cancellationToken)
    {
        await context.AddAsync(new Product
        {
            Name = command.Name,
            Description = command.Description,
            Price = command.Price
        },cancellationToken);
        await context.SaveChangesAsync(cancellationToken);
    }
}
