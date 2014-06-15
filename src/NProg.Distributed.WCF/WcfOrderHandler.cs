using NProg.Distributed.NDatabase;
using NProg.Distributed.WCF.Service;

namespace NProg.Distributed.WCF
{
    public class WcfOrderHandler : SimpleOrderHandler, IOrderService
    {
        public WcfOrderHandler()
            : base(new OrderDaoFactory(), "order_wcf.ndb")
        {
            
        }
    }
}