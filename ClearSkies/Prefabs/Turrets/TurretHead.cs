using System.Collections.Generic;
using ClearSkies.Scripts;
using Microsoft.DirectX;
using DI = Microsoft.DirectX.DirectInput;

namespace ClearSkies.Prefabs.Turrets
{
    /// <summary>
    /// A Prefab used to represent the TurretHead that Players will use to 
    /// shoot at Enemies.
    /// </summary>
    abstract class TurretHead : Prefab
    {
        #region Fields

        private List<TurretBarrel> barrels;

        #endregion

        #region Initializer Methods

        /// <summary>
        /// Creates a vanilla Turret that will be at the given location and 
        /// facing the given rotation. The turret is able to turn at the given
        /// speed and is controlled with the given keyboard Device.
        /// </summary>
        /// <param name="location">Location of the TurretHead in game</param>
        /// <param name="rotation">Rotation the TurretHead is facing</param>
        /// <param name="scale">Scale of the TurretHead</param>
        /// <param name="turretRotationSpeed">
        /// Speed the TurretHead can rotate at
        /// </param>
        /// <param name="keyboard">
        /// Keyboard Device used to control the TurretHead
        /// </param>
        public TurretHead(Vector3 location, Vector3 rotation, Vector3 scale, float turretRotationSpeed, DI.Device keyboard) : base(location, rotation, scale)
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
