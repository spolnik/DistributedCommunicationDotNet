using System;
using NProg.Distributed.Domain;
using NProg.Distributed.Service;
using Thrift.Protocol;
using Thrift.Transport;

namespace NProg.Distributed.Thrift
{
    public class ThriftOrderClient : IHandler<Guid, Order>, IDisposable
    {
        private readonly TBufferedTransport transport;
        private readonly OrderService.Client client;
        private readonly TSocket socket;

        public ThriftOrderClient(Uri serviceUri)
        {
            socket = new TSocket(serviceUri.Host, serviceUri.Port);
            transport = new TBufferedTransport(socket);
            transport.Open();

            client = new OrderService.Client(new TCompactProtocol(transport));
        }

        public void Add(Guid key, Order item)
        {
            var thriftOrder = OrderMapper.MapOrder(item);
            client.Add(thriftOrder.OrderId, thriftOrder);
        }

        public Order Get(Guid guid)
        {
            return OrderMapper.MapOrder(client.Get(guid.ToString()));
        }

        public bool Remove(Guid guid)
        {
            return client.Remove(guid.ToString());
        }

        public void Dispose()
        {
            transport.Close();
            socket.Close();
        }
    }
}