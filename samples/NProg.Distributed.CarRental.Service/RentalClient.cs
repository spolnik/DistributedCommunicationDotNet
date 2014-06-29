using System;
using System.Collections.Generic;
using NProg.Distributed.CarRental.Domain;
using NProg.Distributed.CarRental.Domain.Api;
using NProg.Distributed.CarRental.Domain.DTO;
using NProg.Distributed.Core.Service.Messaging;

namespace NProg.Distributed.CarRental.Service
{
    public class RentalClient : MessageClientBase, IRentalApi
    {

        /// <summary>
        /// Initializes a new instance of the <see cref="T:System.Object"/> class.
        /// </summary>
        public RentalClient(IRequestSender requestSender) : base(requestSender)
        {}

        #region Implementation of IRentalApi

        public Rental RentCarToCustomer(string loginEmail, int carId, DateTime dateDueBack)
        {
            throw new NotImplementedException();
        }

        public Rental RentCarToCustomer(string loginEmail, int carId, DateTime rentalDate, DateTime dateDueBack)
        {
            throw new NotImplementedException();
        }

        public void AcceptCarReturn(int carId)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Rental> GetRentalHistory(string loginEmail)
        {
            throw new NotImplementedException();
        }

        public Reservation GetReservation(int reservationId)
        {
            throw new NotImplementedException();
        }

        public Reservation MakeReservation(string loginEmail, int carId, DateTime rentalDate, DateTime returnDate)
        {
            throw new NotImplementedException();
        }

        public void ExecuteRentalFromReservation(int reservationId)
        {
            throw new NotImplementedException();
        }

        public void CancelReservation(int reservationId)
        {
            throw new NotImplementedException();
        }

        public CustomerReservationData[] GetCurrentReservations()
        {
            throw new NotImplementedException();
        }

        public CustomerReservationData[] GetCustomerReservations(string loginEmail)
        {
            throw new NotImplementedException();
        }

        public Rental GetRental(int rentalId)
        {
            throw new NotImplementedException();
        }

        public CustomerRentalData[] GetCurrentRentals()
        {
            throw new NotImplementedException();
        }

        public Reservation[] GetDeadReservations()
        {
            throw new NotImplementedException();
        }

        public bool IsCarCurrentlyRented(int carId)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}