using NProg.Distributed.CarRental.Data.Repository;
using NProg.Distributed.CarRental.Service.Business;
using NProg.Distributed.CarRental.Service.Requests;
using NProg.Distributed.Core.Service;
using NProg.Distributed.Core.Service.Messaging;

namespace NProg.Distributed.CarRental.Service.Handlers
{
    public class ExecuteRentalFromReservationHandler 
        : MessageHandlerBase<ExecuteRentalFromReservationRequest, IReservationRepository>
    {
        private readonly IAccountRepository accountRepository;
        private readonly ICarRentalEngine carRentalEngine;

        /// <summary>
        /// Initializes a new instance of the MessageHandlerBase class.
        /// </summary>
        public ExecuteRentalFromReservationHandler(IReservationRepository repository, IAccountRepository accountRepository, ICarRentalEngine carRentalEngine)
            : base(repository)
        {
            this.accountRepository = accountRepository;
            this.carRentalEngine = carRentalEngine;
        }

        #region Overrides of MessageHandlerBase<ExecuteRentalFromReservationRequest,IReservationRepository>

        protected override IRequestResponse Process(ExecuteRentalFromReservationRequest request)
        {
            var reservation = repository.Get(request.ReservationId);

            if (reservation == null)
            {
                throw new NotFoundException(string.Format("Reservation {0} is not found.", request.ReservationId));
            }

            var account = accountRepository.Get(reservation.AccountId);

            if (account == null)
            {
                throw new NotFoundException(string.Format("No account found for account ID '{0}'.", reservation.AccountId));
            }

            carRentalEngine.RentCarToCustomer(account.LoginEmail, reservation.CarId, reservation.RentalDate, reservation.ReturnDate);

            repository.Remove(reservation);

            return new StatusResponse {Status = true};
        }

        #endregion
    }
}