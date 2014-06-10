using System;

namespace NProg.Distributed.Domain
{
    public class Order
    {
        public Guid OrderId { get; set; }

        public int Count { get; set; }

        public DateTime OrderDate { get; set; }

        public decimal UnitPrice { get; set; }

        public string UserName { get; set; }

        public override string ToString()
        {
            return string.Format("OrderId: {0}, Count: {1}, OrderDate: {2}, UnitPrice: {3}, UserName: {4}", OrderId,
                Count, OrderDate, UnitPrice, UserName);
        }
    }
}
