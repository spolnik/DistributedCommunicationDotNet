using System;

namespace NProg.Distributed.Domain.Requests
{
    [Serializable]
    public class RemoveOrderRequest
    {
        public Guid OrderId { get; set; }
    }
}