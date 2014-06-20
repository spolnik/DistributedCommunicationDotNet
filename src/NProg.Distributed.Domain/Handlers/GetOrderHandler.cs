using NProg.Distributed.OrderService.Api;
using NProg.Distributed.OrderService.Database;
using NProg.Distributed.OrderService.Requests;
using NProg.Distributed.OrderService.Responses;
using NProg.Distributed.Service.Messaging;

namespace NProg.Distributed.OrderService.Handlers
{
    public sealed class GetOrderHandler : IMessageHandler
    {
        private readonly IOrderApi dao;

        public GetOrderHandler(IOrderDaoFactory daoFactory)
        {
            dao = daoFactory.CreateDao();
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