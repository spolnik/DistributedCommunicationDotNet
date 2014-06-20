using System;
using System.ComponentModel.Composition;
using NProg.Distributed.Service;
using NProg.Distributed.Service.Messaging;

namespace NProg.Distributed.Ice
{
    [Export(typeof(IServiceFactory)), ExportMetadata("Name", "ice")]
    public sealed class IceServiceFactory : IServiceFactory
    {
        public IRunnable GetServer(IMessageReceiver messageReceiver, IMessageMapper messageMapper, int port)
        {
            return new IceMessageServer(messageReceiver, messageMapper, port);
        }

        public IMessageMapper GetMessageMapper()
        {
            return new IceMessageMapper();
        }

        public IRequestSender GetRequestSender(Uri serviceUri, IMessageMapper messageMapper)
        {
            return new IceRequestSender(serviceUri, messageMapper);
        }
    }

}