using System;

namespace NProg.Distributed.CarRental.Domain.DTO
{
    [Serializable]
    public class CustomerReservationData
    {
        public int ReservationId { get; set; }

        public string CustomerName { get; set; }

        public string Car { get; set; }
        public DateTime RentalDate { get; set; }

        public DateTime ReturnDate { get; set; }
    }
}
