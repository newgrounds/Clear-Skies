using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.DirectX;
using ClearSkies.Prefabs.Bullets;

namespace ClearSkies.Prefabs.Enemies
{
    /// <summary>
    /// A Prefab used to represent the Enemies a Player will be fighting in game.
    /// </summary>
    abstract class Enemy : Prefab
    {
        #region Fields

        private float colliderSize;

        #endregion

        #region Initializer Methods

        /// <summary>
        /// Creates a vanilla Enemy at the give location facing the given
        /// rotation with a collider of the given size.
        /// </summary>
        /// <param name="location">Location of Enemy in gameworld</param>
        /// <param name="rotation">Direction Enemy is facing in gameworld</param>
        /// <param name="colliderSize">Size of collider on enemy</param>
        public Enemy(Vector3 location, Vector3 rotation, float colliderSize)
            : base(location, rotation)
        {
            this.colliderSize = colliderSize;
        }

        #endregion

        #region Getters and Setters

        /// <summary>
        /// The size of the spherical collider for this Enemy.
        /// </summary>
        public float ColliderSize
        {
            get { return this.colliderSize; }
        }

        #endregion

        #region Public Methods

        // TODO: extract this to subclasses.
        public override void detectCollision(Prefab collider)
        {
            base.detectCollision(collider);

            if (collider is Bullet)
            {
                this.scripts.Clear();
                // TODO: Add death animation script
            }
        }

        #endregion
    }
}
