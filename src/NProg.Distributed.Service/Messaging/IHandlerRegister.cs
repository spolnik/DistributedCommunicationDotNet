namespace NProg.Distributed.Service.Messaging
{
    public interface IHandlerRegister
    {
        IMessageHandler GetHandler(Message message);
    }

}