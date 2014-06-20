using System;
using NProg.Distributed.OrderService.Domain;

namespace NProg.Distributed.OrderService.Api
{
    public interface IOrderApi
    {
        bool Add(Guid key, Order order);

        Order Get(Guid key);

        bool Remove(Guid key);
    }
}