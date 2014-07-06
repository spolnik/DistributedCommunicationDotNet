using System;
using NProg.Distributed.Core.Service.Messaging;

namespace NProg.Distributed.CarRental.Service.Queries
{
    [Serializable]
    public class GetReservationQuery : IMessage
    {
        public int ReservationId { get; set; }
    }
}