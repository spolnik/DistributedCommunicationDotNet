using System;
using NProg.Distributed.Messaging.Spec;

namespace NProg.Distributed.ZeroMQ.Command
{
    public class RemoveOrderCommand : ICommand
    {
        public Guid OrderId { get; set; }

        internal const string Name = "remove-order";

        public string GetName()
        {
            return Name;
        }
    }
}