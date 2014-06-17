namespace NProg.Distributed.Service
{
    public interface IHandler<in TKey, TValue> where TValue : class 
    {
        void Add(TKey key, TValue value);

        TValue Get(TKey key);

        bool Remove(TKey key);
    }
}