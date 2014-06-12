namespace NProg.Distributed.Messaging.Order.Queries
{
    public class AddOrderRequest
    {
        public Domain.Order Order { get; set; }

        public const string Name = "add-order";
    }
}