using System;

namespace ClearSkies.Exceptions
{
    /// <summary>
    /// Exception for if a class was not initialized before use.
    /// </summary>
    class UninitializedException : Exception
    {
        #region Initializer Methods

        /// <summary>
        /// Exception to be thrown if a class is uninitialized before first use.
        /// </summary>
        /// <param name="message">Message to display on failure</param>
        public UninitializedException(string message) : base(message) { }

        #endregion
    }
}