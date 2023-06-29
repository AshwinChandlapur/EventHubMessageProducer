using Microsoft.Extensions.Azure;
using Microsoft.Extensions.DependencyInjection;
using Producer;

namespace EventHubMessageProducer
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello, EventHub Producer!");
            IServiceCollection serviceCollection = new ServiceCollection();
            serviceCollection
                .AddScoped<IEventHubHelper, EventHubHelper>()
                .AddScoped<IQueueProcessor, QueueProcessor>()
                .AddAzureClients(builder =>
                {
                    builder.AddEventHubProducerClient("ConnectionString", "EventHubName")
                    .WithName("EventHub");
                }); ;

            IServiceProvider serviceProvider = serviceCollection.BuildServiceProvider();
            var sender = serviceProvider.GetService<IQueueProcessor>();
            sender.Start();

            Console.WriteLine("Application is running");
            Console.ReadLine();
        }
    }
}