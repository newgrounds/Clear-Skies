using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.DirectX;
using ClearSkies.Prefabs;
using ClearSkies.Prefabs.Enemies;
using ClearSkies.Prefabs.Enemies.Tanks;

namespace ClearSkies.Scripts
{
    /// <summary>
    /// A script that will move a Tank Prefab forward
    /// </summary>
    class TankMovementScript : Script
    {
        #region Fields

        private float driveSpeed;
        private float turnSpeed;
        private BasicTank tank;

        #endregion

        #region Initializer Methods

        /// <summary>
        /// Script allows a Tank to move at a certain speed.
        /// </summary>
        /// <param name="turret">Tank to translate.</param>
        /// <param name="movementSpeed">Speed to move at.</param>
        public TankMovementScript(BasicTank tank, float driveSpeed, float turnSpeed)
        {
            this.tank = tank;
            this.driveSpeed = driveSpeed;
            this.turnSpeed = turnSpeed;
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Moves the prefab tank toward the turret.
        /// </summary>
        /// <param name="deltaTime">Time in seconds since last Update</param>
        public void run(float deltaTime)
        {
            Vector3 location = tank.Location;

            location += new Vector3(
                (float)(Math.Cos(tank.Rotation.X) * driveSpeed * deltaTime), 
                0f,
                (float)(Math.Sin(tank.Rotation.X) * driveSpeed * deltaTime));

            tank.Location = location;
        }

        #endregion
    }
}
