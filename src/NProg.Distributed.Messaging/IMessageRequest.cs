namespace NProg.Distributed.Messaging
{
    public interface IMessageRequest
    {
        Message Send(Message message);
    }
}