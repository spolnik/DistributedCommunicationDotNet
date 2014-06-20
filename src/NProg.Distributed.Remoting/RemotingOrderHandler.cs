using System;
using System.Collections.Generic;
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
            var inMemoryDao = new InMemoryDao();
            var register = new List<IMessageHandler>
            {
                new AddOrderHandler(inMemoryDao),
                new GetOrderHandler(inMemoryDao),
                new RemoveOrderHandler(inMemoryDao)
            };

            var handlerRegister = new HandlerRegister(register);
            messageReceiver = new MessageReceiver(handlerRegister);
        }

        public Message Send(Message message)
        {
            return messageReceiver.Send(message);
        }
    }
}