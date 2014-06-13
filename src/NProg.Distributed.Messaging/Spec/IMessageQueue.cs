using System;

namespace NProg.Distributed.Messaging.Spec
{
    public interface IMessageQueue : IDisposable
    {
        string Address { get; }

        void InitialiseOutbound(string address, MessagePattern pattern);

        void InitialiseInbound(string address, MessagePattern pattern);
        
        void Send(Message message);
        
        void Listen(Action<Message> onMessageReceived);

        void Receive(Action<Message> onMessageReceived);

        IMessageQueue GetResponseQueue();

        IMessageQueue GetReplyQueue(Message message);
    }

}
