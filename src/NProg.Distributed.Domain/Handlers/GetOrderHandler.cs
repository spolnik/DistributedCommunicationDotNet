using System;
using NProg.Distributed.Core.Data;
using NProg.Distributed.Core.Service.Messaging;
using NProg.Distributed.OrderService.Domain;
using NProg.Distributed.OrderService.Requests;
using NProg.Distributed.OrderService.Responses;

namespace NProg.Distributed.OrderService.Handlers
{
    public sealed class GetOrderHandler : IMessageHandler
    {
        private readonly IDataRepository<Guid, Order> repository;

        public GetOrderHandler(IDataRepository<Guid, Order> orderRepository)
        {
            repository = orderRepository;
        }

        public bool CanHandle(Message message)
        {
            return message.BodyType == typeof (GetOrderRequest);
        }

        public Message Handle(Message message)
        {
            var orderId = message.Receive<GetOrderRequest>().OrderId;

            var order = repository.Get(orderId);
            return Message.From(new GetOrderResponse { Order = order });
        }
    }
}