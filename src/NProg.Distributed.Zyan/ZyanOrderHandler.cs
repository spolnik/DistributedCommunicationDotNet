using NProg.Distributed.NDatabase;

namespace NProg.Distributed.Zyan
{
    public class ZyanOrderHandler : SimpleOrderHandler
    {
        public ZyanOrderHandler()
            : base(new OrderDaoFactory(), "order_zyan.ndb")
        {
        }
    }
}