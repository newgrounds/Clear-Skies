using System.Collections.Generic;
using ClearSkies.Exceptions;
using ClearSkies.Prefabs.Turrets;
using ClearSkies.Properties;
using Microsoft.DirectX;
using D3D = Microsoft.DirectX.Direct3D;
using DI = Microsoft.DirectX.DirectInput;

namespace ClearSkies.Managers
{
    /// <summary>
    /// Manages the drawing, updating, and creation of all Turret objects for 
    /// use within the game.
    /// </summary>
    class TurretManager : Manager
    {
        #region Fields

        private static bool initialized;
        private static List<Turret> managedTurrets;

        #endregion

        #region Initializer Methods

        /// <summary>
        /// Initailizes all data for use in the TurretManager.
        /// </summary>
        static TurretManager() 
        {
            initialized = true; 
            managedTurrets = new List<Turret>();
        }

        #endregion

        #region Getter Methods

        /// <summary>
        /// All Turret Prefabs currently being managed.
        /// </summary>
        public static List<Turret> ManagedTurrets
        {
            get { return managedTurrets; }
        }

        #endregion

        #region Public Static Methods

        /// <summary>
        /// Spawns a new turret of the given type and the given location and 
        /// facing the given rotation. Turret is controlled using the given 
        /// input Device.
        /// </summary>
        /// <param name="turretType">Type of Turret to spawn</param>
        /// <param name="location">Location to spawn Turret at</param>
        /// <param name="rotation">Rotation for turret to be facing</param>
        /// <param name="keyboard">Keyboard Device used to control turret</param>
        /// <returns>A reference to the spawned Turret</returns>
        public static Turret spawnTurret(TurretType turretType, Vector3 location, Vector3 rotation, Vector3 scale, DI.Device keyboard)
        {
            checkIfInitialized();
            Turret spawnedTurret = null;

            switch (turretType)
            {
                case TurretType.Test:
                    spawnedTurret = new TestTurret(location, rotation, scale, keyboard);
                    break;
                case TurretType.Basic:
                    spawnedTurret = new BasicTurret(location, rotation, scale, keyboard);
                    break;
            }

            managedTurrets.Add(spawnedTurret);
            return spawnedTurret;
        }

        #endregion

        #region Private Static Methods

        /// <summary>
        /// Checks if the TurretManager was initialized. This should 
        /// be called at the beginning of every public static method.
        /// </summary>
        private static void checkIfInitialized()
        {
            if (!initialized)
            {
                throw new UninitializedException(Resources.Turret_Manager_Uninitialized_Exception);
            }
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Updates all Turret Prefabs being managed within this manager. Also 
        /// removes any Turret Prefabs that no longer meet the requirements of 
        /// being alive.
        /// </summary>
        /// <param name="deltaTime">Time in seconds since last update</param>
        public void update(float deltaTime)
        {
            for (int i = 0; i < managedTurrets.Count; i++)
            {
                if (managedTurrets[i].Alive)
                {
                    managedTurrets[i].update(deltaTime);
                }
                else
                {
                    managedTurrets.RemoveAt(i);
                }
            }
        }

        /// <summary>
        /// Draws all Turret Prefabs managed in this Manager.
        /// </summary>
        /// <param name="device">Device to draw Turret Prefabs to</param>
        public void draw(D3D.Device device)
        {
            foreach (Turret managedTurret in managedTurrets)
            {
                managedTurret.draw(device);
            }
        }

        #endregion
    }
}