using System;

namespace NProg.Distributed.OrderService.Api
{
    public interface IOrderClient : IOrderApi, IDisposable
    {
    }
}