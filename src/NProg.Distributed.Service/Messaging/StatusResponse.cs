using System;

namespace NProg.Distributed.Core.Service.Messaging
{
    [Serializable]
    public sealed class StatusResponse : IRequestResponse
    {
        public bool Status { get; set; }
    }
}