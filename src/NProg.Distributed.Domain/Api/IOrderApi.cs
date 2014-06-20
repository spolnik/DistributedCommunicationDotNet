using System;

namespace NProg.Distributed.Domain.Api
{
    public interface IOrderApi
    {
        bool Add(Guid key, Order order);

        Order Get(Guid key);

        bool Remove(Guid key);
    }
}