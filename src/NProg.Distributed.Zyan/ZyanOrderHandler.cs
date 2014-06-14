using NProg.Distributed.NDatabase;

namespace NProg.Distributed.Zyan
{
    public class ZyanOrderHandler : SimpleOrderHandler
    {
        public ZyanOrderHandler()
            : base("order_zyan.ndb")
        {
        }
    }
}