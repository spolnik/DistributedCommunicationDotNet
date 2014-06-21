namespace NProg.Distributed.Common.Service.Messaging
{
    /// <summary>
    /// Interface IMessageReceiver
    /// </summary>
    public interface IMessageReceiver
    {
        /// <summary>
        /// Sends the specified message.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <returns>Message.</returns>
        Message Send(Message message);
    }
}