﻿using System.Linq;
using NProg.Distributed.CarRental.Data.Repository;
using NProg.Distributed.CarRental.Service.Queries;
using NProg.Distributed.CarRental.Service.Responses;
using NProg.Distributed.Core.Service.Messaging;

namespace NProg.Distributed.CarRental.Service.Handlers
{
    public class GetAllCarsHandler 
        : MessageHandlerBase<GetAllCarsQuery, ICarRepository>
    {
        private readonly IRentalRepository rentalRepository;

        /// <summary>
        /// Initializes a new instance of the MessageHandlerBase class.
        /// </summary>
        public GetAllCarsHandler(ICarRepository repository, IRentalRepository rentalRepository) : base(repository)
        {
            this.rentalRepository = rentalRepository;
        }

        #region Overrides of MessageHandlerBase<GetAllCarsQuery,ICarRepository>

        protected override IMessage Process(GetAllCarsQuery command)
        {
            var cars = repository.GetAll().ToList();
            var rentedCars = rentalRepository.GetCurrentlyRentedCars().ToList();

            foreach (var car in cars)
            {
                var rentedCar = rentedCars.FirstOrDefault(item => item.CarId == car.CarId);
                car.CurrentlyRented = (rentedCar != null);
            }

            return new GetCarsResponse {Cars = cars.ToArray()};
        }

        #endregion
    }
}