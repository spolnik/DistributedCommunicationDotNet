using System;

namespace NProg.Distributed.Domain.Requests
{
    [Serializable]
    public class AddOrderRequest
    {
        public Order Order { get; set; }
    }
}