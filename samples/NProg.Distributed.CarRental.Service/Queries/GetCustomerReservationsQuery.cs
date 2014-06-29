using System;
using NProg.Distributed.Core.Service.Messaging;

namespace NProg.Distributed.CarRental.Service.Queries
{
    [Serializable]
    public class GetCustomerReservationsQuery : IMessage
    {
        public string LoginEmail { get; set; }
    }
}