using System;

namespace NProg.Distributed.CarRental.Domain.Api
{
    public interface IInventoryApi
    {
        Car UpdateCar(Car car);
        
        void DeleteCar(int carId);

        Car GetCar(int carId);

        Car[] GetAllCars();

        Car[] GetAvailableCars(DateTime pickupDate, DateTime returnDate); 
    }

}