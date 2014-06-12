using System;

namespace NProg.Distributed.ZeroMQ.Queries
{
    public class GetOrderRequest
    {
        public Guid OrderId { get; set; }

        internal const string Name = "get-order";
    }
}