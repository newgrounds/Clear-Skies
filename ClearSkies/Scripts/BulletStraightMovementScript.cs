using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ClearSkies.Prefabs;
using Microsoft.DirectX;
using ClearSkies.Prefabs.Bullets;

namespace ClearSkies.Scripts
{
    /// <summary>
    /// A Script used to move a single Bullet in its own forward direction in 
    /// the gameworld.
    /// </summary>
    class BulletStraightMovementScript : Script
    {
        #region Fields

        private Bullet bullet;

        #endregion

        #region Initializer Methods

        /// <summary>
        /// Creates the simple bullet movement script to be used on a 
        /// single Bullet.
        /// </summary>
        /// <param name="bullet">Bullet to move</param>
        public BulletStraightMovementScript(Bullet bullet)
        {
            this.bullet = bullet;
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Moves the Bullet straight forward.
        /// </summary>
        /// <param name="deltaTime">Time in seconds since last update</param>
        public void run(float deltaTime)
        {
            Vector3 bulletLocation = bullet.Location;
            bulletLocation.Z += bullet.Speed * (float)(Math.Sin(bullet.Rotation.Y) * Math.Cos(bullet.Rotation.X));
            bulletLocation.X += bullet.Speed * (float)(Math.Sin(bullet.Rotation.Y) * Math.Sin(bullet.Rotation.X));
            bulletLocation.Y += bullet.Speed * (float)Math.Cos(bullet.Rotation.Y);
            bullet.Location = bulletLocation;
        }

        #endregion
    }
}
