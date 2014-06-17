using System;
using NProg.Distributed.Domain;
using NProg.Distributed.NDatabase;
using NProg.Distributed.Service;
using NProg.Distributed.WCF.Service;

namespace NProg.Distributed.WCF
{
    public class WcfOrderHandler : SimpleHandler<Guid, Order>, IOrderService
    {
        public WcfOrderHandler()
            : base(new OrderDaoFactory(), "order_wcf.ndb")
        {
        }
    }
}