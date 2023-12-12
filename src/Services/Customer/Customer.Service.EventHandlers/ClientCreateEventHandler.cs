using Customer.Persistence.DataBase;
using Customer.Service.EventHandlers.Commands;
using MediatR;

namespace Customer.Service.EventHandlers;

public sealed class ClientCreateEventHandler : INotificationHandler<ClientCreateCommand>
{
    private readonly ApplicationDbContext context;

    public ClientCreateEventHandler(ApplicationDbContext context)
    {
        this.context = context;
    }

    public async Task Handle(ClientCreateCommand notification, CancellationToken cancellationToken)
    {
        await context.AddAsync(new Domain.Cliente
        {
            Name = notification.name
        }, cancellationToken);
        await context.SaveChangesAsync(cancellationToken);
    }
}
