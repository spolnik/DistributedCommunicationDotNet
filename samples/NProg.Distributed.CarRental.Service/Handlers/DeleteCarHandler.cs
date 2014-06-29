using NProg.Distributed.CarRental.Data.Repository;
using NProg.Distributed.CarRental.Service.Requests;
using NProg.Distributed.Core.Service.Messaging;

namespace NProg.Distributed.CarRental.Service.Handlers
{
    public class DeleteCarHandler 
        : MessageHandlerBase<DeleteCarRequest, ICarRepository>
    {

        /// <summary>
        /// Initializes a new instance of the MessageHandlerBase class.
        /// </summary>
        public DeleteCarHandler(ICarRepository repository) : base(repository)
        {}

        #region Overrides of MessageHandlerBase<DeleteCarRequest,ICarRepository>

        protected override IRequestResponse Process(DeleteCarRequest request)
        {
            return new StatusResponse {Status = repository.Remove(request.CarId)};
        }

        #endregion
    }
}