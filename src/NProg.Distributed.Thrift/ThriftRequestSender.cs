using System;
using NProg.Distributed.Core.Service.Extensions;
using NProg.Distributed.Core.Service.Messaging;
using NProg.Distributed.Thrift;
using Thrift.Protocol;
using Thrift.Transport;

namespace NProg.Distributed.Transport.Thrift
{
    internal sealed class ThriftRequestSender : RequestSenderBase
    {
        private readonly TBufferedTransport transport;
        private readonly MessageService.Client client;
        private readonly TSocket socket;

        internal ThriftRequestSender(Uri serviceUri)
        {
            socket = new TSocket(serviceUri.Host, serviceUri.Port);
            transport = new TBufferedTransport(socket);
            transport.Open();

            client = new MessageService.Client(new TCompactProtocol(transport));
        }

        protected override Message SendInternal(Message message)
        {
            var thriftMessage = MessageMapper.Map(message).As<ThriftMessage>();
            return MessageMapper.Map(client.Send(thriftMessage));
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