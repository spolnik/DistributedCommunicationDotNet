using NProg.Distributed.CarRental.Domain;

namespace NProg.Distributed.CarRental.Data.DTO
{
    public class CustomerReservationInfo
    {
        public Account Customer { get; set; }
        public Car Car { get; set; }
        public Reservation Reservation { get; set; }
    }
}
