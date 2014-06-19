using NProg.Distributed.Domain.Handlers;
using NProg.Distributed.NDatabase;
using NProg.Distributed.Service.Messaging;
using NProg.Distributed.WCF.Service;

namespace NProg.Distributed.WCF
{
    public class WcfOrderHandler : IMessageService
    {
        private readonly MessageReceiver messageReceiver;

        public WcfOrderHandler()
        {
            var handlerRegister = new HandlerRegister();
            handlerRegister.Register(new AddOrderHandler(new InMemoryDao()));
            handlerRegister.Register(new GetOrderHandler(new InMemoryDao()));
            handlerRegister.Register(new RemoveOrderHandler(new InMemoryDao()));
            messageReceiver = new MessageReceiver(handlerRegister);
        }

        public Message Send(Message message)
        {
            return messageReceiver.Send(message);
        }
    }
}