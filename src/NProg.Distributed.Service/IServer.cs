﻿using System;

namespace NProg.Distributed.Core.Service
{
    /// <summary>
    /// Interface IServer
    /// </summary>
    public interface IServer : IDisposable
    {
        /// <summary>
        /// Runs this instance.
        /// </summary>
        void Run();
    }
}