using System;

namespace NProg.Distributed.Domain.Requests
{
    [Serializable]
    public class GetOrderRequest
    {
        public Guid OrderId { get; set; }
    }
}