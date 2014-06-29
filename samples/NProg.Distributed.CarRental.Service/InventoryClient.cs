using System;
using NProg.Distributed.CarRental.Domain;
using NProg.Distributed.CarRental.Domain.Api;
using NProg.Distributed.CarRental.Service.Requests;
using NProg.Distributed.CarRental.Service.Responses;
using NProg.Distributed.Core.Service.Messaging;

namespace NProg.Distributed.CarRental.Service
{
    public class InventoryClient : IInventoryApi
    {
        /// <summary>
        /// The request sender
        /// </summary>
        private readonly IRequestSender requestSender;

        /// <summary>
        /// Initializes a new instance of the <see cref="T:System.Object"/> class.
        /// </summary>
        public InventoryClient(IRequestSender requestSender)
        {
            this.requestSender = requestSender;
        }

        #region Implementation of IInventoryApi

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

        #region Implementation of IDisposable

        /// <summary>
        /// Releases unmanaged and - optionally - managed resources.
        /// </summary>
        /// <param name="disposing"><c>true</c> to release both managed and unmanaged resources; <c>false</c> to release only unmanaged resources.</param>
        private void Dispose(bool disposing)
        {
            if (disposing && requestSender != null)
                requestSender.Dispose();
        }

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        #endregion
    }
}