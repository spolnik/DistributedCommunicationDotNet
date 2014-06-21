using System;
using NProg.Distributed.Core.Data;

namespace NProg.Distributed.CarRental.Domain
{
    [Serializable]
    public class Car : IIdentifiableEntity<int>
    {
        public int CarId { get; set; }

        public string Description { get; set; }

        public string Color { get; set; }

        public int Year { get; set; }

        public decimal RentalPrice { get; set; }

        public bool CurrentlyRented { get; set; }

        #region Implementation of IIdentifiableEntity<int>

        public int EntityId
        {
            get { return CarId; }
            set { CarId = value; }
        }

        #endregion
    }
}
