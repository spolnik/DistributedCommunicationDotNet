using NProg.Distributed.CarRental.Domain;

namespace NProg.Distributed.CarRental.Data.DTO
{
    public class CustomerRentalInfo
    {
        public Account Customer { get; set; }
        public Car Car { get; set; }
        public Rental Rental { get; set; }
    }
}