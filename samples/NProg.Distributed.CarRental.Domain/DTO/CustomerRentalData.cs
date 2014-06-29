using System;

namespace NProg.Distributed.CarRental.Domain.DTO
{
    [Serializable]
    public class CustomerRentalData
    {
        public int RentalId { get; set; }

        public string CustomerName { get; set; }

        public string Car { get; set; }

        public DateTime DateRented { get; set; }

        public DateTime ExpectedReturn { get; set; }
    }
}
