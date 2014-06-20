using NProg.Distributed.Domain.Api;
using NProg.Distributed.Domain.Requests;
using NProg.Distributed.Domain.Responses;
using NProg.Distributed.Service.Messaging;

namespace NProg.Distributed.Domain.Handlers
{
    public class AddOrderHandler : IMessageHandler
    {
        private readonly IOrderApi dao;

        public AddOrderHandler(IOrderApi dao)
        {
            this.dao = dao;
        }

        public bool CanHandle(Message message)
        {
            return message.BodyType == typeof(AddOrderRequest);
        }

        public Message Handle(Message message)
        {
            var order = message.Receive<AddOrderRequest>().Order;

            dao.Add(order.OrderId, order);
            return Message.From(new StatusResponse { Status = true });
        }
    }
}