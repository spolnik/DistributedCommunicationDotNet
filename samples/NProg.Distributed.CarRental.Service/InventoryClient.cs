using System;
using NProg.Distributed.CarRental.Domain;
using NProg.Distributed.CarRental.Domain.Api;
using NProg.Distributed.CarRental.Service.Requests;
using NProg.Distributed.CarRental.Service.Responses;
using NProg.Distributed.Core.Service.Messaging;

namespace NProg.Distributed.CarRental.Service
{
    public class InventoryClient : MessageClientBase, IInventoryApi
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="T:System.Object"/> class.
        /// </summary>
        public InventoryClient(IRequestSender requestSender) : base(requestSender)
        {}

        #region IInventoryApi Members

        public Car UpdateCar(Car car)
        {
            return requestSender.Send(new AddOrUpdateCarRequest {Car = car})
                .Receive<GetCarResponse>().Car;
        }

        public bool DeleteCar(int carId)
        {
            return requestSender.Send(new DeleteCarRequest {CarId = carId})
                .Receive<StatusResponse>().Status;
        }

        public Car GetCar(int carId)
        {
            return requestSender.Send(new GetCarRequest {CarId = carId})
                .Receive<GetCarResponse>().Car;
        }

        public Car[] GetAllCars()
        {
            return requestSender.Send(new GetAllCarsRequest())
                .Receive<GetCarsResponse>().Cars;
        }

        public Car[] GetAvailableCars(DateTime pickupDate, DateTime returnDate)
        {
            return requestSender.Send(new GetAvailableCarsRequest {PickupDate = pickupDate, ReturnDate = returnDate})
                .Receive<GetCarsResponse>().Cars;
        }

        #endregion
    }
}