using System;
using System.Runtime.Remoting.Channels;
using System.Runtime.Remoting.Channels.Tcp;
using NProg.Distributed.Domain;
using NProg.Distributed.Service;

namespace NProg.Distributed.Remoting
{
    public class RemotingOrderClient : IHandler<Order>
    {
        private readonly RemotingOrderHandler orderHandler;

        public RemotingOrderClient(Uri serviceUri)
        {
            var tcpChannel = new TcpChannel();
            ChannelServices.RegisterChannel(tcpChannel, false);
            // Create an instance of the remote object
            var address = string.Format("tcp://{0}:{1}/OrderHandler", serviceUri.Host, serviceUri.Port);
            orderHandler = (RemotingOrderHandler)Activator.GetObject(typeof(RemotingOrderHandler), address);
        }

        public void Add(Order item)
        {
            orderHandler.Add(item);
        }

        public Order Get(Guid guid)
        {
            return orderHandler.Get(guid);
        }

        public bool Remove(Guid guid)
        {
            return orderHandler.Remove(guid);
        }
    }
}