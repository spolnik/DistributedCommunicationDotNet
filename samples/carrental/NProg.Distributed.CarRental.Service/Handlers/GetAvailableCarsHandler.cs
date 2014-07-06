using System.Linq;
using NProg.Distributed.CarRental.Data.Repository;
using NProg.Distributed.CarRental.Service.Business;
using NProg.Distributed.CarRental.Service.Queries;
using NProg.Distributed.CarRental.Service.Responses;
using NProg.Distributed.Core.Service.Messaging;

namespace NProg.Distributed.CarRental.Service.Handlers
{
    public class GetAvailableCarsHandler
        : MessageHandlerBase<GetAvailableCarsQuery, ICarRepository>
    {
        private readonly ICarRentalEngine carRentalEngine;
        private readonly IRentalRepository rentalRepository;
        private readonly IReservationRepository reservationRepository;

        /// <summary>
        /// Initializes a new instance of the MessageHandlerBase class.
        /// </summary>
        public GetAvailableCarsHandler(ICarRepository repository, IRentalRepository rentalRepository,
            IReservationRepository reservationRepository, ICarRentalEngine carRentalEngine)
            : base(repository)
        {
            this.rentalRepository = rentalRepository;
            this.reservationRepository = reservationRepository;
            this.carRentalEngine = carRentalEngine;
        }

        #region Overrides of MessageHandlerBase<GetAvailableCarsQuery,ICarRepository>

        protected override IMessage Process(GetAvailableCarsQuery command)
        {
            var allCars = repository.GetAll();
            var rentedCars = rentalRepository.GetCurrentlyRentedCars().ToList();
            var reservedCars = reservationRepository.GetAll().ToList();

            var availableCars = allCars.Where(car => 
                carRentalEngine.IsCarAvailableForRental(car.CarId, command.PickupDate, command.ReturnDate, rentedCars, reservedCars));

            return new GetCarsResponse {Cars = availableCars.ToArray()};
        }

        #endregion
    }
}