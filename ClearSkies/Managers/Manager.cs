using Microsoft.DirectX.Direct3D;

namespace ClearSkies.Managers
{
    /// <summary>
    /// Manages the drawing, updating, and creation of all Prefabs for use 
    /// within the game.
    /// </summary>
    interface Manager
    {
        #region Public Methods

        /// <summary>
        /// Updates all Prefabs being managed within this manager. Also removes
        /// any Prefabs that no longer meet the requirements of being alive.
        /// </summary>
        /// <param name="deltaTime">Time since last update</param>
        void update(float deltaTime);

        /// <summary>
        /// Draws all Prefabs in the managed in this Manager.
        /// </summary>
        /// <param name="device">Device to draw Prefabs to</param>
        void draw(Device device);

        #endregion
    }
}
