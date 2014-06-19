using System;

namespace NProg.Distributed.Domain.Responses
{
    [Serializable]
    public class GetOrderResponse
    {
        public Domain.Order Order { get; set; } 
    }
}