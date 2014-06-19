namespace NProg.Distributed.Service.Messaging
{
    public interface IHandlerRegister
    {
        void Register(IMessageHandler messageHandler);

        IMessageHandler GetHandler(Message message);
    }

}