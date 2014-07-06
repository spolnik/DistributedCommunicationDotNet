using System;
using NProg.Distributed.Core.Service.Messaging;

namespace NProg.Distributed.CarRental.Service.Commands
{
    [Serializable]
    public class MakeReservationCommand : IMessage
    {
        public string LoginEmail { get; set; }

        public int CarId { get; set; }

        public DateTime RentalDate { get; set; }

        public DateTime ReturnDate { get; set; }
    }
}