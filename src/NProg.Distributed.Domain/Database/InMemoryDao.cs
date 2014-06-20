using System;
using System.Collections.Concurrent;
using NProg.Distributed.OrderService.Api;
using NProg.Distributed.OrderService.Domain;

namespace NProg.Distributed.OrderService.Database
{
    internal sealed class InMemoryDao : IOrderApi
    {
        private static readonly ConcurrentDictionary<Guid, Order> InMemoryDb = new ConcurrentDictionary<Guid, Order>();

        public bool Add(Guid key, Order value)
        {
            InMemoryDb.AddOrUpdate(key, value, (guid, order) => order);

            return true;
        }

        public Order Get(Guid key)
        {
            Order value;
            InMemoryDb.TryGetValue(key, out value);

            return value ?? new Order();
        }

        public bool Remove(Guid key)
        {
            Order value;
            return InMemoryDb.TryRemove(key, out value);
        }
    }
}