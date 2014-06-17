using System.Collections.Concurrent;
using NProg.Distributed.Service;

namespace NProg.Distributed.NDatabase
{
    public class InMemoryDao<TKey, TValue> : IHandler<TKey, TValue> where TValue 
        : class, new()
    {
        private static readonly ConcurrentDictionary<TKey, TValue> InmemoryDb = new ConcurrentDictionary<TKey, TValue>();

        public void Add(TKey key, TValue value)
        {
            InmemoryDb.AddOrUpdate(key, value, (guid, order) => order);
        }

        public TValue Get(TKey key)
        {
            TValue value;
            InmemoryDb.TryGetValue(key, out value);

            return value ?? new TValue();
        }

        public bool Remove(TKey key)
        {
            TValue value;
            return InmemoryDb.TryRemove(key, out value);
        }
    }
}