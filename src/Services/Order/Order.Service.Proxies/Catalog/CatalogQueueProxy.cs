using Azure.Messaging.ServiceBus;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Order.Service.Proxies.Catalog.Commands;
using System.Text.Json;

namespace Order.Service.Proxies.Catalog
{
    public class CatalogQueueProxy : ICatalogProxy
    {
        private readonly string connectionString;
        private readonly IHttpContextAccessor httpContext;

        public CatalogQueueProxy(IOptions<AzureServiceBus> azure, IHttpContextAccessor httpContext)
        {
            connectionString = azure.Value.ConnectionString!;
            this.httpContext = httpContext;
        }
        public async Task UpdateStockAsync(ProductInStockUpdateCommand request)
        {
            ServiceBusClient client;

            // the sender used to publish messages to the queue
            ServiceBusSender sender;

            // number of messages to be sent to the queue
            const int numOfMessages = 2;

            // The Service Bus client types are safe to cache and use as a singleton for the lifetime
            // of the application, which is best practice when messages are being published or read
            // regularly.
            //
            // set the transport type to AmqpWebSockets so that the ServiceBusClient uses the port 443. 
            // If you use the default AmqpTcp, you will need to make sure that the ports 5671 and 5672 are open

            // TODO: Replace the <NAMESPACE-CONNECTION-STRING> and <QUEUE-NAME> placeholders
            var clientOptions = new ServiceBusClientOptions()
            {
                TransportType = ServiceBusTransportType.AmqpWebSockets
            };
            client = new ServiceBusClient(connectionString, clientOptions);
            string? token = null;
            if (httpContext.HttpContext.User.Identity!.IsAuthenticated && httpContext.HttpContext.Request.Headers.ContainsKey("Authorization"))
                token = httpContext.HttpContext.Request.Headers["Authorization"].ToString();
            sender = client.CreateSender("order-stock-update");
            request.AccesToken = token;
            string body = JsonSerializer.Serialize(request);
            var content = new ServiceBusMessage(body);

            using ServiceBusMessageBatch messageBatch = await sender.CreateMessageBatchAsync();

            // try adding a message to the batch
            if (!messageBatch.TryAddMessage(content))
            {
                // if it is too large for the batch
                throw new Exception($"The message {body} is too large to fit in the batch.");
            }
            try
            {
                // Use the producer client to send the batch of messages to the Service Bus queue
                await sender.SendMessagesAsync(messageBatch);
                Console.WriteLine($"A batch of {numOfMessages} messages has been published to the queue.");
            }
            finally
            {
                // Calling DisposeAsync on client types is required to ensure that network
                // resources and other unmanaged objects are properly cleaned up.
                await sender.DisposeAsync();
                await client.DisposeAsync();
            }

        }
    }
}
