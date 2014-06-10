using System;
using System.Globalization;
using NProg.Distributed.Domain;

namespace NProg.Distributed.Thrift
{
    internal static class OrderMapper
    {
        internal static Order MapOrder(ThriftOrder order)
        {
            return new Order
            {
                Count = order.Count,
                OrderDate = DateTime.Parse(order.OrderDate),
                OrderId = Guid.Parse(order.OrderId),
                UnitPrice = (decimal) order.UnitPrice,
                UserName = order.UserName
            };
        }

        internal static ThriftOrder MapOrder(Order order)
        {
            return new ThriftOrder
            {
                Count = order.Count,
                OrderDate = order.OrderDate.ToString(CultureInfo.InvariantCulture),
                OrderId = order.OrderId.ToString(),
                UnitPrice = Convert.ToDouble(order.UnitPrice),
                UserName = order.UserName
            };
        }
    }
}