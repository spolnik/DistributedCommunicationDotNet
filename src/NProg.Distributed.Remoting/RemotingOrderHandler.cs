using System;
using NProg.Distributed.Domain.Handlers;
using NProg.Distributed.NDatabase;
using NProg.Distributed.Service.Messaging;

namespace NProg.Distributed.Remoting
{
    public class RemotingOrderHandler : MarshalByRefObject
    {
        private readonly MessageReceiver messageReceiver;

        public RemotingOrderHandler()
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