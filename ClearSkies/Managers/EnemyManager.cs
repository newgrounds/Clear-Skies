using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ClearSkies.Prefabs.Enemies;
using Microsoft.DirectX.Direct3D;
using Microsoft.DirectX;

namespace ClearSkies.Managers
{
    /// <summary>
    /// Manages the drawing, updating, and creation of all Enemy objects for use within the game.
    /// </summary>
    class EnemyManager : Manager
    {
        #region Fields

        private static List<Enemy> managedEnemies;

        #endregion

        #region Initializer Methods

        /// <summary>
        /// Initailizes all data for use in the EnemyManager.
        /// </summary>
        static EnemyManager() 
        {
            managedEnemies = new List<Enemy>();
        }

        #endregion

        #region Getter Methods

        /// <summary>
        /// All Enemy Prefabs currently being managed.
        /// </summary>
        public static List<Enemy> ManagedEnemies
        {
            get { return managedEnemies; }
        }

        #endregion

        #region Static Methods

        /// <summary>
        /// Creates a single Enemy unit of the give type at the give location
        /// and facing the given rotation.
        /// </summary>
        /// <param name="enemyType">Type of Enemy to spawn</param>
        /// <param name="location">Locaiton to spawn Enemy at</param>
        /// <param name="rotation">Rotation for Enemy to face</param>
        /// <returns>A reference to the spawned Enemy</returns>
        public static Enemy spawnEnemy(EnemyType enemyType, Vector3 location, Vector3 rotation)
        {
            Enemy spawnedEnemy = null;

            switch (enemyType)
            {
                case EnemyType.BasicPlane:

                    break;
                case EnemyType.BasicTank:
                    spawnedEnemy = new Tank(location, rotation);
                    break;
            }

            managedEnemies.Add(spawnedEnemy);
            return spawnedEnemy;
        }
        
        #endregion

        #region Public Methods

        /// <summary>
        /// Updates all Enemy Prefabs being managed within this manager. Also 
        /// removes any Enemy Prefabs that no longer meet the requirements of 
        /// being alive.
        /// </summary>
        /// <param name="deltaTime">Time in seconds since last update</param>
        public void update(float deltaTime)
        {
            for (int i = 0; i < managedEnemies.Count; i++)
            {
                if (managedEnemies[i].Alive)
                {
                    managedEnemies[i].update(deltaTime);
                }
                else
                {
                    managedEnemies.RemoveAt(i);
                }
            }
        }

        /// <summary>
        /// Draws all Enemy Prefabs managed in this Manager.
        /// </summary>
        /// <param name="device">Device to draw Enemy Prefabs to</param>
        public void draw(Device device)
        {
            foreach (Enemy managedEnemy in managedEnemies)
            {
                managedEnemy.draw(device);
            }
        }

        #endregion
    }
}
