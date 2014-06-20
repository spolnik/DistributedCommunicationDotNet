using System;
using System.ComponentModel.Composition;
using NProg.Distributed.Service;
using NProg.Distributed.Service.Messaging;

namespace NProg.Distributed.ZeroMQ
{
    [Export(typeof(IServiceFactory)), ExportMetadata("Name", "zmq")]
    public sealed class ZmqServiceFactory : IServiceFactory
    {
        public IRunnable GetServer(IMessageReceiver messageReceiver, IMessageMapper messageMapper, int port = -1)
        {
            return new ZmqMessageServer(messageReceiver, port);
        }

        public IMessageMapper GetMessageMapper()
        {
            return null;
        }

        public IRequestSender GetRequestSender(Uri serviceUri, IMessageMapper messageMapper)
        {
            return new ZmqRequestSender(serviceUri);
        }
    }
}