using System;
using System.Collections.Concurrent;
using NProg.Distributed.Domain;
using NProg.Distributed.Domain.Api;

namespace NProg.Distributed.NDatabase
{
    public class InMemoryDao : IOrderApi
    {
        private static readonly ConcurrentDictionary<Guid, Order> InmemoryDb = new ConcurrentDictionary<Guid, Order>();

        public bool Add(Guid key, Order value)
        {
            InmemoryDb.AddOrUpdate(key, value, (guid, order) => order);

            return true;
        }

        public Order Get(Guid key)
        {
            Order value;
            InmemoryDb.TryGetValue(key, out value);

            return value ?? new Order();
        }

        public bool Remove(Guid key)
        {
            Order value;
            return InmemoryDb.TryRemove(key, out value);
        }
    }
}