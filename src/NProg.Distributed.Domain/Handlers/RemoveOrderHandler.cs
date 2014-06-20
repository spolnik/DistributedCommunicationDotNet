using NProg.Distributed.Domain.Api;
using NProg.Distributed.Domain.Requests;
using NProg.Distributed.Domain.Responses;
using NProg.Distributed.Service.Messaging;

namespace NProg.Distributed.Domain.Handlers
{
    public class RemoveOrderHandler : IMessageHandler
    {
        private readonly IOrderApi dao;

        public RemoveOrderHandler(IOrderApi dao)
        {
            this.dao = dao;
        }

        public bool CanHandle(Message message)
        {
            return message.BodyType == typeof (RemoveOrderRequest);
        }

        public Message Handle(Message message)
        {
            var orderId = message.Receive<RemoveOrderRequest>().OrderId;

            var status = dao.Remove(orderId);
            return Message.From(new StatusResponse { Status = status });
        }
    }
}