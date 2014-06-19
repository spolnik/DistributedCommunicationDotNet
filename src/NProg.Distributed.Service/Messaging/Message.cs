using System;
using NProg.Distributed.Service.Extensions;

namespace NProg.Distributed.Service.Messaging
{
    [Serializable]
    public class Message
    {
        private object body;

        public Type BodyType
        {
            get { return Body.GetType(); }
        }

        public object Body
        {
            get { return body; }
            set
            {
                body = value;
                MessageType = body.GetMessageType();
            }
        }

        public string MessageType { get; set; }

        public TBody Receive<TBody>()
        {
            return (TBody) Body;
        }

        public static Message FromJson(string json)
        {
            var message = json.ReadFromJson<Message>();
            //the body is a JObject at this point - deserialize to the real message type:
            message.Body = message.Body.ToString().ReadFromJson(message.MessageType);
            return message;
        }

        public static Message From<TRequest>(TRequest request) where TRequest : class 
        {
            return new Message
            {
                Body = request
            };
        }
    }
}