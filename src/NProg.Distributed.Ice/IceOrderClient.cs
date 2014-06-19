using System;
using Ice;
using NProg.Distributed.Domain;
using NProg.Distributed.Domain.Api;
using NProg.Distributed.Domain.Requests;
using NProg.Distributed.Domain.Responses;
using NProg.Distributed.Service.Extensions;
using NProg.Distributed.Service.Messaging;
using NProgDistributed.TheIce;

namespace NProg.Distributed.Ice
{
    public class IceOrderClient : MessageRequest, IOrderApi
    {
        private readonly IMessageMapper messageMapper;
        private Communicator communicator;
        private readonly MessageServicePrx proxy;

        public IceOrderClient(Uri serviceUri, IMessageMapper messageMapper)
        {
            this.messageMapper = messageMapper;
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
            return messageMapper.Map(proxy.Send(messageMapper.Map(message).As<MessageDto>()));
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && communicator != null)
            {
                communicator.destroy();
                communicator = null;
            }
        }
    }
}