using NProg.Distributed.Domain.Api;
using NProg.Distributed.Domain.Requests;
using NProg.Distributed.Domain.Responses;
using NProg.Distributed.Service.Messaging;

namespace NProg.Distributed.Domain.Handlers
{
    public class GetOrderHandler : IMessageHandler
    {
        private readonly IOrderApi dao;

        public GetOrderHandler(IOrderApi dao)
        {
            this.dao = dao;
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