using System;
using System.ServiceModel;
using NProg.Distributed.Domain;
using NProg.Distributed.Domain.Requests;
using NProg.Distributed.Domain.Responses;
using NProg.Distributed.Service;
using NProg.Distributed.Service.Messaging;
using NProg.Distributed.WCF.Service;

namespace NProg.Distributed.WCF
{
    public class WcfOrderClient : IHandler<Guid, Order>, IDisposable
    {
        private ChannelFactory<IMessageService> channelFactory;
        private readonly IMessageService proxy;

        public WcfOrderClient(Uri serviceUri)
        {
            var tcpBinding = new NetTcpBinding();
            var endpoint = new EndpointAddress(string.Format("net.tcp://{0}:{1}/OrderService", serviceUri.Host, serviceUri.Port));
            channelFactory = new ChannelFactory<IMessageService>(tcpBinding, endpoint);
            proxy = channelFactory.CreateChannel();
        }

        public void Add(Guid key, Order item)
        {
            var message = Message.From(new AddOrderRequest { Order = item });
            proxy.Send(message);
        }

        public Order Get(Guid guid)
        {
            var message = Message.From(new GetOrderRequest { OrderId = guid });
            return proxy.Send(message).Receive<GetOrderResponse>().Order;
        }

        public bool Remove(Guid guid)
        {
            var message = Message.From(new RemoveOrderRequest { OrderId = guid });
            return proxy.Send(message).Receive<StatusResponse>().Status;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        private void Dispose(bool disposing)
        {
            if (disposing && proxy != null)
            {
                channelFactory.Close();
                channelFactory = null;
            }
        }
    }
}