using System;

namespace NProg.Distributed.Common.Service.Messaging
{
    /// <summary>
    /// Interface IRequestSender
    /// </summary>
    public interface IRequestSender : IDisposable
    {
        /// <summary>
        /// Sends the specified message.
        /// </summary>
        /// <typeparam name="TRequest">The type of the t request.</typeparam>
        /// <param name="message">The message.</param>
        /// <returns>Message.</returns>
        Message Send<TRequest>(TRequest message) where TRequest : class;
    }
}