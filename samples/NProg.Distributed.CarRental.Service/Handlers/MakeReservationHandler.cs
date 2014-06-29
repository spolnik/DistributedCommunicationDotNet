using NProg.Distributed.CarRental.Data.Repository;
using NProg.Distributed.CarRental.Domain;
using NProg.Distributed.CarRental.Service.Requests;
using NProg.Distributed.CarRental.Service.Responses;
using NProg.Distributed.Core.Service;
using NProg.Distributed.Core.Service.Messaging;

namespace NProg.Distributed.CarRental.Service.Handlers
{
    public class MakeReservationHandler 
        : MessageHandlerBase<MakeReservationRequest, IReservationRepository>
    {
        private readonly IAccountRepository accountRepository;

        /// <summary>
        /// Initializes a new instance of the MessageHandlerBase class.
        /// </summary>
        public MakeReservationHandler(IReservationRepository repository, IAccountRepository accountRepository)
            : base(repository)
        {
            this.accountRepository = accountRepository;
        }

        #region Overrides of MessageHandlerBase<MakeReservationRequest,IReservationRepository>

        protected override IRequestResponse Process(MakeReservationRequest request)
        {
            var account = accountRepository.GetByLogin(request.LoginEmail);

            if (account == null)
            {
                throw new NotFoundException(string.Format("No account found for login '{0}'.", request.LoginEmail));
            }

            var reservation = new Reservation
                {
                    AccountId = account.AccountId,
                    CarId = request.CarId,
                    RentalDate = request.RentalDate,
                    ReturnDate = request.ReturnDate
                };

            var savedEntity = repository.Add(reservation);

            return new GetReservationResponse {Reservation = savedEntity};
        }

        #endregion
    }
}