namespace NProg.Distributed.Service
{
    public interface IDaoFactory<in TKey, TValue> where TValue : class 
    {
        IHandler<TKey, TValue> CreateDao(string dbName);
    }
}