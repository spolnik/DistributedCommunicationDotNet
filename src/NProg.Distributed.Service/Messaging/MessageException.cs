using System;

namespace NProg.Distributed.Core.Service.Messaging
{
    [Serializable]
    public sealed class MessageException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="T:System.Exception"/> class with a specified error message.
        /// </summary>
        /// <param name="message">The message that describes the error. </param>
        public MessageException(string message) : base(message)
        {}
    }
}