using System;
using NProg.Distributed.Core.Service.Messaging;

namespace NProg.Distributed.OrderService.Responses
{
    [Serializable]
    public sealed class StatusResponse : IRequestResponse
    {
        public bool Status { get; set; }
    }
}