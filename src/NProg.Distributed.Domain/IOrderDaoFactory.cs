using NProg.Distributed.Domain.Api;

namespace NProg.Distributed.Domain
{
    public interface IOrderDaoFactory
    {
        IOrderApi CreateDao(string dbName);
    }
}