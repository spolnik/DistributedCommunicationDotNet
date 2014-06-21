namespace NProg.Distributed.Core.Service.Messaging
{
    /// <summary>
    /// Interface IMessageMapper
    /// </summary>
    public interface IMessageMapper
    {
        /// <summary>
        /// Maps the specified custom message.
        /// </summary>
        /// <param name="customMessage">The custom message.</param>
        /// <returns>Message.</returns>
        Message Map(object customMessage);

        /// <summary>
        /// Maps the specified message.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <returns>Custom message.</returns>
        object Map(Message message);
    }
}