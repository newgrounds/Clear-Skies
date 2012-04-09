using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ClearSkies.Exceptions
{
    /// <summary>
    /// Exception for an unaquirable Mouse Device.
    /// </summary>
    class MouseNotFoundException : Exception
    {
        #region Initializer Methods

        /// <summary>
        /// Exception to be thrown if DirectInput is unable to aquire a mouse.
        /// </summary>
        /// <param name="message">Message to display on failure</param>
        public MouseNotFoundException(string message) : base(message) { }

        #endregion
    }
}
