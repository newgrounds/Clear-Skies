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
            Game app = new Game();
            DateTime lastUpdate = DateTime.Now;
            app.Show();
            while (app.Created)
            {
                TimeSpan deltaTime = DateTime.Now.Subtract(lastUpdate);

                app.update(deltaTime.Milliseconds / 1000f);
                lastUpdate = DateTime.Now;

                app.draw();
                Application.DoEvents();
            }
            app.DisposeGraphics();
            Application.Exit();
        }

        #endregion
    }
}
