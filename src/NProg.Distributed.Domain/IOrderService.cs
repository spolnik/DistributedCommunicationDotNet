using System;

namespace NProg.Distributed.Domain
{
    public interface IOrderService
    {
        void Add(Guid key, Order value);

        Order Get(Guid key);

        bool Remove(Guid key);
    }
}