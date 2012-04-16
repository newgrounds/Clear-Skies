using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ClearSkies.Prefabs.Enemies;
using ClearSkies.Prefabs.Turrets;
using Microsoft.DirectX.Direct3D;
using Microsoft.DirectX;
using ClearSkies;

namespace ClearSkies.Managers
{
    /// <summary>
    /// Manages the drawing, updating, and creation of all Enemy objects for use within the game.
    /// </summary>
    class EnemyManager : Manager
    {
        #region Fields

        private static List<Enemy> managedEnemies;
        private static Turret turret;
        private static int waveNumber;

        #endregion

        #region Initializer Methods

        /// <summary>
        /// Initailizes all data for use in the EnemyManager.
        /// </summary>
        public EnemyManager(Turret t)
        {
            managedEnemies = new List<Enemy>();
            turret = t;
            waveNumber = 0;
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

        public static int WaveNumber
        {
            get { return waveNumber; }
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
        /// <param name="scale">Scale of the Enemy model</param>
        /// <returns>A reference to the spawned Enemy</returns>
        public static Enemy spawnEnemy(EnemyType enemyType, Vector3 location, Vector3 rotation, Vector3 scale)
        {
            Enemy spawnedEnemy = null;

            switch (enemyType)
            {
                case EnemyType.BasicPlane:

                    break;
                case EnemyType.BasicTank:
                    spawnedEnemy = new Tank(location, rotation, scale, turret.Location);
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

            // spawn the next wave if there are no more enemies
            if (managedEnemies.Count() == 0)
            {
                nextWave();
            }
        }

        /// <summary>
        /// Handles creation of waves of enemies.
        /// </summary>
        public void nextWave()
        {
            for (int j = 0; j < waveNumber; j++)
            {
                for (int i = 0; i < 21; i++)
                {
                    Enemy basicTank =
                        EnemyManager.spawnEnemy(EnemyType.BasicTank, new Vector3(-5 + i, 4, 30 + (5 * j)), Vector3.Empty, new Vector3(1f,1f,1f));
                }
            }

            waveNumber += 1;
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
