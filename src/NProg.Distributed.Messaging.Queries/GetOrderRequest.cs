using System;

namespace NProg.Distributed.Messaging.Queries
{
    public class GetOrderRequest
    {
        public Guid OrderId { get; set; }

        public const string Name = "get-order";
    }
}