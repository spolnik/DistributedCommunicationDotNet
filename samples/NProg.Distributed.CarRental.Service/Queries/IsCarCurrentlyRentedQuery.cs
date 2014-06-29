using System;
using NProg.Distributed.Core.Service.Messaging;

namespace NProg.Distributed.CarRental.Service.Queries
{
    [Serializable]
    public class IsCarCurrentlyRentedQuery : IMessage
    {
        public int CarId { get; set; }
    }
}