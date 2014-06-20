using System;

namespace NProg.Distributed.Service
{
    /// <summary>
    /// Interface IRunnable
    /// </summary>
    public interface IRunnable : IDisposable
    {
        /// <summary>
        /// Runs this instance.
        /// </summary>
        void Run();
    }
}