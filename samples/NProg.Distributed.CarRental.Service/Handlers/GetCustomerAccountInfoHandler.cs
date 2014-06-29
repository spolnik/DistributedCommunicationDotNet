using NProg.Distributed.CarRental.Data.Repository;
using NProg.Distributed.CarRental.Service.Requests;
using NProg.Distributed.CarRental.Service.Responses;
using NProg.Distributed.Core.Service;
using NProg.Distributed.Core.Service.Messaging;

namespace NProg.Distributed.CarRental.Service.Handlers
{
    public sealed class GetCustomerAccountInfoHandler :
        MessageHandlerBase<GetCustomerAccountInfoRequest, IAccountRepository>
    {

        /// <summary>
        /// Initializes a new instance of the MessageHandlerBase class.
        /// </summary>
        public GetCustomerAccountInfoHandler(IAccountRepository repository)
            : base(repository)
        {}

        protected override IRequestResponse Process(GetCustomerAccountInfoRequest request)
        {
            var accountEntity = repository.GetByLogin(request.LoginEmail);

            if (accountEntity == null)
            {
                throw new NotFoundException(string.Format("Account with login {0} is not in database",
                    request.LoginEmail));
            }

            return new GetAccountResponse {Account = accountEntity};
        }

    }
}