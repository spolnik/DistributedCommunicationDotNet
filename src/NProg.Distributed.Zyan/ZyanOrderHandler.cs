using System;
using NProg.Distributed.Domain;
using NProg.Distributed.Domain.Requests;
using NProg.Distributed.Domain.Responses;
using NProg.Distributed.Service;
using NProg.Distributed.Service.Messaging;

namespace NProg.Distributed.Zyan
{
    public class ZyanOrderHandler : SimpleHandler<Guid, Order>, IMessageRequest
    {
        public ZyanOrderHandler(IDaoFactory<Guid, Order> orderDaoFactory, string dbName)
            : base(orderDaoFactory, dbName, null)
        {
        }

        public Message Send(Message message)
        {
            var response = new Message();

            if (message.BodyType == typeof(AddOrderRequest))
            {
                response = AddOrder(message);
            }
            else if (message.BodyType == typeof(GetOrderRequest))
            {
                response = GetOrder(message);
            }
            else if (message.BodyType == typeof(RemoveOrderRequest))
            {
                response = RemoveOrder(message);
            }

            return response;
        }

        private Message AddOrder(Message message)
        {
            var order = message.Receive<AddOrderRequest>().Order;

            Add(order.OrderId, order);
            return Message.From(new StatusResponse { Status = true });
        }

        private Message GetOrder(Message message)
        {
            var orderId = message.Receive<GetOrderRequest>().OrderId;

            var order = Get(orderId);
            return Message.From(new GetOrderResponse { Order = order });
        }

        private Message RemoveOrder(Message message)
        {
            var orderId = message.Receive<RemoveOrderRequest>().OrderId;

            var status = Remove(orderId);
            return Message.From(new StatusResponse { Status = status });
        }
    }
}