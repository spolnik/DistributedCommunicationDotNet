using NProg.Distributed.CarRental.Data.Repository;
using NProg.Distributed.CarRental.Service.Commands;
using NProg.Distributed.Core.Service.Messaging;

namespace NProg.Distributed.CarRental.Service.Handlers
{
    public class DeleteCarHandler 
        : MessageHandlerBase<DeleteCarCommand, ICarRepository>
    {

        /// <summary>
        /// Initializes a new instance of the MessageHandlerBase class.
        /// </summary>
        public DeleteCarHandler(ICarRepository repository) : base(repository)
        {}

        #region Overrides of MessageHandlerBase<DeleteCarCommand,ICarRepository>

        protected override IMessage Process(DeleteCarCommand command)
        {
            return new StatusResponse {Status = repository.Remove(command.CarId)};
        }

        #endregion
    }
}