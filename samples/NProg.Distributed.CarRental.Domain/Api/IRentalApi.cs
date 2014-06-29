using System;
using System.Collections.Generic;
using NProg.Distributed.CarRental.Domain.DTO;

namespace NProg.Distributed.CarRental.Domain.Api
{
    public interface IRentalApi : IDisposable
    {
        Rental RentCarToCustomer(string loginEmail, int carId, DateTime dateDueBack);

        Rental RentCarToCustomer(string loginEmail, int carId, DateTime rentalDate, DateTime dateDueBack);

        void AcceptCarReturn(int carId);

        IEnumerable<Rental> GetRentalHistory(string loginEmail);

        Reservation GetReservation(int reservationId);

        Reservation MakeReservation(string loginEmail, int carId, DateTime rentalDate, DateTime returnDate);

        void ExecuteRentalFromReservation(int reservationId);

        void CancelReservation(int reservationId);

        CustomerReservationData[] GetCurrentReservations();

        CustomerReservationData[] GetCustomerReservations(string loginEmail);

        Rental GetRental(int rentalId);

        CustomerRentalData[] GetCurrentRentals();

        Reservation[] GetDeadReservations();

        bool IsCarCurrentlyRented(int carId);        
    }
}