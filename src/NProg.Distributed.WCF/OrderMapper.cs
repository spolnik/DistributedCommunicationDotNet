using NProg.Distributed.Domain;
using NProg.Distributed.WCF.Service;

namespace NProg.Distributed.WCF
{
    internal static class OrderMapper
    {
        internal static Order MapOrder(OrderDto order)
        {
            return new Order
            {
                Count = order.Count,
                OrderDate = order.OrderDate,
                OrderId = order.OrderId,
                UnitPrice = order.UnitPrice,
                UserName = order.UserName
            };
        }

        internal static OrderDto MapOrder(Order order)
        {
            return new OrderDto
            {
                Count = order.Count,
                OrderDate = order.OrderDate,
                OrderId = order.OrderId,
                UnitPrice = order.UnitPrice,
                UserName = order.UserName
            };
        }
    }
}