using System;
using NProg.Distributed.Domain;
using NProg.Distributed.Service;
using Zyan.Communication;

namespace NProg.Distributed.Zyan
{
    public class ZyanOrderClient : IHandler<Order>
    {
        private readonly IHandler<Order> proxy;

        public ZyanOrderClient(Uri serviceUri)
        {
            var serverUrl = string.Format("tcp://{0}:{1}/OrderService", serviceUri.Host, serviceUri.Port);
            var connection = new ZyanConnection(serverUrl);
            proxy = connection.CreateProxy<IHandler<Order>>();
        }

        public void Add(Order item)
        {
            proxy.Add(item);
        }

        public Order Get(Guid guid)
        {
            return proxy.Get(guid);
        }

        public bool Remove(Guid guid)
        {
            return proxy.Remove(guid);
        }
    }
}