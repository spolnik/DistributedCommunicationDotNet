using System;

namespace NProg.Distributed.Messaging.Queries
{
    public class RemoveOrderRequest
    {
        public Guid OrderId { get; set; }

        public const string Name = "remove-order";
    }
}