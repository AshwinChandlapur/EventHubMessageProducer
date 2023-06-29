namespace Producer
{
    public interface IEventHubHelper
    {
        Task Send(string data);
    }
}
