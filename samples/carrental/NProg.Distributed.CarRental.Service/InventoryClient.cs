using System;
using NProg.Distributed.CarRental.Domain;
using NProg.Distributed.CarRental.Domain.Api;
using NProg.Distributed.CarRental.Service.Commands;
using NProg.Distributed.CarRental.Service.Queries;
using NProg.Distributed.CarRental.Service.Responses;
using NProg.Distributed.Core.Service.Messaging;

namespace NProg.Distributed.CarRental.Service
{
    public class InventoryClient : MessageClientBase, IInventoryApi
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="T:System.Object"/> class.
        /// </summary>
        public InventoryClient(IMessageSender messageSender) : base(messageSender)
        {}

        #region IInventoryApi Members

        public Car UpdateCar(Car car)
        {
            return messageSender.Send(new AddOrUpdateCarCommand {Car = car})
                .Receive<GetCarResponse>().Car;
        }

        public bool DeleteCar(int carId)
        {
            return messageSender.Send(new DeleteCarCommand {CarId = carId})
                .Receive<StatusResponse>().Status;
        }

        public Car GetCar(int carId)
        {
            return messageSender.Send(new GetCarQuery {CarId = carId})
                .Receive<GetCarResponse>().Car;
        }

        public Car[] GetAllCars()
        {
            return messageSender.Send(new GetAllCarsQuery())
                .Receive<GetCarsResponse>().Cars;
        }

        public Car[] GetAvailableCars(DateTime pickupDate, DateTime returnDate)
        {
            return messageSender.Send(new GetAvailableCarsQuery {PickupDate = pickupDate, ReturnDate = returnDate})
                .Receive<GetCarsResponse>().Cars;
        }

        #endregion
    }
}