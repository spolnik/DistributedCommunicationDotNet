using System;
using NProg.Distributed.Core.Service.Messaging;

namespace NProg.Distributed.OrderService.Requests
{
    [Serializable]
    public sealed class RemoveOrderRequest : IRequestResponse
    {
        public Guid OrderId { get; set; }
    }
}