using System;

namespace NProg.Distributed.CarRental.Domain
{
    [Serializable]
    public class Rental
    {
        public int RentalId { get; set; }

        public int AccountId { get; set; }

        public int CarId { get; set; }

        public DateTime DateRented { get; set; }

        public DateTime DateDue { get; set; }

        public DateTime? DateReturned { get; set; }
    }
}
