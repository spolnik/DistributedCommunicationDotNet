using System;
using NProg.Distributed.Service.Messaging;
using Zyan.Communication;

namespace NProg.Distributed.Zyan
{
    public class ZyanRequestSender : RequestSender
    {
        private readonly IMessageHandler proxy;
        private ZyanConnection connection;

        public ZyanRequestSender(Uri serviceUri)
        {
            var serverUrl = string.Format("tcp://{0}:{1}/OrderService", serviceUri.Host, serviceUri.Port);
            connection = new ZyanConnection(serverUrl);
            proxy = connection.CreateProxy<IMessageHandler>();
        }

        protected override Message SendInternal(Message message)
        {
            return proxy.Send(message);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && connection != null)
            {
                connection.Dispose();
                connection = null;
            }
        }
    }
}