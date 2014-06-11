using System;
using NProg.Distributed.Messaging.Spec;

namespace NProg.Distributed.ZeroMQ.Command
{
    public class GetOrderCommand : ICommand
    {
        public Guid OrderId { get; set; }

        internal const string Name = "get-order";

        public string GetName()
        {
            return Name;
        }
    }
}