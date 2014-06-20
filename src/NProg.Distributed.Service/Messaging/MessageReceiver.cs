namespace NProg.Distributed.Service.Messaging
{
    public class MessageReceiver : IMessageReceiver
    {
        private readonly IHandlerRegister handlerRegister;

        public MessageReceiver(IHandlerRegister handlerRegister)
        {
            this.handlerRegister = handlerRegister;
        }

        public Message Send(Message message)
        {
            var messageHandler = handlerRegister.GetHandler(message);
            return messageHandler.Handle(message);
        }
    }
}