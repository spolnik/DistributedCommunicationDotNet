using System;
using NProg.Distributed.Domain;
using NProg.Distributed.Domain.Requests;
using NProg.Distributed.Domain.Responses;
using NProg.Distributed.Messaging;
using NProg.Distributed.NDatabase;
using NProg.Distributed.Service;

namespace NProg.Distributed.Remoting
{
    public class RemotingOrderHandler : MarshalByRefObject, IMessageRequest
    {
        private readonly IHandler<Guid, Order> orderHandler;

        public RemotingOrderHandler()
        {
            orderHandler = new SimpleHandler<Guid, Order>(new OrderDaoFactory(), "order_remoting.ndb");
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

            orderHandler.Add(order.OrderId, order);
            return Message.From(new StatusResponse { Status = true });
        }

        private Message GetOrder(Message message)
        {
            var orderId = message.Receive<GetOrderRequest>().OrderId;

            var order = orderHandler.Get(orderId);
            return Message.From(new GetOrderResponse { Order = order });
        }

        private Message RemoveOrder(Message message)
        {
            var orderId = message.Receive<RemoveOrderRequest>().OrderId;

            var status = orderHandler.Remove(orderId);
            return Message.From(new StatusResponse { Status = status });
        }
    }
}