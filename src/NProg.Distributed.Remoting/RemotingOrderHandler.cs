using System;
using System.Collections.Generic;
using NProg.Distributed.Core.Service.Messaging;
using NProg.Distributed.Database;
using NProg.Distributed.OrderService.Handlers;

namespace NProg.Distributed.Remoting
{
    public sealed class RemotingOrderHandler : MarshalByRefObject
    {
        private readonly MessageReceiver messageReceiver;

        public RemotingOrderHandler()
        {
            var inMemoryDao = new InMemoryDao();

            var messageHandlers = new List<IMessageHandler>
                {
                    new AddOrderHandler(inMemoryDao),
                    new GetOrderHandler(inMemoryDao),
                    new RemoveOrderHandler(inMemoryDao)
                };

            messageReceiver = new MessageReceiver(messageHandlers);
        }

        public Message Send(Message message)
        {
            return messageReceiver.Send(message);
        }
    }
}