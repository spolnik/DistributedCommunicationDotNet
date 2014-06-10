using System;
using NProg.Distributed.Domain;
using NProg.Distributed.Service;
using Thrift.Protocol;
using Thrift.Transport;

namespace NProg.Distributed.Thrift
{
    public class ThriftOrderClient : IHandler<Order>
    {
        private readonly Uri serviceUri;

        public ThriftOrderClient(Uri serviceUri)
        {
            this.serviceUri = serviceUri;
        }

        public void Add(Order item)
        {
            using (var transport = new TSocket(serviceUri.Host, serviceUri.Port))
            {
                var protocol = new TBinaryProtocol(transport);
                var client = new OrderService.Client(protocol);

                transport.Open();

                client.Add(OrderMapper.MapOrder(item));
            }
        }

        public Order Get(Guid guid)
        {
            using (var transport = new TSocket(serviceUri.Host, serviceUri.Port))
            {
                var protocol = new TBinaryProtocol(transport);
                var client = new OrderService.Client(protocol);

                transport.Open();

                return OrderMapper.MapOrder(client.Get(guid.ToString()));
            }
        }

        public bool Remove(Guid guid)
        {
            using (var transport = new TSocket(serviceUri.Host, serviceUri.Port))
            {
                var protocol = new TBinaryProtocol(transport);
                var client = new OrderService.Client(protocol);

                transport.Open();

                return client.Remove(guid.ToString());
            }
        }
    }
}