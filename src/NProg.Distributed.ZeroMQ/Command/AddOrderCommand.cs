using NProg.Distributed.Domain;
using NProg.Distributed.Messaging.Spec;

namespace NProg.Distributed.ZeroMQ.Command
{
    public class AddOrderCommand : ICommand
    {
        public Order Order { get; set; }

        internal const string Name = "add-order";

        public string GetName()
        {
            return Name;
        }
    }
}