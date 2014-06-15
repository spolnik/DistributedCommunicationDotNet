using System;
using System.Collections.Concurrent;
using NProg.Distributed.Domain;
using NProg.Distributed.Service;

namespace NProg.Distributed.NDatabase
{
    public class InMemoryOrderDao : IHandler<Order>
    {
        private static readonly ConcurrentDictionary<Guid, Order> InmemoryDb = new ConcurrentDictionary<Guid, Order>();

        public void Add(Order item)
        {
            InmemoryDb.AddOrUpdate(item.OrderId, item, (guid, order) => order);
        }

        public Order Get(Guid guid)
        {
            Order order;
            InmemoryDb.TryGetValue(guid, out order);

            return order ?? new Order();
        }

        public bool Remove(Guid guid)
        {
            Order order;
            var status = InmemoryDb.TryRemove(guid, out order);

            return status;
        }
    }
}