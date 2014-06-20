namespace NProg.Distributed.Service.Messaging
{
    public interface IMessageReceiver
    {
        Message Send(Message message);
    }
}