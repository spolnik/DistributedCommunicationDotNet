using System;
using System.ComponentModel.Composition;
using NProg.Distributed.Service;
using NProg.Distributed.Service.Messaging;

namespace NProg.Distributed.Thrift
{
    [Export(typeof(IServiceFactory)), ExportMetadata("Name", "thrift")]
    public sealed class ThriftServiceFactory : IServiceFactory
    {
        public IRunnable GetServer(IMessageReceiver messageReceiver, IMessageMapper messageMapper, int port)
        {
            return new ThriftMessageServer(messageReceiver, messageMapper, port);
        }

        public IMessageMapper GetMessageMapper()
        {
            return new ThriftMessageMapper();
        }

        public IRequestSender GetRequestSender(Uri serviceUri, IMessageMapper messageMapper)
        {
            return new ThriftRequestSender(serviceUri, messageMapper);
        }
    }
}