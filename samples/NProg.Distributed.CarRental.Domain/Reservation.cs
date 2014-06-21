using System;

namespace NProg.Distributed.CarRental.Domain
{
    [Serializable]
    public class Reservation
    {
        public int ReservationId { get; set; }

        public int AccountId { get; set; }

        public int CarId { get; set; }

        public DateTime RentalDate { get; set; }

        public DateTime ReturnDate { get; set; }
    }
}
