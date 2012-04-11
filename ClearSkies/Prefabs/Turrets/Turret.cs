using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.DirectX;
using Microsoft.DirectX.Direct3D;
using ClearSkies.Scripts;
using DI = Microsoft.DirectX.DirectInput;
using ClearSkies.Managers;

namespace ClearSkies.Prefabs.Turrets
{
    /// <summary>
    /// A Prefab used to represent the Turrets that Players will use to shoot
    /// at Enemies.
    /// </summary>
    abstract class Turret : Prefab
    {
        #region Fields

        private static Vector3 DEFAULT_TURRET_ROTATION = new Vector3(0f, (float)Math.PI / 2f, 0f);

        private List<TurretBarrel> barrels;

        #endregion

        #region Initializer Methods

        /// <summary>
        /// Creates a vanilla Turret that will be at the given location and 
        /// facing the given rotation. The turret is able to turn at the given
        /// speed and is controlled with the given keyboard Device.
        /// </summary>
        /// <param name="location">Location of the Turret in game</param>
        /// <param name="rotation">Rotation the Turret is facing</param>
        /// <param name="turretRotationSpeed">Speed the Turret can rotate at</param>
        /// <param name="keyboard">Keyboard Device used to controll the Turret</param>
        public Turret(Vector3 location, Vector3 rotation, float turretRotationSpeed, DI.Device keyboard) : base(location, rotation)
        {
            this.barrels = new List<TurretBarrel>();

            this.scripts.Add(new TurretRotationScript(this, keyboard, turretRotationSpeed));
        }

        #endregion

        #region Getters and Setters

        /// <summary>
        /// The angle of rotation for the TurretBarrels.
        /// </summary>
        public float BarrelRotation
        {
            get 
            {
                float barrelRotation = 0.0f;

                if (children.Count > 0)
                {
                    barrelRotation = children[0].Rotation.Y;
                }

                return barrelRotation;
            }
            set 
            { 
                foreach (Prefab child in children)
                {
                    if (child is TurretBarrel)
                    {
                        Vector3 childRotation = child.Rotation;
                        childRotation.Y = value;
                        child.Rotation = childRotation;
                    }
                }
            }
        }

        #endregion
    }
}
