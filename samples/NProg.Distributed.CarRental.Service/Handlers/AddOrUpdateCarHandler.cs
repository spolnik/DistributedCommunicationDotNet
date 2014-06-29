using NProg.Distributed.CarRental.Data.Repository;
using NProg.Distributed.CarRental.Service.Commands;
using NProg.Distributed.CarRental.Service.Responses;
using NProg.Distributed.Core.Service.Messaging;

namespace NProg.Distributed.CarRental.Service.Handlers
{
    public class AddOrUpdateCarHandler 
        : MessageHandlerBase<AddOrUpdateCarCommand, ICarRepository>
    {

        /// <summary>
        /// Initializes a new instance of the MessageHandlerBase class.
        /// </summary>
        public AddOrUpdateCarHandler(ICarRepository repository) : base(repository)
        {}

        #region Overrides of MessageHandlerBase<AddOrUpdateCarCommand,ICarRepository>

        protected override IMessage Process(AddOrUpdateCarCommand command)
        {
            var updatedEntity = command.Car.CarId == 0
                ? repository.Add(command.Car)
                : repository.Update(command.Car);

            return new GetCarResponse {Car = updatedEntity};
        }

        #endregion
    }
}