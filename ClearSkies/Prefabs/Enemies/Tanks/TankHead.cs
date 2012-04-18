using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.DirectX;
using DI = Microsoft.DirectX.DirectInput;
using ClearSkies.Scripts;

namespace ClearSkies.Prefabs.Enemies.Tanks
{
    abstract class TankHead : Prefab
    {
        #region Fields

        private List<TankBarrel> barrels;
        private float headRotationSpeed;
        private float barrelRotationSpeed;

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
        public TankHead(Vector3 location, Vector3 rotation, Vector3 scale, float headRotationSpeed, float barrelRotationSpeed) : base(location, rotation, scale)
        {
            this.headRotationSpeed = headRotationSpeed;
            this.barrelRotationSpeed = barrelRotationSpeed;
            this.barrels = new List<TankBarrel>();

            this.scripts.Add(new TankHeadRotationScript(this));
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
                    if (child is TankBarrel)
                    {
                        Vector3 childRotation = child.Rotation;
                        childRotation.Y = value;
                        child.Rotation = childRotation;
                    }
                }
            }
        }

        public float BarrelRotationSpeed
        {
            get { return this.barrelRotationSpeed; }
        }

        public float HeadRotationSpeed
        {
            get { return this.headRotationSpeed; }
        }

        #endregion
    }
}
