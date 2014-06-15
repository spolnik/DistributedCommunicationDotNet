using NProg.Distributed.Service;

namespace NProg.Distributed.NDatabase
{
    public interface IDaoFactory<TItem> where TItem : class 
    {
        IHandler<TItem> CreateDao(string dbName);
    }
}