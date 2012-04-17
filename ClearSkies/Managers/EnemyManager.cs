using System;
using System.Collections.Generic;
using ClearSkies.Exceptions;
using ClearSkies.Prefabs.Enemies;
using ClearSkies.Prefabs.Enemies.Planes;
using ClearSkies.Prefabs.Enemies.Tanks;
using ClearSkies.Properties;
using Microsoft.DirectX;
using Microsoft.DirectX.Direct3D;

namespace ClearSkies.Managers
{
    /// <summary>
    /// Manages the drawing, updating, and creation of all Enemy objects for 
    /// use within the game.
    /// </summary>
    class EnemyManager : Manager
    {
        #region Fields

        private static bool initialized;
        private static List<Enemy> managedEnemies;
        private static Random random;
        private static float timeSinceLastSpawn;

        private static int currentWave;
        private static List<Wave> waves;

        #endregion

        #region Initializer Methods

        /// <summary>
        /// Initailizes all data for use in the EnemyManager.
        /// </summary>
        /// <param name="wavesToSpawn">A List of all Enemy Waves to spawn</param>
        /// <param name="startWave">The number of the first Wave to spawn</param>
        public EnemyManager(List<Wave> wavesToSpawn, int startWave)
        {
            random = new Random();
            initialize(wavesToSpawn, startWave);
        }

        /// <summary>
        /// Initailizes all data for use in the EnemyManager.
        /// </summary>
        /// <param name="wavesToSpawn">A List of all Enemy Waves to spawn</param>
        /// <param name="startWave">The number of the first Wave to spawn</param>
        /// <param name="seed">A seed for randomly choosing wether or not to 
        /// spawn a Tank or a Plane</param>
        public EnemyManager(List<Wave> wavesToSpawn, int startWave, int seed)
        {
            random = new Random(seed);
            initialize(wavesToSpawn, startWave);
        }

        /// <summary>
        /// Initializes all data for use in the EnemyManager.
        /// </summary>
        /// <param name="wavesToSpawn">A List of all Enemy Waves to spawn</param>
        /// <param name="startWave">The number of the first Wave to spawn</param>
        private void initialize(List<Wave> wavesToSpawn, int startWave)
        {
            currentWave = startWave;
            waves = wavesToSpawn;

            managedEnemies = new List<Enemy>();
            timeSinceLastSpawn = 0f;

            initialized = true;
        }

        #endregion

        #region Getter Methods

        /// <summary>
        /// The curret Wave being spawned by the EnemyManager.
        /// </summary>
        public static Wave CurrentWave
        {
            get { return waves[currentWave]; }
        }

        /// <summary>
        /// All Enemy Prefabs currently being managed.
        /// </summary>
        public static List<Enemy> ManagedEnemies
        {
            get { return managedEnemies; }
        }

        #endregion

        #region Public Static Methods

        /// <summary>
        /// Creates a single Enemy unit of the give type at the give location
        /// and facing the given rotation.
        /// </summary>
        /// <param name="enemyType">Type of Enemy to spawn</param>
        /// <param name="location">Locaiton to spawn Enemy at</param>
        /// <param name="rotation">Rotation for Enemy to face</param>
        /// <param name="scale">Scale of the Enemy model</param>
        /// <param name="speed">The speed an Enemy can move forward at</param>
        /// <param name="turnSpeed">The speed and Enemy can rotate at</param>
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

        /// <summary>
        /// Checks if the EnemyManager was initialized. This should be called 
        /// at the beginning of every public static method.
        /// </summary>
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
        /// Updates all Enemy Prefabs being managed within this manager, 
        /// removes any Enemy Prefabs that no longer meet the requirements of 
        /// being alive, and spaws Enemy Tanks or Planes if needed.
        /// </summary>
        /// <param name="deltaTime">Time in seconds since last update</param>
        public void update(float deltaTime)
        {
            if (currentWave < waves.Count)
            {
                Wave changedWave = waves[currentWave];

                for (int i = 0; i < managedEnemies.Count; i++)
                {
                    if (managedEnemies[i].Alive)
                    {
                        managedEnemies[i].update(deltaTime);
                    }
                    else
                    {
                        if (managedEnemies[i] is BasicPlane)
                        {
                            changedWave.planesDestroyed++;
                        }
                        else if (managedEnemies[i] is BasicTank)
                        {
                            changedWave.tanksDestroyed++;
                        }

                        managedEnemies.RemoveAt(i);
                    }
                }

                timeSinceLastSpawn += deltaTime;

                // spawn enemies if nessecary
                if (timeSinceLastSpawn >= waves[currentWave].spawnDelay)
                {
                    for (int i = 0; i < waves[currentWave].enemiesPerSpawn; i++)
                    {
                        bool spawnTanks = changedWave.tanksSpawned < changedWave.tanksToSpawn;
                        bool spawnPlanes = changedWave.planesSpawned < changedWave.planesToSpawn;

                        Vector3 spawnRotation = new Vector3((float)(2 * Math.PI * random.NextDouble()), 0f, 0f);
                        Vector3 spawnLocation = new Vector3(
                            (float)Math.Cos(spawnRotation.X) * changedWave.spawnDistance, 
                            0f, 
                            (float)Math.Sin(spawnRotation.X) * changedWave.spawnDistance);

                        if (spawnTanks & spawnPlanes)
                        {
                            switch (random.Next(1))
                            {
                                case 0:
                                    spawnEnemy(EnemyType.BasicTank, spawnLocation, -spawnRotation, Settings.DEFAULT_TANK_SCALE, 
                                        changedWave.tankSpeed, changedWave.tankTurnSpeed);
                                    changedWave.tanksSpawned++;
                                    break;
                                case 1:
                                    spawnEnemy(EnemyType.BasicPlane, spawnLocation, spawnRotation, Settings.DEFAULT_PLANE_SCALE, 
                                        changedWave.planeSpeed, changedWave.planeTurnSpeed);
                                    changedWave.planesSpawned++;
                                    break;
                            }
                        }
                        else if (spawnTanks)
                        {
                            spawnEnemy(EnemyType.BasicTank, spawnLocation, spawnRotation, Settings.DEFAULT_TANK_SCALE, 
                                changedWave.tankSpeed, changedWave.tankTurnSpeed);
                            changedWave.tanksSpawned++;
                        }
                        else if (spawnPlanes)
                        {
                            spawnEnemy(EnemyType.BasicPlane, spawnLocation, spawnRotation, Settings.DEFAULT_PLANE_SCALE, 
                                changedWave.planeSpeed, changedWave.planeTurnSpeed);
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
