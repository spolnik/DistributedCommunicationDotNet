using System;
using Ice;
using NProg.Distributed.Domain;
using NProg.Distributed.Domain.Requests;
using NProg.Distributed.Domain.Responses;
using NProg.Distributed.Messaging;
using NProg.Distributed.Service;
using NProgDistributed.TheIce;

namespace NProg.Distributed.Ice
{
    public class IceOrderClient : MessageRequest, IHandler<Guid, Order>
    {
        private readonly Communicator communicator;
        private readonly MessageServicePrx proxy;

        public IceOrderClient(Uri serviceUri)
        {
            var address = string.Format("OrderService:tcp -p {1} -h {0}", serviceUri.Host, serviceUri.Port);

            communicator = Util.initialize();
            proxy = MessageServicePrxHelper.checkedCast(communicator.stringToProxy(address));
        }

        public void Add(Guid key, Order item)
        {
            var message = new Message
            {
                Body = new AddOrderRequest { Order = item }
            };

            Send(message);
        }

        public Order Get(Guid guid)
        {
            var message = new Message
            {
                Body = new GetOrderRequest { OrderId = guid }
            };

            return Send(message).Receive<GetOrderResponse>().Order;
        }

        public bool Remove(Guid guid)
        {
            var message = new Message
            {
                Body = new RemoveOrderRequest { OrderId = guid }
            };

            return Send(message).Receive<StatusResponse>().Status;
        }

        protected override Message SendInternal(Message message)
        {
            return MessageMapper.Map(proxy.Send(MessageMapper.Map(message)));
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && communicator != null)
                communicator.destroy();
        }
    }
}