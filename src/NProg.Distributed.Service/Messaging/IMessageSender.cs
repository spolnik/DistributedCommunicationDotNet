using System;

namespace NProg.Distributed.Core.Service.Messaging
{
    /// <summary>
    /// Interface IMessageSender
    /// </summary>
    public interface IMessageSender : IDisposable
    {
        /// <summary>
        /// Sends the specified message.
        /// </summary>
        /// <typeparam name="TRequest">The type of the t request.</typeparam>
        /// <param name="message">The message.</param>
        /// <returns>Message.</returns>
        Message Send<TRequest>(TRequest message) where TRequest : IMessage;
    }
}