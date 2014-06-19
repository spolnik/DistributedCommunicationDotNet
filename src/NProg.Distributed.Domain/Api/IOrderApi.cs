using System;

namespace NProg.Distributed.Domain.Api
{
    public interface IOrderApi
    {
        void Add(Guid key, Order value);

        Order Get(Guid key);

        bool Remove(Guid key);
    }
}