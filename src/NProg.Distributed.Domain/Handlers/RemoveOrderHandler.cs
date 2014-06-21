using System;
using NProg.Distributed.Core.Data;
using NProg.Distributed.Core.Service.Messaging;
using NProg.Distributed.OrderService.Domain;
using NProg.Distributed.OrderService.Requests;
using NProg.Distributed.OrderService.Responses;

namespace NProg.Distributed.OrderService.Handlers
{
    public sealed class RemoveOrderHandler : IMessageHandler
    {
        private readonly IDataRepository<Guid, Order> repository;

        public RemoveOrderHandler(IDataRepository<Guid, Order> orderRepository)
        {
            repository = orderRepository;
        }

        public bool CanHandle(Message message)
        {
            return message.BodyType == typeof (RemoveOrderRequest);
        }

        public Message Handle(Message message)
        {
            var orderId = message.Receive<RemoveOrderRequest>().OrderId;

            var status = repository.Remove(orderId);
            return Message.From(new StatusResponse { Status = status });
        }
    }
}