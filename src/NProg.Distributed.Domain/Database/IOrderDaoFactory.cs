using NProg.Distributed.OrderService.Api;

namespace NProg.Distributed.OrderService.Database
{
    public interface IOrderDaoFactory
    {
        IOrderApi CreateDao();
    }
}