using System;
using NProg.Distributed.Service.Extensions;
using NProg.Distributed.Service.Messaging;
using Thrift.Protocol;
using Thrift.Transport;

namespace NProg.Distributed.Thrift
{
    internal sealed class ThriftRequestSender : RequestSenderBase
    {
        private readonly IMessageMapper messageMapper;
        private TBufferedTransport transport;
        private readonly MessageService.Client client;
        private TSocket socket;

        internal ThriftRequestSender(Uri serviceUri, IMessageMapper messageMapper)
        {
            this.messageMapper = messageMapper;
            socket = new TSocket(serviceUri.Host, serviceUri.Port);
            transport = new TBufferedTransport(socket);
            transport.Open();

            client = new MessageService.Client(new TCompactProtocol(transport));
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