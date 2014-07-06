using System;
using NProg.Distributed.Core.Service.Messaging;

namespace NProg.Distributed.CarRental.Service.Commands
{
    [Serializable]
    public class ExecuteRentalFromReservationCommand : IMessage
    {
        public int ReservationId { get; set; }
    }
}