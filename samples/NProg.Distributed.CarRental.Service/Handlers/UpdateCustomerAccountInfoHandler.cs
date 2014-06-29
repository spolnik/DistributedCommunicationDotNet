using NProg.Distributed.CarRental.Data.Repository;
using NProg.Distributed.CarRental.Service.Requests;
using NProg.Distributed.Core.Service.Messaging;

namespace NProg.Distributed.CarRental.Service.Handlers
{
    public class UpdateCustomerAccountInfoHandler 
        : MessageHandlerBase<UpdateCustomerAccountInfoRequest, IAccountRepository>
    {

        /// <summary>
        /// Initializes a new instance of the MessageHandlerBase class.
        /// </summary>
        public UpdateCustomerAccountInfoHandler(IAccountRepository repository) : base(repository)
        {}

        #region Overrides of MessageHandlerBase<UpdateCustomerAccountInfoRequest,IAccountRepository>

        protected override IRequestResponse Process(UpdateCustomerAccountInfoRequest request)
        {
            var updatedAccount = repository.Update(request.Account);
            return new StatusResponse {Status = updatedAccount != null};
        }

        #endregion
    }
}