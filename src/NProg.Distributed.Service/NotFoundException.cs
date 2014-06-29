using System;

namespace NProg.Distributed.Core.Service
{
    [Serializable]
    public sealed class NotFoundException : ApplicationException
    {
        public NotFoundException(string message)
            : base(message)
        {
        }

        public NotFoundException(string message, Exception exception)
            : base(message, exception)
        {
        }
    }
}