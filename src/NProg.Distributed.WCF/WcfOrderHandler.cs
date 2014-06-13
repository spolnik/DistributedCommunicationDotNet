using System;
using NProg.Distributed.Domain;
using NProg.Distributed.NDatabase;
using NProg.Distributed.Service;
using NProg.Distributed.WCF.Service;

namespace NProg.Distributed.WCF
{
    public class WcfOrderHandler : IHandler<Order>, IOrderService
    {
        private readonly NdbOrderDao ndbOrderDao;

        public WcfOrderHandler()
        {
            ndbOrderDao = new NdbOrderDao("order_wcf.ndb");
        }

        public void Add(Order item)
        {
            ndbOrderDao.Add(item);
        }

        public void Add(OrderDto item)
        {
            Add(OrderMapper.MapOrder(item));
        }

        OrderDto IOrderService.Get(Guid guid)
        {
            return OrderMapper.MapOrder(Get(guid));
        }

        public Order Get(Guid guid)
        {
            return ndbOrderDao.Get(guid);
        }

        public bool Remove(Guid guid)
        {
            return ndbOrderDao.Remove(guid);
        }
    }
}