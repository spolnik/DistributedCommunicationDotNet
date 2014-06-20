namespace NProg.Distributed.Service.Messaging
{
    public interface IMessageHandler
    {
        bool CanHandle(Message message);

        Message Handle(Message message);
    }
}