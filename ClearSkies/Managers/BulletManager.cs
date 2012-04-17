using System.Collections.Generic;
using ClearSkies.Exceptions;
using ClearSkies.Prefabs;
using ClearSkies.Prefabs.Bullets;
using ClearSkies.Properties;
using Microsoft.DirectX.Direct3D;

namespace ClearSkies.Managers
{
    /// <summary>
    /// Manages the drawing, updating, and creation of all Bullet objects for 
    /// use within the game.
    /// </summary>
    class BulletManager : Manager
    {
        #region Fields

        private static bool initialized;
        private static List<Bullet> managedBullets;

        #endregion

        #region Initializer Methods

        /// <summary>
        /// Initailizes all data for use in the BulletManager.
        /// </summary>
        public BulletManager() 
        {
            initialized = true;
            managedBullets = new List<Bullet>();
        }

        #endregion

        #region Getter Methods

        /// <summary>
        /// All Bullet Prefabs currently being managed.
        /// </summary>
        public static List<Bullet> ManagedBullets
        {
            get { return managedBullets; }
        }

        #endregion

        #region Public Static Methods

        /// <summary>
        /// Spawns a single Bullet of the give type at the owners location and rotation.
        /// </summary>
        /// <param name="bulletType">Type of bullet to spawn</param>
        /// <param name="owner">The Prefab that owns the Bullet</param>
        /// <returns>A refernce to the spawned Bullet</returns>
        public static Bullet spawn(BulletType bulletType, Prefab owner)
        {
            checkIfInitialized();
            Bullet spawnedBullet = null;

            switch (bulletType)
            {
                case BulletType.Basic:
                    spawnedBullet= new BasicBullet(owner);
                    break;
                case BulletType.Bomb:
                    spawnedBullet = new BombBullet(owner);
                    break;
            }

            if (spawnedBullet != null)
            {
                managedBullets.Add(spawnedBullet);
            }

            return spawnedBullet;
        }

        #endregion

        #region Private Static Methods

        /// <summary>
        /// Checks if the BulletManager was initialized. This should be called 
        /// at the beginning of every public static method.
        /// </summary>
        private static void checkIfInitialized()
        {
            if (!initialized)
            {
                throw new UninitializedException(Resources.Bullet_Manager_Uninitialized_Exception);
            }
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