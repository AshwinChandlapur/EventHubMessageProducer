using Azure.Messaging.EventHubs;
using Azure.Messaging.EventHubs.Producer;
using Microsoft.Extensions.Azure;

namespace Producer
{
    public class EventHubHelper : IEventHubHelper
    {
        private readonly EventHubProducerClient producerClient;

        public EventHubHelper(IAzureClientFactory<EventHubProducerClient> producerFactory)
        {
            System.Net.ServicePointManager.SecurityProtocol = System.Net.SecurityProtocolType.Tls12;
            this.producerClient = producerFactory.CreateClient("EventHub");
        }

        public async Task Send(string data)
        {
            using (EventDataBatch eventBatch = await producerClient.CreateBatchAsync())
            {

                if (!eventBatch.TryAdd(new EventData(data)))
                {
                    throw new Exception($"Event is too large for the batch and cannot be sent.");
                }

                await producerClient.SendAsync(eventBatch);
            }
        }
    }
}