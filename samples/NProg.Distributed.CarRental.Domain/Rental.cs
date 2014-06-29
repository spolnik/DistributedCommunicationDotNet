using System;
using NProg.Distributed.Core.Data;

namespace NProg.Distributed.CarRental.Domain
{
    [Serializable]
    public class Rental : IIdentifiableEntity<int>
    {
        public int RentalId { get; set; }

        public int AccountId { get; set; }

        public int CarId { get; set; }

        public DateTime DateRented { get; set; }

        public DateTime DateDue { get; set; }

        public DateTime? DateReturned { get; set; }

        #region Implementation of IIdentifiableEntity<int>

        public int EntityId
        {
            get { return RentalId; }
            set { RentalId = value; }
        }

        #endregion
    }
}
