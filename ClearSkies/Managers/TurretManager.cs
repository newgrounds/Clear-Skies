using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ClearSkies.Prefabs.Turrets;
using Microsoft.DirectX;
using DI = Microsoft.DirectX.DirectInput;
using D3D = Microsoft.DirectX.Direct3D;
using Microsoft.DirectX.Direct3D;
using ClearSkies.Prefabs;

namespace ClearSkies.Managers
{
    /// <summary>
    /// Manages the drawing, updating, and creation of all Turret objects for use within the game.
    /// </summary>
    class TurretManager : Manager
    {
        #region Fields

        private static List<Turret> managedTurrets;

        #endregion

        #region Initializer Methods

        /// <summary>
        /// Initailizes all data for use in the TurretManager.
        /// </summary>
        static TurretManager() 
        {
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

        #region Static Methods

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
        public void draw(Device device)
        {
            foreach (Turret managedTurret in managedTurrets)
            {
                managedTurret.draw(device);
            }
        }

        #endregion
    }
}
