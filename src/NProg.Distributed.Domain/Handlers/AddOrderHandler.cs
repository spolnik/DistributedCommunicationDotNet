using NProg.Distributed.OrderService.Api;
using NProg.Distributed.OrderService.Database;
using NProg.Distributed.OrderService.Requests;
using NProg.Distributed.OrderService.Responses;
using NProg.Distributed.Service.Messaging;

namespace NProg.Distributed.OrderService.Handlers
{
    public sealed class AddOrderHandler : IMessageHandler
    {
        private readonly IOrderApi dao;

        public AddOrderHandler(IOrderDaoFactory daoFactory, string dbName = null)
        {
            this.dao = daoFactory.CreateDao(dbName);
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