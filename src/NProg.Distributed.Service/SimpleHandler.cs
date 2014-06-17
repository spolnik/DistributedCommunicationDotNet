namespace NProg.Distributed.Service
{
    public class SimpleHandler<TKey, TValue> : IHandler<TKey, TValue> where TValue : class
    {
        private readonly IHandler<TKey, TValue> dao;

        public SimpleHandler(IDaoFactory<TKey, TValue> daoFactory, string dbName)
        {
            dao = daoFactory.CreateDao(dbName);
        }

        public void Add(TKey key, TValue item)
        {
            dao.Add(key, item);
        }

        public TValue Get(TKey key)
        {
            return dao.Get(key);
        }

        public bool Remove(TKey key)
        {
            return dao.Remove(key);
        }
    }
}