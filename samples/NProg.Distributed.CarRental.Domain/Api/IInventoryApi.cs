using System;

namespace NProg.Distributed.CarRental.Domain.Api
{
    public interface IInventoryApi : IDisposable
    {
        Car UpdateCar(Car car);
        
        bool DeleteCar(int carId);

        Car GetCar(int carId);

        Car[] GetAllCars();

        Car[] GetAvailableCars(DateTime pickupDate, DateTime returnDate); 
    }

}