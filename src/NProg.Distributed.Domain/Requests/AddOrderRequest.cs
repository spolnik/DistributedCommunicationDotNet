using System;

namespace NProg.Distributed.Domain.Requests
{
    [Serializable]
    public class AddOrderRequest
    {
        public Domain.Order Order { get; set; }

        public const string Name = "add-order";
    }
}