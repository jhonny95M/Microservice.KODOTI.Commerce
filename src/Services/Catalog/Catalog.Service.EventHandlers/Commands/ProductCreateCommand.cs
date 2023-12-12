using MediatR;

namespace Catalog.Service.EventHandlers.Commands;

public record ProductCreateCommand:INotification
{
    public string Name { get; set; } = "";
    public string Description { get; set; } = "";
    public decimal Price { get; set; }
}
