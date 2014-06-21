using NProg.Distributed.Common.Service.Messaging;
using NProg.Distributed.OrderService.Api;
using NProg.Distributed.OrderService.Requests;
using NProg.Distributed.OrderService.Responses;

namespace NProg.Distributed.OrderService.Handlers
{
    public sealed class GetOrderHandler : IMessageHandler
    {
        private readonly IOrderApi dao;

        public GetOrderHandler(IOrderApi orderDao)
        {
            dao = orderDao;
        }

        public bool CanHandle(Message message)
        {
            return message.BodyType == typeof (GetOrderRequest);
        }

        public Message Handle(Message message)
        {
            var orderId = message.Receive<GetOrderRequest>().OrderId;

            var order = dao.Get(orderId);
            return Message.From(new GetOrderResponse { Order = order });
        }
    }
}