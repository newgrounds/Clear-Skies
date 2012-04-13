using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.DirectX;
using ClearSkies.Prefabs;
using ClearSkies.Prefabs.Enemies;

namespace ClearSkies.Scripts
{
    /// <summary>
    /// A script that will move a Tank Prefab forward
    /// </summary>
    class TankMovementScript : Script
    {
        #region Fields

        private float movementSpeed;
        private Tank tank;

        #endregion

        #region Initializer Methods

        /// <summary>
        /// Script allows a Tank to move at a certain speed.
        /// </summary>
        /// <param name="turret">Tank to translate.</param>
        /// <param name="movementSpeed">Speed to move at.</param>
        public TankMovementScript(Tank tank, float movementSpeed)
        {
            this.tank = tank;
            this.movementSpeed = movementSpeed;
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Moves the prefab.
        /// </summary>
        /// <param name="deltaTime">Time in seconds since last Update</param>
        public void run(float deltaTime)
        {
            Vector3 location = tank.Location;

            location += new Vector3(0, 0, movementSpeed*deltaTime);

            tank.Location = location;
        }

        #endregion
    }
}
