using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ClearSkies.Scripts
{
    /// <summary>
    /// A Script interface, contains only one method to allow scripts to
    /// update whatever prefabs they are attached to.
    /// </summary>
    interface Script
    {
        #region Public Methods

        /// <summary>
        /// Runs the current script.
        /// </summary>
        /// <param name="deltaTime"></param>
        void run(float deltaTime);

        #endregion
    }
}
