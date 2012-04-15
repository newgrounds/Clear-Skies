using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ClearSkies.Prefabs.Bullets;
using Microsoft.DirectX;
using ClearSkies.Prefabs.Enemies;
using Microsoft.DirectX.Direct3D;
using ClearSkies.Content;
using ClearSkies.Prefabs.Turrets;

namespace ClearSkies.Managers
{
    /// <summary>
    /// Manages the drawing, updating, and creation of all Bullet objects for use within the game.
    /// </summary>
    class BulletManager : Manager
    {
        #region Fields

        private static List<Bullet> managedBullets;

        private static Turret turret;

        #endregion

        #region Initializer Methods

        /// <summary>
        /// Initailizes all data for use in the BulletManager.
        /// </summary>
        public BulletManager(Turret t)
        {
            managedBullets = new List<Bullet>();
            turret = t;
        }

        #endregion

        #region Getter Methods

        /// <summary>
        /// All Bullet Prefabs currently being Managed
        /// </summary>
        public static List<Bullet> ManagedBullets
        {
            get { return managedBullets; }
        }

        #endregion

        #region Static Methods

        /// <summary>
        /// Spawns a single Bullet of the give type at the desired location and rotation.
        /// </summary>
        /// <param name="bulletType">Type of bullet to spawn</param>
        /// <param name="location">Location to spawn bullet at</param>
        /// <param name="rotation">Rotation bullet should be facing</param>
        /// <returns>A refernce to the spawned Bullet</returns>
        public static Bullet spawn(BulletType bulletType, Vector3 location, Vector3 rotation, Vector3 scale)
        {
            Bullet spawnedBullet = null;

            switch (bulletType)
            {
                case BulletType.Basic:
                    spawnedBullet= new BasicBullet(location, rotation, scale);
                    break;
                case BulletType.Tracer:
                    spawnedBullet = new TracerBullet(location, rotation, scale);
                    break;
                case BulletType.Tank:
                    spawnedBullet = new TankBullet(location, rotation, scale, turret);
                    break;
            }

            if (spawnedBullet != null)
            {
                managedBullets.Add(spawnedBullet);
            }

            return spawnedBullet;
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Updates all Bullet Prefabs being managed within this manager. Also 
        /// removes any Bullet Prefabs that no longer meet the requirements of 
        /// being alive.
        /// </summary>
        /// <param name="deltaTime">Time since last update</param>
        public void update(float deltaTime)
        {
            for (int i = 0; i < managedBullets.Count; i++)
            {
                if (managedBullets[i].Alive)
                {
                    managedBullets[i].update(deltaTime);
                }
                else
                {
                    managedBullets.RemoveAt(i);
                }
            }
        }

        /// <summary>
        /// Draws all Bullet Prefabs managed in this Manager.
        /// </summary>
        /// <param name="device">Device to draw Prefabs to</param>
        public void draw(Device device)
        {
            foreach (Bullet managedBullet in managedBullets)
            {
                managedBullet.draw(device);
            }
        }

        #endregion
    }
}
