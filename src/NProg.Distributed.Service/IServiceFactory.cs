using System;
using NProg.Distributed.Core.Service.Messaging;

namespace NProg.Distributed.Core.Service
{
    /// <summary>
    /// Interface IServiceFactory
    /// </summary>
    public interface IServiceFactory
    {
        /// <summary>
        /// Gets the server.
        /// </summary>
        /// <param name="messageReceiver">The message receiver.</param>
        /// <param name="port">The port.</param>
        /// <returns>IServer.</returns>
        IServer GetServer(IMessageReceiver messageReceiver, int port);

        /// <summary>
        /// Gets the request sender.
        /// </summary>
        /// <param name="serviceUri">The service URI.</param>
        /// <returns>IRequestSender.</returns>
        IRequestSender GetRequestSender(Uri serviceUri);
    }
}