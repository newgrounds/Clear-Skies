using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ClearSkies.Prefabs;
using Microsoft.DirectX;
using DI = Microsoft.DirectX.DirectInput;
using ClearSkies.Prefabs.Enemies;
using ClearSkies.Managers;
using ClearSkies.Prefabs.Bullets;

namespace ClearSkies.Scripts
{
    /// <summary>
    /// A Script for shooting BasicBullet projectiles
    /// </summary>
    class TankShootScript : Script
    {
        #region Fields

        private const float PULL_SPEED = 2f;
        private const float PUSH_SPEED = 1f;
        private const float SHOOT_DELAY = 1f;

        private const float PULL_TIME = SHOOT_DELAY * PUSH_SPEED / (PULL_SPEED + PUSH_SPEED);
        private const float PUSH_TIME = SHOOT_DELAY * PULL_SPEED / (PULL_SPEED + PUSH_SPEED);

        private Tank shooter;

        private float timeSinceLastShot;

        private Vector3 turretLoc;

        #endregion

        #region Initializer Methods

        /// <summary>
        /// Creates a ShootScript that will fire Bullets from the given 
        /// Prefabs location and in the given Prefabs rotation. Firing is
        /// controlled by the given keyboard Device.
        /// </summary>
        /// <param name="shooter">Prefab to shoot the Bullets from</param>
        public TankShootScript(Tank shooter, Vector3 turretLocation)
        {
            this.shooter = shooter;

            this.turretLoc = turretLocation;

            this.timeSinceLastShot = SHOOT_DELAY;
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Shoots a TankBullet every few seconds once the parent Tank
        /// is in a certain range.
        /// </summary>
        /// <param name="deltaTime">Time in seconds since last update</param>
        public void run(float deltaTime)
        {
            // if the Tank is in range of the turret
            if (15f > (turretLoc - shooter.Location).Length() 
                && this.timeSinceLastShot >= SHOOT_DELAY)
            {
                this.timeSinceLastShot = 0.0f;
                BulletManager.spawn(BulletType.Tank, shooter.Location, shooter.Rotation + new Vector3(0,-90,0), shooter.Scale);
            }
            else
            {
                timeSinceLastShot += deltaTime;
            }
        }

        #endregion
    }
}
