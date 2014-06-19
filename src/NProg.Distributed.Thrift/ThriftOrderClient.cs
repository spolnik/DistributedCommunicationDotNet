using System;
using NProg.Distributed.Domain;
using NProg.Distributed.Domain.Api;
using NProg.Distributed.Domain.Requests;
using NProg.Distributed.Domain.Responses;
using NProg.Distributed.Service.Extensions;
using NProg.Distributed.Service.Messaging;
using Thrift.Protocol;
using Thrift.Transport;

namespace NProg.Distributed.Thrift
{
    public class ThriftOrderClient : MessageRequest, IOrderApi
    {
        private readonly IMessageMapper messageMapper;
        private TBufferedTransport transport;
        private readonly MessageService.Client client;
        private TSocket socket;

        public ThriftOrderClient(Uri serviceUri, IMessageMapper messageMapper)
        {
            this.messageMapper = messageMapper;
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

            return Send(message).Receive<GetOrderResponse>().Order;
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
            return messageMapper.Map(client.Send(messageMapper.Map(message).As<ThriftMessage>()));
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