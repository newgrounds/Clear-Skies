using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.DirectX;
using ClearSkies.Scripts;
using ClearSkies.Content;

namespace ClearSkies.Prefabs.Enemies
{
    /// <summary>
    /// An enemy tank
    /// </summary>
    class Tank : Enemy
    {
        #region Fields

        private static float TANK_SPEED = 1.0f;
        private static float COLLIDER_SIZE = 1.0f;

        #endregion

        #region Initializer Methods

        /// <summary>
        /// Creates a simple Tank at the given location facing the given
        /// rotation.
        /// </summary>
        /// <param name="location">Location of the Tank in the gameworld</param>
        /// <param name="rotation">Rotation the Tank is facing</param>
        public Tank(Vector3 location, Vector3 rotation)
            : base(location, rotation, COLLIDER_SIZE)
        {
            this.models.Add(ContentLoader.TankModel);

            this.scripts.Add(new TankMovementScript(this, TANK_SPEED));
        }

        #endregion
    }
}
