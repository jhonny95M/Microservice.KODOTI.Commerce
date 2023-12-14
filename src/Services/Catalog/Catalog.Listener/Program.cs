using Azure.Messaging.ServiceBus;
using System.Text;
using System.Text.Json;
// the client that owns the connection and can be used to create senders and receivers
ServiceBusClient client;

// the processor that reads and processes messages from the queue
ServiceBusProcessor processor;
var clientOptions = new ServiceBusClientOptions()
{
    TransportType = ServiceBusTransportType.AmqpWebSockets
};
client = new ServiceBusClient("Endpoint=sb://kodoti-queue.servicebus.windows.net/;SharedAccessKeyName=RootManageSharedAccessKey;SharedAccessKey=e05Qs6zfZ2uf60n7hRd+FijE1mJynbmh2+ASbICrER8=", clientOptions);

// create a processor that we can use to process the messages
// TODO: Replace the <QUEUE-NAME> placeholder
processor = client.CreateProcessor("order-stock-update", new ServiceBusProcessorOptions());

try
{
    // add handler to process messages
    processor.ProcessMessageAsync += MessageHandler;

    // add handler to process any errors
    processor.ProcessErrorAsync += ErrorHandler;

    // start processing 
    await processor.StartProcessingAsync();

    Console.WriteLine("Wait for a minute and then press any key to end the processing");
    Console.ReadKey();

    // stop processing 
    Console.WriteLine("\nStopping the receiver...");
    await processor.StopProcessingAsync();
    Console.WriteLine("Stopped receiving messages");
}
finally
{
    // Calling DisposeAsync on client types is required to ensure that network
    // resources and other unmanaged objects are properly cleaned up.
    await processor.DisposeAsync();
    await client.DisposeAsync();
}

// handle received messages
async Task MessageHandler(ProcessMessageEventArgs args)
{
    string body = args.Message.Body.ToString();
    var jwtContent = JsonSerializer.Deserialize<ProductInStockUpdateCommand>(body);
    var jwt = jwtContent!.AccesToken!;
    HttpClient httpClient = new HttpClient();
    //string catalogUrl= "http://localhost:20000/";
    string catalogUrl = "https://kcm-catalog.azurewebsites.net/";
    var contend = new StringContent(body, Encoding.UTF8, "application/json");
    httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer",jwt.Replace("Bearer ",""));
    var request = await httpClient.PutAsync(catalogUrl + "api/productInStock", contend);
    request.EnsureSuccessStatusCode();
    Console.WriteLine($"Received: {body}");

    // complete the message. message is deleted from the queue. 
    await args.CompleteMessageAsync(args.Message);
}

// handle any errors when receiving messages
Task ErrorHandler(ProcessErrorEventArgs args)
{
    Console.WriteLine(args.Exception.ToString());
    return Task.CompletedTask;
}
public record ProductInStockUpdateCommand
{
    public string? AccesToken { get; set; }
}