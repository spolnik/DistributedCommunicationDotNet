using System;
using NProg.Distributed.CarRental.Domain;
using NProg.Distributed.Core.Service.Messaging;

namespace NProg.Distributed.CarRental.Service.Commands
{
    [Serializable]
    public class AddOrUpdateCarCommand : IMessage
    {
        public Car Car { get; set; }
    }
}