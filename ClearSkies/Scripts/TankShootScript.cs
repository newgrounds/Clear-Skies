using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ClearSkies.Prefabs.Enemies.Tanks;
using Microsoft.DirectX;
using ClearSkies.Managers;
using ClearSkies.Prefabs.Bullets;

namespace ClearSkies.Scripts
{
    class TankShootScript : Script
    {
        #region Fields

        private TankBarrel shooter;

        private float timeSinceLastShot;
        private bool shooting;

        private float pushTime;
        private float pullTime;


        #endregion

        /// <summary>
        /// Creates a ShootScript that will fire Bullets from the given 
        /// Prefabs location and in the given Prefabs rotation. Firing is
        /// controlled by the given keyboard Device.
        /// </summary>
        /// <param name="shooter">Prefab to shoot the Bullets from</param>
        /// <param name="keyboard">Keyboard Device that determines when we shoot</param>
        public TankShootScript(TankBarrel shooter)
        {
            this.shooter = shooter;
            
            this.shooting = false;
            this.timeSinceLastShot = shooter.ShootDelay;
            this.pushTime = shooter.ShootDelay * shooter.PullSpeed / (shooter.PullSpeed + shooter.PushSpeed);
            this.pullTime = shooter.ShootDelay * shooter.PushSpeed / (shooter.PullSpeed + shooter.PushSpeed);        
        }


        public void run(float deltaTime)
        {
            Vector3 targetVector = TurretManager.ManagedTurrets[0].Location - shooter.Location;

            if (shooter.ShootDistance > targetVector.Length() && timeSinceLastShot >= shooter.ShootDelay)
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
    }
}
