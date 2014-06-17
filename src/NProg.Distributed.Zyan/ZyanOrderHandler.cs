using System;
using NProg.Distributed.Domain;
using NProg.Distributed.NDatabase;
using NProg.Distributed.Service;

namespace NProg.Distributed.Zyan
{
    public class ZyanOrderHandler : SimpleHandler<Guid, Order>
    {
        public ZyanOrderHandler()
            : base(new OrderDaoFactory(), "order_zyan.ndb")
        {
        }
    }
}