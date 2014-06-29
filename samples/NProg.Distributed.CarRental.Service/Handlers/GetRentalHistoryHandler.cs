using System.Linq;
using NProg.Distributed.CarRental.Data.Repository;
using NProg.Distributed.CarRental.Service.Queries;
using NProg.Distributed.CarRental.Service.Responses;
using NProg.Distributed.Core.Service;
using NProg.Distributed.Core.Service.Messaging;

namespace NProg.Distributed.CarRental.Service.Handlers
{
    public class GetRentalHistoryHandler 
        : MessageHandlerBase<GetRentalHistoryQuery, IRentalRepository>
    {
        private readonly IAccountRepository accountRepository;

        /// <summary>
        /// Initializes a new instance of the MessageHandlerBase class.
        /// </summary>
        public GetRentalHistoryHandler(IRentalRepository repository, IAccountRepository accountRepository) : base(repository)
        {
            this.accountRepository = accountRepository;
        }

        #region Overrides of MessageHandlerBase<GetRentalHistoryQuery,IRentalRepository>

        protected override IMessage Process(GetRentalHistoryQuery command)
        {
            var account = accountRepository.GetByLogin(command.LoginEmail);

            if (account == null)
            {
                throw new NotFoundException(string.Format("No account found for login '{0}'.", command.LoginEmail));
            }

            var rentalHistory = repository.GetRentalHistoryByAccount(account.AccountId);

            return new GetRentalHistoryResponse {Rentals = rentalHistory.ToArray()};
        }

        #endregion
    }
}