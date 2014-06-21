using System;
using NProg.Distributed.Core.Data;
using NProg.Distributed.Core.Service.Messaging;
using NProg.Distributed.OrderService.Domain;
using NProg.Distributed.OrderService.Requests;
using NProg.Distributed.OrderService.Responses;

namespace NProg.Distributed.OrderService.Handlers
{
    public sealed class AddOrderHandler : IMessageHandler
    {
        private readonly IDataRepository<Guid, Order> repository;

        public AddOrderHandler(IDataRepository<Guid, Order> orderRepository)
        {
            repository = orderRepository;
        }

        public bool CanHandle(Message message)
        {
            return message.BodyType == typeof(AddOrderRequest);
        }

        public Message Handle(Message message)
        {
            var order = message.Receive<AddOrderRequest>().Order;

            var addedOrder = repository.Add(order);
            return Message.From(new StatusResponse { Status = addedOrder != null });
        }
    }
}