using System;
using System.ComponentModel.Composition;
using NProg.Distributed.Service;
using NProg.Distributed.Service.Messaging;

namespace NProg.Distributed.Zyan
{
    [Export(typeof(IServiceFactory)), ExportMetadata("Name", "zyan")]
    public sealed class ZyanServiceFactory : IServiceFactory
    {
        public IRunnable GetServer(IMessageReceiver messageReceiver, IMessageMapper messageMapper, int port = -1)
        {
            return new ZyanMessageServer(messageReceiver, port);
        }

        public IMessageMapper GetMessageMapper()
        {
            return null;
        }

        public IRequestSender GetRequestSender(Uri serviceUri, IMessageMapper messageMapper)
        {
            return new ZyanRequestSender(serviceUri);
        }
    }
}