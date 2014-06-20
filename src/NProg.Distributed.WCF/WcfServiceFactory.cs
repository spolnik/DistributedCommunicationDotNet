using System;
using System.ComponentModel.Composition;
using NProg.Distributed.Service;
using NProg.Distributed.Service.Messaging;

namespace NProg.Distributed.WCF
{
    [Export(typeof(IServiceFactory)), ExportMetadata("Name", "wcf")]
    public sealed class WcfServiceFactory : IServiceFactory
    {
        public IRunnable GetServer(IMessageReceiver messageReceiver, IMessageMapper messageMapper, int port)
        {
            return new WcfMessageServer(messageReceiver, port);
        }

        public IMessageMapper GetMessageMapper()
        {
            return null;
        }

        public IRequestSender GetRequestSender(Uri serviceUri, IMessageMapper messageMapper)
        {
            return new WcfRequestSender(serviceUri);
        }
    }
}