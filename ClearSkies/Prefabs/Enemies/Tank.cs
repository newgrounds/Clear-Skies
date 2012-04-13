using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.DirectX;
using ClearSkies.Scripts;
using ClearSkies.Content;
using ClearSkies.Prefabs.Bullets;

namespace ClearSkies.Prefabs.Enemies
{
    /// <summary>
    /// An enemy tank
    /// </summary>
    class Tank : Enemy
    {
        #region Fields

        private static float TANK_SPEED = -1.0f;
        // we probably want this set to the size of the tank, which is currently 0.5f
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

        #region Public Methods

        // this detects and handles collisions for the tank
        public override void detectCollision(Prefab collider)
        {
            base.detectCollision(collider);

            if (collider is Bullet)
            {
                this.scripts.Clear();
                // TODO: Add death animation script
            }
        }

        #endregion
    }
}
