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

        private Tank shooter;
        private TankBarrel barrel;

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
        public TankShootScript(Tank shooter, TankBarrel barrel)
        {
            this.shooter = shooter;
            this.barrel = barrel;
            
            this.shooting = false;
            this.timeSinceLastShot = barrel.ShootDelay;
            this.pushTime = barrel.ShootDelay * barrel.PullSpeed / (barrel.PullSpeed + barrel.PushSpeed);
            this.pullTime = barrel.ShootDelay * barrel.PushSpeed / (barrel.PullSpeed + barrel.PushSpeed);        
        }


        public void run(float deltaTime)
        {
            Vector3 targetVector = TurretManager.ManagedTurrets[0].Location - barrel.Location;

            if (barrel.ShootDistance > targetVector.Length() && timeSinceLastShot >= barrel.ShootDelay)
            {
                shooting = true;
                this.timeSinceLastShot = 0.0f;
                BulletManager.spawn(BulletType.Basic, shooter, barrel.Location, barrel.Rotation, barrel.Scale);
            }
            else
            {
                timeSinceLastShot += deltaTime;
            }

            if (shooting)
            {
                if (timeSinceLastShot <= pullTime)
                {
                    Vector3 recoilLocation = barrel.DrawLocation;

                    recoilLocation.Z -= deltaTime * barrel.PullSpeed * (float)(Math.Sin(barrel.Rotation.Y) * Math.Cos(barrel.Rotation.X));
                    recoilLocation.X -= deltaTime * barrel.PullSpeed * (float)(Math.Sin(barrel.Rotation.Y) * Math.Sin(barrel.Rotation.X));
                    recoilLocation.Y -= deltaTime * barrel.PullSpeed * (float)Math.Cos(barrel.Rotation.Y);

                    barrel.DrawLocation = recoilLocation;
                }
                else
                {
                    Vector3 recoilLocation = barrel.DrawLocation;

                    recoilLocation.Z += deltaTime * barrel.PushSpeed * (float)(Math.Sin(barrel.Rotation.Y) * Math.Cos(barrel.Rotation.X));
                    recoilLocation.X += deltaTime * barrel.PushSpeed * (float)(Math.Sin(barrel.Rotation.Y) * Math.Sin(barrel.Rotation.X));
                    recoilLocation.Y += deltaTime * barrel.PushSpeed * (float)Math.Cos(barrel.Rotation.Y);

                    shooting = timeSinceLastShot <= pullTime + pushTime;

                    if (!shooting)
                    {
                        recoilLocation = barrel.Location;
                    }

                    barrel.DrawLocation = recoilLocation;
                }
            }
        }
    }
}
