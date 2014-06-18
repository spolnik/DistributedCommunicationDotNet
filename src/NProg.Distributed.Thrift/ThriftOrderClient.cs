using System;
using NProg.Distributed.Domain;
using NProg.Distributed.Messaging;
using NProg.Distributed.Messaging.Queries;
using NProg.Distributed.Service;
using Thrift.Protocol;
using Thrift.Transport;

namespace NProg.Distributed.Thrift
{
    public class ThriftOrderClient : MessageRequest, IHandler<Guid, Order>
    {
        private TBufferedTransport transport;
        private readonly MessageService.Client client;
        private TSocket socket;

        public ThriftOrderClient(Uri serviceUri)
        {
            socket = new TSocket(serviceUri.Host, serviceUri.Port);
            transport = new TBufferedTransport(socket);
            transport.Open();

            client = new MessageService.Client(new TCompactProtocol(transport));
        }

        public void Add(Guid key, Order item)
        {
            var message = new Message
            {
                Body = new AddOrderRequest {Order = item}
            };

            Send(message);
        }

        public Order Get(Guid guid)
        {
            var message = new Message
            {
                Body = new GetOrderRequest {OrderId = guid}
            };

            return Send(message).Receive<Order>();
        }

        public bool Remove(Guid guid)
        {
            var message = new Message
            {
                Body = new RemoveOrderRequest {OrderId = guid}
            };

            return Send(message).Receive<StatusResponse>().Status;
        }

        protected override Message SendInternal(Message message)
        {
            return MessageMapper.Map(client.Send(MessageMapper.Map(message)));
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && transport != null)
            {
                transport.Close();
                transport = null;
            }

            if (disposing && socket != null)
            {
                socket.Close();
                socket = null;
            }
        }
    }
}