using System;

namespace NProg.Distributed.Service.Messaging
{
    public interface IRequestSender : IDisposable
    {
        Message Send<TRequest>(TRequest message) where TRequest : class;
    }
}