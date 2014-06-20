using System;
using System.ComponentModel.Composition;
using NProg.Distributed.Service;
using NProg.Distributed.Service.Messaging;

namespace NProg.Distributed.Remoting
{
    [Export(typeof(IServiceFactory)), ExportMetadata("Name", "remoting")]
    public sealed class RemotingServiceFactory : IServiceFactory
    {
        public IRunnable GetServer(IMessageReceiver messageReceiver, IMessageMapper messageMapper, int port)
        {
            return new RemotingOrderServer(port);
        }

        public IMessageMapper GetMessageMapper()
        {
            return null;
        }

        public IRequestSender GetRequestSender(Uri serviceUri, IMessageMapper messageMapper)
        {
            return new RemotingRequestSender(serviceUri);
        }
    }

}