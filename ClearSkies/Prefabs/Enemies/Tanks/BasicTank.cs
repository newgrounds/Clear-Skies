using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.DirectX;
using ClearSkies.Scripts;
using ClearSkies.Content;
using ClearSkies.Prefabs.Bullets;
using ClearSkies.Managers;
using ParticleEngine;

namespace ClearSkies.Prefabs.Enemies.Tanks
{
    /// <summary>
    /// An enemy tank
    /// </summary>
    class BasicTank : Tank
    {
        #region Initializer Methods

        /// <summary>
        /// Creates a simple Tank at the given location facing the given
        /// rotation.
        /// </summary>
        /// <param name="location">Location of the Tank in the gameworld</param>
        /// <param name="rotation">Rotation the Tank is facing</param>
        public BasicTank(Vector3 location, Vector3 rotation, Vector3 scale, float driveSpeed, float turnSpeed)
            : base(ContentLoader.TankModel, location, rotation, scale, Settings.TANK_COLLIDER_SIZE)
        {
            this.scripts.Add(new TankMovementScript(this, driveSpeed, turnSpeed));
        }

        #endregion
    }
}
