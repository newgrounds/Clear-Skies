using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ClearSkies.Exceptions
{
    /// <summary>
    /// Exception for if ContentLoader was not initialized before use.
    /// </summary>
    class ContentLoaderUninitializedException : Exception
    {
        #region Initializer Methods

        /// <summary>
        /// Exception to be thrown if ContentLoader is uninitialized before first use.
        /// </summary>
        /// <param name="message">Message to display on failure</param>
        public ContentLoaderUninitializedException(string message) : base(message) { }

        #endregion
    }
}
