using System;
using NProg.Distributed.Core.Data;

namespace NProg.Distributed.CarRental.Domain
{
    [Serializable]
    public class Reservation : IIdentifiableEntity<int>
    {
        public int ReservationId { get; set; }

        public int AccountId { get; set; }

        public int CarId { get; set; }

        public DateTime RentalDate { get; set; }

        public DateTime ReturnDate { get; set; }

        #region Implementation of IIdentifiableEntity<int>

        public int EntityId
        {
            get { return ReservationId; }
            set { ReservationId = value; }
        }

        #endregion
    }
}
