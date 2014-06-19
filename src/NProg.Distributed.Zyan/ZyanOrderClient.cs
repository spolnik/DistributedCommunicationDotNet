using System;
using NProg.Distributed.Domain;
using NProg.Distributed.Domain.Requests;
using NProg.Distributed.Domain.Responses;
using NProg.Distributed.Service;
using NProg.Distributed.Service.Messaging;
using Zyan.Communication;

namespace NProg.Distributed.Zyan
{
    public class ZyanOrderClient : IHandler<Guid, Order>, IDisposable
    {
        private readonly IMessageRequest proxy;
        private ZyanConnection connection;

        public ZyanOrderClient(Uri serviceUri)
        {
            var serverUrl = string.Format("tcp://{0}:{1}/OrderService", serviceUri.Host, serviceUri.Port);
            connection = new ZyanConnection(serverUrl);
            proxy = connection.CreateProxy<IMessageRequest>();
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
            if (disposing && connection != null)
            {
                connection.Dispose();
                connection = null;
            }
        }
    }
}