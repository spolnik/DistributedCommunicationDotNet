﻿using System;

namespace NProg.Distributed.Core.Service.Messaging
{
    public abstract class MessageClientBase : IDisposable
    {
         /// <summary>
        /// The request sender
        /// </summary>
         protected readonly IMessageSender messageSender;

        /// <summary>
        /// Initializes a new instance of the <see cref="T:System.Object"/> class.
        /// </summary>
        protected MessageClientBase(IMessageSender messageSender)
        {
            this.messageSender = messageSender;
        }

        #region Implementation of IDisposable

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Releases unmanaged and - optionally - managed resources.
        /// </summary>
        /// <param name="disposing"><c>true</c> to release both managed and unmanaged resources; <c>false</c> to release only unmanaged resources.</param>
        private void Dispose(bool disposing)
        {
            if (disposing && messageSender != null)
            {
                messageSender.Dispose();
            }
        }

        #endregion
    }
}