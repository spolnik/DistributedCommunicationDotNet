using System;

namespace NProg.Distributed.Messaging
{
    public interface IMessageRequest : IDisposable
    {
        Message Send(Message message);
    }
}