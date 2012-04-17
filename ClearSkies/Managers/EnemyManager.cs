using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ClearSkies.Prefabs.Enemies;
using ClearSkies.Prefabs.Turrets;
using Microsoft.DirectX.Direct3D;
using Microsoft.DirectX;
using ClearSkies.Prefabs.Enemies.Planes;
using ClearSkies.Exceptions;
using ClearSkies.Properties;
using ClearSkies.Prefabs.Enemies.Tanks;

namespace ClearSkies.Managers
{
    /// <summary>
    /// Manages the drawing, updating, and creation of all Enemy objects for use within the game.
    /// </summary>
    class EnemyManager : Manager
    {
        #region Fields

        private static List<Enemy> managedEnemies;
        private static List<Wave> waves;
        private static int currentWave;
        private static float timeSinceLastSpawn;
        private static Random random;
        private static bool initialized;

        #endregion

        #region Initializer Methods

        /// <summary>
        /// Initailizes all data for use in the EnemyManager.
        /// </summary>
        public EnemyManager(List<Wave> wavesToSpawn, int startWave)
        {
            managedEnemies = new List<Enemy>();
            waves = wavesToSpawn;
            currentWave = startWave;
            timeSinceLastSpawn = 0f;
            random = new Random();
            initialized = true;
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

        public static Wave CurrentWave
        {
            get { return waves[currentWave]; }
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
        public static Enemy spawnEnemy(EnemyType enemyType, Vector3 location, Vector3 rotation, Vector3 scale, float speed, float turnSpeed)
        {
            checkIfInitialized();
            Enemy spawnedEnemy = null;

            switch (enemyType)
            {
                case EnemyType.BasicPlane:
                    spawnedEnemy = new BasicPlane(location + Settings.DEFAULT_PLANE_HEIGHT, rotation, scale, speed, turnSpeed);
                    break;
                case EnemyType.BasicTank:
                    spawnedEnemy = new BasicTank(location, rotation, scale, speed, turnSpeed);
                    break;
            }

            if (spawnedEnemy != null)
            {
                managedEnemies.Add(spawnedEnemy);
            }

            return spawnedEnemy;
        }
        
        #endregion

        #region Private Static Methods

        private static void checkIfInitialized()
        {
            if (!initialized)
            {
                throw new UninitializedException(Resources.Enemy_Manager_Uninitialized_Exception);
            }
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
                    Wave changedWave = waves[currentWave];

                    if (managedEnemies[i] is BasicPlane)
                    {
                        changedWave.planesDestroyed++;
                    }
                    else if (managedEnemies[i] is BasicTank)
                    {
                        changedWave.tanksDestroyed++;
                    }

                    managedEnemies.RemoveAt(i);
                    waves[currentWave] = changedWave;
                }
            }

            timeSinceLastSpawn += deltaTime;

            if (currentWave < waves.Count && timeSinceLastSpawn >= waves[currentWave].spawnDelay)
            {
                Wave changedWave = waves[currentWave];

                for (int i = 0; i < waves[currentWave].enemiesPerSpawn; i++)
                {
                    bool spawnTanks = changedWave.tanksSpawned < changedWave.tanksToSpawn;
                    bool spawnPlanes = changedWave.planesSpawned < changedWave.planesToSpawn;

                    Vector3 spawnRotation = new Vector3((float)(2 * Math.PI * random.NextDouble()), 0f, 0f);
                    Vector3 spawnLocation = new Vector3((float)Math.Cos(spawnRotation.X) * changedWave.spawnDistance, 0f, (float)Math.Sin(spawnRotation.X) * changedWave.spawnDistance);

                    if (spawnTanks & spawnPlanes)
                    {
                        switch (random.Next(1))
                        {
                            case 0:
                                spawnEnemy(EnemyType.BasicTank, spawnLocation, -spawnRotation, Settings.DEFAULT_TANK_SCALE, changedWave.tankSpeed, changedWave.tankTurnSpeed);
                                changedWave.tanksSpawned++;
                                break;
                            case 1:
                                spawnEnemy(EnemyType.BasicPlane, spawnLocation, spawnRotation, Settings.DEFAULT_PLANE_SCALE, changedWave.planeSpeed, changedWave.planeTurnSpeed);
                                changedWave.planesSpawned++;
                                break;
                        }
                    }
                    else if (spawnTanks)
                    {
                        spawnEnemy(EnemyType.BasicTank, spawnLocation, spawnRotation, Settings.DEFAULT_TANK_SCALE, changedWave.tankSpeed, changedWave.tankTurnSpeed);
                        changedWave.tanksSpawned++;
                    }
                    else if (spawnPlanes)
                    {
                        spawnEnemy(EnemyType.BasicPlane, spawnLocation, spawnRotation,Settings.DEFAULT_PLANE_SCALE, changedWave.planeSpeed, changedWave.planeTurnSpeed);
                        changedWave.planesSpawned++;
                    }
                }

                waves[currentWave] = changedWave;

                if (waves[currentWave].planesDestroyed == waves[currentWave].planesToSpawn &&
                    waves[currentWave].tanksDestroyed == waves[currentWave].tanksToSpawn &&
                    currentWave < waves.Count)
                {
                    currentWave++;
                }
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
