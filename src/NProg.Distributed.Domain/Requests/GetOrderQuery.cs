using System;
using NProg.Distributed.Core.Service.Messaging;

namespace NProg.Distributed.OrderService.Requests
{
    [Serializable]
    public sealed class GetOrderQuery : IMessage
    {
        public Guid OrderId { get; set; }
    }
}