using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ClearSkies.Prefabs.Turrets;
using Microsoft.DirectX;
using ClearSkies.Prefabs.Bullets;

namespace ClearSkies.Scripts
{
    /// <summary>
    /// A Script used to move a single Bullet in the direction
    /// of a given location.
    /// </summary>
    class BulletLocationMovementScript : Script
    {
        #region Fields

        private Bullet bullet;

        private Turret turret;

        #endregion

        #region Initializer Methods

        /// <summary>
        /// Creates the simple bullet movement script to be used on a 
        /// single Bullet.
        /// </summary>
        /// <param name="bullet">Bullet to move</param>
        /// <param name="headTo">Location to head towards</param>
        public BulletLocationMovementScript(Bullet bullet, Turret t)
        {
            this.bullet = bullet;
            this.turret = t;
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Moves the Bullet towards the Turret.
        /// </summary>
        /// <param name="deltaTime">Time in seconds since last update</param>
        public void run(float deltaTime)
        {
            Vector3 bulletLocation = bullet.Location;
            // make this send it towards the given location
            //bulletLocation.Z += bullet.Speed * (float)(Math.Sin(bullet.Rotation.Y) * Math.Cos(bullet.Rotation.X));
            //bulletLocation.X += bullet.Speed * (float)(Math.Sin(bullet.Rotation.Y) * Math.Sin(bullet.Rotation.X));
            //bulletLocation.Y += bullet.Speed * (float)Math.Cos(turret.Rotation.Y - bullet.Rotation.Y);
            bulletLocation += bullet.Speed * deltaTime * (turret.Location - bullet.Location);
            bullet.Location = bulletLocation;
        }

        #endregion
    }
}
