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

        #region Equality members

        protected bool Equals(Car other)
        {
            return CarId == other.CarId && string.Equals(Description, other.Description) &&
                   string.Equals(Color, other.Color) && Year == other.Year && RentalPrice == other.RentalPrice &&
                   CurrentlyRented.Equals(other.CurrentlyRented);
        }

        /// <summary>
        /// Determines whether the specified <see cref="T:System.Object"/> is equal to the current <see cref="T:System.Object"/>.
        /// </summary>
        /// <returns>
        /// true if the specified object  is equal to the current object; otherwise, false.
        /// </returns>
        /// <param name="obj">The object to compare with the current object. </param>
        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj))
            {
                return false;
            }
            if (ReferenceEquals(this, obj))
            {
                return true;
            }
            if (obj.GetType() != this.GetType())
            {
                return false;
            }
            return Equals((Car) obj);
        }

        /// <summary>
        /// Serves as a hash function for a particular type. 
        /// </summary>
        /// <returns>
        /// A hash code for the current <see cref="T:System.Object"/>.
        /// </returns>
        public override int GetHashCode()
        {
            unchecked
            {
                int hashCode = CarId;
                hashCode = (hashCode*397) ^ (Description != null
                    ? Description.GetHashCode()
                    : 0);
                hashCode = (hashCode*397) ^ (Color != null
                    ? Color.GetHashCode()
                    : 0);
                hashCode = (hashCode*397) ^ Year;
                hashCode = (hashCode*397) ^ RentalPrice.GetHashCode();
                hashCode = (hashCode*397) ^ CurrentlyRented.GetHashCode();
                return hashCode;
            }
        }

        #endregion

        /// <summary>
        /// Returns a string that represents the current object.
        /// </summary>
        /// <returns>
        /// A string that represents the current object.
        /// </returns>
        public override string ToString()
        {
            return
                string.Format(
                    "CarId: {0}, Description: {1}, Color: {2}, Year: {3}, RentalPrice: {4}, CurrentlyRented: {5}, EntityId: {6}",
                    CarId, Description, Color, Year, RentalPrice, CurrentlyRented, EntityId);
        }
    }
}
