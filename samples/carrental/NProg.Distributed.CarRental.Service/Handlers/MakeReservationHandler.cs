using NProg.Distributed.CarRental.Data.Repository;
using NProg.Distributed.CarRental.Domain;
using NProg.Distributed.CarRental.Service.Commands;
using NProg.Distributed.CarRental.Service.Responses;
using NProg.Distributed.Core.Service;
using NProg.Distributed.Core.Service.Messaging;

namespace NProg.Distributed.CarRental.Service.Handlers
{
    public class MakeReservationHandler 
        : MessageHandlerBase<MakeReservationCommand, IReservationRepository>
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

        #region Overrides of MessageHandlerBase<MakeReservationCommand,IReservationRepository>

        protected override IMessage Process(MakeReservationCommand command)
        {
            var account = accountRepository.GetByLogin(command.LoginEmail);

            if (account == null)
            {
                throw new NotFoundException(string.Format("No account found for login '{0}'.", command.LoginEmail));
            }

            var reservation = new Reservation
                {
                    AccountId = account.AccountId,
                    CarId = command.CarId,
                    RentalDate = command.RentalDate,
                    ReturnDate = command.ReturnDate
                };

            var savedEntity = repository.Add(reservation);

            return new GetReservationResponse {Reservation = savedEntity};
        }

        #endregion
    }
}