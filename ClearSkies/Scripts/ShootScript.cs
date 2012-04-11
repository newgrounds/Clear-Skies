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

        private const float PULL_SPEED = 2f;
        private const float PUSH_SPEED = 1f;
        private const float SHOOT_DELAY = 0.2f;

        private const float PULL_TIME = SHOOT_DELAY * PUSH_SPEED / (PULL_SPEED + PUSH_SPEED);
        private const float PUSH_TIME = SHOOT_DELAY * PULL_SPEED / (PULL_SPEED + PUSH_SPEED);

        private Prefab shooter;
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
        public ShootScript(Prefab shooter, DI.Device keyboard)
        {
            this.shooter = shooter;
            this.keyboard = keyboard;
            
            this.shooting = false;
            this.timeSinceLastShot = SHOOT_DELAY;
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

            if (keys[DI.Key.Space] && timeSinceLastShot >= SHOOT_DELAY)
            {
                shooting = true;
                this.timeSinceLastShot = 0.0f;
                BulletManager.spawn(BulletType.Basic, shooter.Location, shooter.Rotation);
            }
            else
            {
                timeSinceLastShot += deltaTime;
            }

            if (shooting)
            {
                if (timeSinceLastShot <= PULL_TIME)
                {
                    Vector3 recoilLocation = shooter.Location;

                    recoilLocation.Z -= deltaTime * PULL_SPEED * (float)(Math.Sin(shooter.Rotation.Y) * Math.Cos(shooter.Rotation.X));
                    recoilLocation.X -= deltaTime * PULL_SPEED * (float)(Math.Sin(shooter.Rotation.Y) * Math.Sin(shooter.Rotation.X));
                    recoilLocation.Y -= deltaTime * PULL_SPEED * (float)Math.Cos(shooter.Rotation.Y);

                    shooter.Location = recoilLocation;

                }
                else
                {
                    Vector3 recoilLocation = shooter.Location;

                    recoilLocation.Z += deltaTime * PUSH_SPEED * (float)(Math.Sin(shooter.Rotation.Y) * Math.Cos(shooter.Rotation.X));
                    recoilLocation.X += deltaTime * PUSH_SPEED * (float)(Math.Sin(shooter.Rotation.Y) * Math.Sin(shooter.Rotation.X));
                    recoilLocation.Y += deltaTime * PUSH_SPEED * (float)Math.Cos(shooter.Rotation.Y);

                    shooter.Location = recoilLocation;
                    shooting = timeSinceLastShot <= PULL_TIME + PUSH_TIME;
                }
            }
        }

        #endregion
    }
}
