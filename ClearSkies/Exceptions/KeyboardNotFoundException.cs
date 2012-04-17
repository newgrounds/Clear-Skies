using System;

namespace ClearSkies.Exceptions
{
    /// <summary>
    /// Exception for an unaquirable Keyboard Device.
    /// </summary>
    class KeyboardNotFoundException : Exception
    {
        #region Initializer Methods

        /// <summary>
        /// Exception to be thrown if DirectInput is unable to aquire keyboard.
        /// </summary>
        /// <param name="message">Message to display on failure</param>
        public KeyboardNotFoundException(string message) : base(message) { }

        #endregion
    }
}