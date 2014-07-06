using System;
using NProg.Distributed.Core.Service.Messaging;

namespace NProg.Distributed.OrderService.Commands
{
    [Serializable]
    public sealed class RemoveOrderCommand : IMessage
    {
        public Guid OrderId { get; set; }
    }
}