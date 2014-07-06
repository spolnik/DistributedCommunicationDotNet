using System;
using System.Collections.Generic;
using NProg.Distributed.Core.Service.Messaging;
using NProg.Distributed.Database.InMemory;
using NProg.Distributed.OrderService.Domain;
using NProg.Distributed.OrderService.Handlers;

namespace NProg.Distributed.OrderService.Remoting
{
    public sealed class RemotingOrderHandler : MarshalByRefObject
    {
        private readonly MessageReceiver messageReceiver;

        public RemotingOrderHandler()
        {
            var inMemoryDao = new InMemoryRepository<Guid, Order>();

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