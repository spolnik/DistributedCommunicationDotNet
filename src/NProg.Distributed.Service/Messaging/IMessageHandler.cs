namespace NProg.Distributed.Service.Messaging
{
    public interface IMessageHandler
    {
        Message Send(Message message);
    }
}