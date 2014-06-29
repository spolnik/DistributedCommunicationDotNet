using System;

namespace NProg.Distributed.Core.Service.Messaging
{
    [Serializable]
    public sealed class StatusResponse : IMessage
    {
        public bool Status { get; set; }
    }
}