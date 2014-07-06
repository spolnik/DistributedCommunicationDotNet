using NProg.Distributed.CarRental.Data.Repository;
using NProg.Distributed.CarRental.Service.Commands;
using NProg.Distributed.Core.Service.Messaging;

namespace NProg.Distributed.CarRental.Service.Handlers
{
    public class UpdateCustomerAccountInfoHandler 
        : MessageHandlerBase<UpdateCustomerAccountInfoCommand, IAccountRepository>
    {

        /// <summary>
        /// Initializes a new instance of the MessageHandlerBase class.
        /// </summary>
        public UpdateCustomerAccountInfoHandler(IAccountRepository repository) : base(repository)
        {}

        #region Overrides of MessageHandlerBase<UpdateCustomerAccountInfoCommand,IAccountRepository>

        protected override IMessage Process(UpdateCustomerAccountInfoCommand command)
        {
            var updatedAccount = repository.Update(command.Account);
            return new StatusResponse {Status = updatedAccount != null};
        }

        #endregion
    }
}