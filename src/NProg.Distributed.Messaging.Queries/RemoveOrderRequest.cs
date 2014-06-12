using System;

namespace NProg.Distributed.Messaging.Order.Queries
{
    public class RemoveOrderRequest
    {
        public Guid OrderId { get; set; }

        public const string Name = "remove-order";
    }
}