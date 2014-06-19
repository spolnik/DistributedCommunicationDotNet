using System;
using System.Runtime.Remoting.Channels;
using System.Runtime.Remoting.Channels.Tcp;
using NProg.Distributed.Domain;
using NProg.Distributed.Domain.Requests;
using NProg.Distributed.Domain.Responses;
using NProg.Distributed.Service;
using NProg.Distributed.Service.Messaging;

namespace NProg.Distributed.Remoting
{
    public class RemotingOrderClient : IHandler<Guid, Order>
    {
        private readonly RemotingOrderHandler proxy;

        public RemotingOrderClient(Uri serviceUri)
        {
            var tcpChannel = new TcpChannel();
            ChannelServices.RegisterChannel(tcpChannel, false);
            
            var address = string.Format("tcp://{0}:{1}/OrderHandler", serviceUri.Host, serviceUri.Port);
            proxy = (RemotingOrderHandler)Activator.GetObject(typeof(RemotingOrderHandler), address);
        }

        public void Add(Guid key, Order item)
        {
            var message = Message.From(new AddOrderRequest {Order = item});
            proxy.Send(message);
        }

        public Order Get(Guid guid)
        {
            var message = Message.From(new GetOrderRequest { OrderId = guid });
            return proxy.Send(message).Receive<GetOrderResponse>().Order;
        }

        public bool Remove(Guid guid)
        {
            var message = Message.From(new RemoveOrderRequest() { OrderId = guid });
            return proxy.Send(message).Receive<StatusResponse>().Status;
        }
    }
}