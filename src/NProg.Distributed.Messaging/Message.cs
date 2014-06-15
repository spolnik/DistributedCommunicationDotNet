using System;
using NProg.Distributed.Messaging.Extensions;

namespace NProg.Distributed.Messaging
{
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

        public TBody BodyAs<TBody>()
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
    }
}