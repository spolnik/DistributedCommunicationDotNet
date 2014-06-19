using System;
using Ice;
using NProg.Distributed.Domain.Requests;
using NProg.Distributed.Domain.Responses;
using NProg.Distributed.Service;
using NProg.Distributed.Service.Extensions;
using NProg.Distributed.Service.Messaging;
using NProgDistributed.TheIce;

namespace NProg.Distributed.Ice
{
    public class IceOrderHandler : MessageServiceDisp_, IHandler<Guid, Domain.Order>
    {
        private readonly IMessageMapper messageMapper;
        private readonly IHandler<Guid, Domain.Order> orderDao;

        public IceOrderHandler(IDaoFactory<Guid, Domain.Order> orderDaoFactory, string dbName, IMessageMapper messageMapper)
        {
            this.messageMapper = messageMapper;
            orderDao = orderDaoFactory.CreateDao(dbName);
        }

        public void Add(Guid key, Domain.Order item)
        {
            orderDao.Add(key, item);
        }

        public Domain.Order Get(Guid guid)
        {
            return orderDao.Get(guid);
        }

        public bool Remove(Guid guid)
        {
            return orderDao.Remove(guid);
        }

        public override MessageDto Send(MessageDto messageDto, Current current)
        {
            var message = messageMapper.Map(messageDto);
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

            return messageMapper.Map(response).As<MessageDto>();
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