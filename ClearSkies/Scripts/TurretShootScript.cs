using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ClearSkies.Prefabs;
using Microsoft.DirectX;
using DI = Microsoft.DirectX.DirectInput;
using ClearSkies.Prefabs.Turrets;
using ClearSkies.Managers;
using ClearSkies.Prefabs.Bullets;

namespace ClearSkies.Scripts
{
    /// <summary>
    /// A Script for shooting BasicBullet projectiles
    /// </summary>
    class TurretShootScript : Script
    {
        #region Fields

        private TurretBarrel shooter;

        private float timeSinceLastShot;
        private bool shooting;

        private float pushTime;
        private float pullTime;

        #endregion

        #region Initializer Methods

        /// <summary>
        /// Creates a ShootScript that will fire Bullets from the given 
        /// Prefabs location and in the given Prefabs rotation. Firing is
        /// controlled by the given keyboard Device.
        /// </summary>
        /// <param name="shooter">Prefab to shoot the Bullets from</param>
        /// <param name="keyboard">Keyboard Device that determines when we shoot</param>
        public TurretShootScript(TurretBarrel shooter)
        {
            this.shooter = shooter;
            
            this.shooting = false;
            this.timeSinceLastShot = shooter.ShootDelay;
            this.pushTime = shooter.ShootDelay * shooter.PullSpeed / (shooter.PullSpeed + shooter.PushSpeed);
            this.pullTime = shooter.ShootDelay * shooter.PushSpeed / (shooter.PullSpeed + shooter.PushSpeed);        
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Shoots a BasicBullet if the Space button on the keyboard Device is
        /// pressed. The process of shooting includes a recoil animation to be
        /// played on the Prefab given in the constructor.
        /// </summary>
        /// <param name="deltaTime">Time in seconds since last update</param>
        public void run(float deltaTime)
        {
            DI.KeyboardState keys = shooter.Keyboard.GetCurrentKeyboardState();

            if (keys[DI.Key.Space] && timeSinceLastShot >= shooter.ShootDelay)
            {
                shooting = true;
                this.timeSinceLastShot = 0.0f;
                BulletManager.spawn(BulletType.Basic, TurretManager.ManagedTurrets[0], shooter.Location, shooter.Rotation, shooter.Scale);
            }
            else
            {
                timeSinceLastShot += deltaTime;
            }

            if (shooting)
            {
                if (timeSinceLastShot <= pullTime)
                {
                    Vector3 recoilLocation = shooter.DrawLocation;

                    recoilLocation.Z -= deltaTime * shooter.PullSpeed * (float)(Math.Sin(shooter.Rotation.Y) * Math.Cos(shooter.Rotation.X));
                    recoilLocation.X -= deltaTime * shooter.PullSpeed * (float)(Math.Sin(shooter.Rotation.Y) * Math.Sin(shooter.Rotation.X));
                    recoilLocation.Y -= deltaTime * shooter.PullSpeed * (float)Math.Cos(shooter.Rotation.Y);

                    shooter.DrawLocation = recoilLocation;
                }
                else
                {
                    Vector3 recoilLocation = shooter.DrawLocation;

                    recoilLocation.Z += deltaTime * shooter.PushSpeed * (float)(Math.Sin(shooter.Rotation.Y) * Math.Cos(shooter.Rotation.X));
                    recoilLocation.X += deltaTime * shooter.PushSpeed * (float)(Math.Sin(shooter.Rotation.Y) * Math.Sin(shooter.Rotation.X));
                    recoilLocation.Y += deltaTime * shooter.PushSpeed * (float)Math.Cos(shooter.Rotation.Y);

                    shooting = timeSinceLastShot <= pullTime + pushTime;

                    if (!shooting)
                    {
                        recoilLocation = shooter.Location;
                    }

                    shooter.DrawLocation = recoilLocation;
                }
            }
        }

        #endregion
    }
}
