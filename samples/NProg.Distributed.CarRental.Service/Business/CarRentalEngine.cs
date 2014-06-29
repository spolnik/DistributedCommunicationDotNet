using System;
using System.Collections.Generic;
using System.Linq;
using NProg.Distributed.CarRental.Data.Repository;
using NProg.Distributed.CarRental.Domain;
using NProg.Distributed.Core.Service;

namespace NProg.Distributed.CarRental.Service.Business
{
    public class CarRentalEngine : ICarRentalEngine
    {
        private readonly IRentalRepository rentalRepository;
        private readonly IAccountRepository accountRepository;

        public CarRentalEngine(IRentalRepository rentalRepository, IAccountRepository accountRepository)
        {
            this.rentalRepository = rentalRepository;
            this.accountRepository = accountRepository;
        }

        public bool IsCarCurrentlyRented(int carId, int accountId)
        {
            var rented = false;

            var currentRental = rentalRepository.GetCurrentRentalByCar(carId);
            if (currentRental != null && currentRental.AccountId == accountId)
                rented = true;

            return rented;
        }

        public bool IsCarCurrentlyRented(int carId)
        {
            var rented = false;

            var currentRental = rentalRepository.GetCurrentRentalByCar(carId);
            if (currentRental != null)
                rented = true;

            return rented;
        }

        public bool IsCarAvailableForRental(int carId, DateTime pickupDate, DateTime returnDate,
            IEnumerable<Rental> rentedCars, IEnumerable<Reservation> reservedCars)
        {
            var available = true;

            var reservation = reservedCars.FirstOrDefault(item => item.CarId == carId);

            if (reservation != null && (
                (pickupDate >= reservation.RentalDate && pickupDate <= reservation.ReturnDate) ||
                (returnDate >= reservation.RentalDate && returnDate <= reservation.ReturnDate)))
            {
                available = false;
            }

            if (!available)
            {
                return false;
            }

            var rental = rentedCars.FirstOrDefault(item => item.CarId == carId);
            if (rental != null && (pickupDate <= rental.DateDue))
                available = false;

            return available;
        }

        public Rental RentCarToCustomer(string loginEmail, int carId, DateTime rentalDate, DateTime dateDueBack)
        {
            if (rentalDate > DateTime.Now)
                throw new UnableToRentForDateException(string.Format("Cannot rent for date {0} yet.", rentalDate.ToShortDateString()));

            var carIsRented = IsCarCurrentlyRented(carId);
            if (carIsRented)
                throw new CarCurrentlyRentedException(string.Format("Car {0} is already rented.", carId));

            var account = accountRepository.GetByLogin(loginEmail);
            if (account == null)
                throw new NotFoundException(string.Format("No account found for login '{0}'.", loginEmail));

            var rental = new Rental
                {
                    AccountId = account.AccountId,
                    CarId = carId,
                    DateRented = rentalDate,
                    DateDue = dateDueBack
                };

            return rentalRepository.Add(rental);
        }
    }
}