using System;

namespace NProg.Distributed.Domain.Requests
{
    [Serializable]
    public class GetOrderRequest
    {
        public Guid OrderId { get; set; }

        public const string Name = "get-order";
    }
}