using System;
using NProg.Distributed.Core.Service.Messaging;

namespace NProg.Distributed.CarRental.Service.Requests
{
    [Serializable]
    public class ExecuteRentalFromReservationRequest : IRequestResponse
    {
        public int ReservationId { get; set; }
    }
}