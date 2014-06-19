namespace NProg.Distributed.Service.Messaging
{
    public interface IMessageRequest
    {
        Message Send(Message message);
    }
}