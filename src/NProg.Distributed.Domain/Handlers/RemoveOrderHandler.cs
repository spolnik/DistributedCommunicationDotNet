using NProg.Distributed.OrderService.Api;
using NProg.Distributed.OrderService.Database;
using NProg.Distributed.OrderService.Requests;
using NProg.Distributed.OrderService.Responses;
using NProg.Distributed.Service.Messaging;

namespace NProg.Distributed.OrderService.Handlers
{
    public sealed class RemoveOrderHandler : IMessageHandler
    {
        private readonly IOrderApi dao;

        public RemoveOrderHandler(IOrderDaoFactory daoFactory, string dbName = null)
        {
            this.dao = daoFactory.CreateDao(dbName);
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