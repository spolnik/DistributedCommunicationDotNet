﻿using System;
using System.Collections.Generic;
using NProg.Distributed.CarRental.Domain;
using NProg.Distributed.Core.Service;

namespace NProg.Distributed.CarRental.Service.Business
{
    public interface ICarRentalEngine : IBusinessEngine
    {
        bool IsCarCurrentlyRented(int carId, int accountId);
     
        bool IsCarCurrentlyRented(int carId);
        
        bool IsCarAvailableForRental(int carId, DateTime pickupDate, DateTime returnDate,
                                     IEnumerable<Rental> rentedCars, IEnumerable<Reservation> reservedCars);
        
        Rental RentCarToCustomer(string loginEmail, int carId, DateTime rentalDate, DateTime dateDueBack);
    }

}