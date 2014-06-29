using System;
using NProg.Distributed.CarRental.Domain;
using NProg.Distributed.Core.Service.Messaging;

namespace NProg.Distributed.CarRental.Service.Responses
{
    [Serializable]
    public class GetReservationsResponse : IMessage
    {
        public Reservation[] Reservations { get; set; }
    }
}