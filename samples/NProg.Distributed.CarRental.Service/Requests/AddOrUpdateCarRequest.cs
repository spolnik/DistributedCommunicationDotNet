using System;
using NProg.Distributed.CarRental.Domain;
using NProg.Distributed.Core.Service.Messaging;

namespace NProg.Distributed.CarRental.Service.Requests
{
    [Serializable]
    public class AddOrUpdateCarRequest : IRequestResponse
    {
        public Car Car { get; set; }
    }
}