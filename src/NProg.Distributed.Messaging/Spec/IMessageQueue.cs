using System;
using System.Collections.Generic;

namespace NProg.Distributed.Messaging.Spec
{
    public interface IMessageQueue : IDisposable
    {
        string Address { get; }

        Dictionary<string, object> Properties { get; }

        void InitialiseOutbound(string address, MessagePattern pattern, Dictionary<string, object> properties = null);

        void InitialiseInbound(string address, MessagePattern pattern, Dictionary<string, object> properties = null);
        
        void Send(Message message);
        
        void Listen(Action<Message> onMessageReceived);

        void Receive(Action<Message> onMessageReceived);

        string GetAddress(string name);

        IMessageQueue GetResponseQueue();

        IMessageQueue GetReplyQueue(Message message);
    }

}
