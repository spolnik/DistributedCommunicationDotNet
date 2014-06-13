using System;
using System.ServiceModel;
using NProg.Distributed.Domain;
using NProg.Distributed.Service;
using NProg.Distributed.WCF.Service;

namespace NProg.Distributed.WCF
{
    public class WcfOrderClient : IHandler<Order>
    {
        private readonly NetTcpBinding tcpBinding;
        private readonly EndpointAddress endpoint;

        public WcfOrderClient(Uri serviceUri)
        {
            tcpBinding = new NetTcpBinding();
            endpoint = new EndpointAddress(string.Format("net.tcp://{0}:{1}/OrderService", serviceUri.Host, serviceUri.Port));
        }

        public void Add(Order item)
        {
            using (var channelFactory = new ChannelFactory<IOrderService>(tcpBinding, endpoint))
            {
                var orderService = channelFactory.CreateChannel();
                orderService.Add(OrderMapper.MapOrder(item));
            }
        }

        public Order Get(Guid guid)
        {
            using (var channelFactory = new ChannelFactory<IOrderService>(tcpBinding, endpoint))
            {
                var orderService = channelFactory.CreateChannel();
                return OrderMapper.MapOrder(orderService.Get(guid));
            }
        }

        public bool Remove(Guid guid)
        {
            using (var channelFactory = new ChannelFactory<IOrderService>(tcpBinding, endpoint))
            {
                var orderService = channelFactory.CreateChannel();
                return orderService.Remove(guid);
            }
        }
    }
}