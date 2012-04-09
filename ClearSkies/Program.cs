using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace ClearSkies
{
    /// <summary>
    /// Starts the program and opens the default game form.
    /// </summary>
    static class Program
    {
        #region Static Methods

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Game());
        }

        #endregion
    }
}
