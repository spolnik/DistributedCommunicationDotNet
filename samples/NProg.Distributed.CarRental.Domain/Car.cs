using System;

namespace NProg.Distributed.CarRental.Domain
{
    [Serializable]
    public class Car
    {
        public int CarId { get; set; }

        public string Description { get; set; }

        public string Color { get; set; }

        public int Year { get; set; }

        public decimal RentalPrice { get; set; }

        public bool CurrentlyRented { get; set; }
    }
}
