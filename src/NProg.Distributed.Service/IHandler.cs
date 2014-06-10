using System;

namespace NProg.Distributed.Service
{
    public interface IHandler<TItem> where TItem : class 
    {
        void Add(TItem item);

        TItem Get(Guid guid);

        bool Remove(Guid guid);
    }
}