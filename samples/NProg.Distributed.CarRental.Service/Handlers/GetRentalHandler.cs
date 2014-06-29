using NProg.Distributed.CarRental.Data.Repository;
using NProg.Distributed.CarRental.Service.Queries;
using NProg.Distributed.CarRental.Service.Responses;
using NProg.Distributed.Core.Service;
using NProg.Distributed.Core.Service.Messaging;

namespace NProg.Distributed.CarRental.Service.Handlers
{
    public class GetRentalHandler 
        : MessageHandlerBase<GetRentalQuery, IRentalRepository>
    {

        /// <summary>
        /// Initializes a new instance of the MessageHandlerBase class.
        /// </summary>
        public GetRentalHandler(IRentalRepository repository) : base(repository)
        {}

        #region Overrides of MessageHandlerBase<GetRentalHistoryQuery,IRentalRepository>

        protected override IMessage Process(GetRentalQuery command)
        {
            var rental = repository.Get(command.RentalId);

            if (rental == null)
            {
                throw new NotFoundException(string.Format("No rental record found for id '{0}'.", command.RentalId));
            }

            return new GetRentalResponse {Rental = rental};
        }

        #endregion
    }
}