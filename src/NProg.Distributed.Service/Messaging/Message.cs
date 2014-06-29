using System;
using NProg.Distributed.Core.Service.Extensions;

namespace NProg.Distributed.Core.Service.Messaging
{
    /// <summary>
    /// Class Message.
    /// </summary>
    [Serializable]
    public sealed class Message
    {
        /// <summary>
        /// The body
        /// </summary>
        private object body;

        /// <summary>
        /// Gets the type of the body.
        /// </summary>
        /// <value>The type of the body.</value>
        public Type BodyType
        {
            get { return Body.GetType(); }
        }

        /// <summary>
        /// Gets or sets the body.
        /// </summary>
        /// <value>The body.</value>
        public object Body
        {
            get { return body; }
            set
            {
                body = value;
                MessageType = body.GetMessageType();
            }
        }

        /// <summary>
        /// Gets or sets the type of the message.
        /// </summary>
        /// <value>The type of the message.</value>
        public string MessageType { get; set; }

        /// <summary>
        /// Receives this instance.
        /// </summary>
        /// <typeparam name="TBody">The type of the t body.</typeparam>
        /// <returns>TBody.</returns>
        public TBody Receive<TBody>() where TBody : IRequestResponse
        {
            return (TBody) Body;
        }

        /// <summary>
        /// Froms the json.
        /// </summary>
        /// <param name="json">The json.</param>
        /// <returns>Message.</returns>
        public static Message FromJson(string json)
        {
            var message = json.ReadFromJson<Message>();
            //the body is a JObject at this point - deserialize to the real message type:
            message.Body = message.Body.ToString().ReadFromJson(message.MessageType);
            return message;
        }

        /// <summary>
        /// Froms the specified request.
        /// </summary>
        /// <typeparam name="TRequest">The type of the t request.</typeparam>
        /// <param name="request">The request.</param>
        /// <returns>Message.</returns>
        public static Message From<TRequest>(TRequest request) where TRequest : IRequestResponse 
        {
            return new Message
            {
                Body = request
            };
        }
    }
}