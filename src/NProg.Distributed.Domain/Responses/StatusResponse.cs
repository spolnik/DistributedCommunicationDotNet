using System;

namespace NProg.Distributed.OrderService.Responses
{
    [Serializable]
    public sealed class StatusResponse
    {
        public bool Status { get; set; }
    }
}