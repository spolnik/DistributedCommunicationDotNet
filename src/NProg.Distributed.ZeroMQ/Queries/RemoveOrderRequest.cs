using System;

namespace NProg.Distributed.ZeroMQ.Queries
{
    public class RemoveOrderRequest
    {
        public Guid OrderId { get; set; }

        internal const string Name = "remove-order";
    }
}