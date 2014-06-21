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
        private readonly TBufferedTransport transport;
        private readonly MessageService.Client client;
        private readonly TSocket socket;

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
            if (!disposing)
            {
                return;
            }

            if (client != null)
            {
                client.Dispose();
            }

            if (transport != null)
            {
                transport.Close();
            }

            if (socket != null)
            {
                socket.Close();}
        }
    }
}