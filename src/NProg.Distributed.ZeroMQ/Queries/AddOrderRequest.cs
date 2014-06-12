using NProg.Distributed.Domain;

namespace NProg.Distributed.ZeroMQ.Queries
{
    public class AddOrderRequest
    {
        public Order Order { get; set; }

        internal const string Name = "add-order";
    }
}