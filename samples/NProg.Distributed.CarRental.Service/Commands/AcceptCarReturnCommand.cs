using System;
using NProg.Distributed.Core.Service.Messaging;

namespace NProg.Distributed.CarRental.Service.Commands
{
    [Serializable]
    public class AcceptCarReturnCommand : IMessage
    {
        public int CarId { get; set; }
    }
}