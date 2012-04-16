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
    class ShootScript : Script
    {
        #region Fields

        private TurretBarrel shooter;
        private DI.Device keyboard;

        private float timeSinceLastShot;
        private bool shooting;

        #endregion

        #region Initializer Methods

        /// <summary>
        /// Creates a ShootScript that will fire Bullets from the given 
        /// Prefabs location and in the given Prefabs rotation. Firing is
        /// controlled by the given keyboard Device.
        /// </summary>
        /// <param name="shooter">Prefab to shoot the Bullets from</param>
        /// <param name="keyboard">Keyboard Device that determines when we shoot</param>
        public ShootScript(TurretBarrel shooter, DI.Device keyboard)
        {
            this.shooter = shooter;
            this.keyboard = keyboard;
            
            this.shooting = false;
            this.timeSinceLastShot = Settings.SHOOT_SHOOT_DELAY;
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
            DI.KeyboardState keys = keyboard.GetCurrentKeyboardState();

            if (keys[DI.Key.Space] && timeSinceLastShot >= Settings.SHOOT_SHOOT_DELAY)
            {
                shooting = true;
                this.timeSinceLastShot = 0.0f;
                BulletManager.spawn(BulletType.Basic, shooter);
            }
            else
            {
                timeSinceLastShot += deltaTime;
            }

            if (shooting)
            {
                if (timeSinceLastShot <= Settings.SHOOT_PULL_TIME)
                {
                    Vector3 recoilLocation = shooter.DrawLocation;

                    recoilLocation.Z -= deltaTime * Settings.SHOOT_PULL_SPEED * (float)(Math.Sin(shooter.Rotation.Y) * Math.Cos(shooter.Rotation.X));
                    recoilLocation.X -= deltaTime * Settings.SHOOT_PULL_SPEED * (float)(Math.Sin(shooter.Rotation.Y) * Math.Sin(shooter.Rotation.X));
                    recoilLocation.Y -= deltaTime * Settings.SHOOT_PULL_SPEED * (float)Math.Cos(shooter.Rotation.Y);

                    shooter.DrawLocation = recoilLocation;
                }
                else
                {
                    Vector3 recoilLocation = shooter.DrawLocation;

                    recoilLocation.Z += deltaTime * Settings.SHOOT_PUSH_SPEED * (float)(Math.Sin(shooter.Rotation.Y) * Math.Cos(shooter.Rotation.X));
                    recoilLocation.X += deltaTime * Settings.SHOOT_PUSH_SPEED * (float)(Math.Sin(shooter.Rotation.Y) * Math.Sin(shooter.Rotation.X));
                    recoilLocation.Y += deltaTime * Settings.SHOOT_PUSH_SPEED * (float)Math.Cos(shooter.Rotation.Y);

                    shooting = timeSinceLastShot <= Settings.SHOOT_PULL_TIME + Settings.SHOOT_PUSH_TIME;

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
