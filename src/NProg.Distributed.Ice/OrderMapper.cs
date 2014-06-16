using System;
using Order;

namespace NProg.Distributed.Ice
{
    internal static class OrderMapper
    {
        internal static Domain.Order MapOrder(OrderDto order)
        {
            return new Domain.Order
            {
                Count = order.count,
                OrderDate = new DateTime(order.orderDate),
                OrderId = Guid.Parse(order.orderId),
                UnitPrice = (decimal) order.unitPrice,
                UserName = order.userName
            };
        }

        internal static OrderDto MapOrder(Domain.Order order)
        {
            return new OrderDto
            {
                count = order.Count,
                orderDate = order.OrderDate.Ticks,
                orderId = order.OrderId.ToString(),
                unitPrice = Convert.ToDouble(order.UnitPrice),
                userName = order.UserName
            };
        }
    }
}