using NProg.Distributed.NDatabase;
using NProg.Distributed.WCF.Service;

namespace NProg.Distributed.WCF
{
    public class WcfOrderHandler : SimpleOrderHandler, IOrderService
    {
        public WcfOrderHandler()
            : base("order_wcf.ndb")
        {
            
        }
    }
}