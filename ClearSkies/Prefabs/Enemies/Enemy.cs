﻿using Microsoft.DirectX;

namespace ClearSkies.Prefabs.Enemies
{
    /// <summary>
    /// A Prefab used to represent the Enemies a Player will be fighting in 
    /// game.
    /// </summary>
    abstract class Enemy : Prefab
    {
        #region Fields

        private float colliderSize;

        #endregion

        #region Initailizer Methods

        /// <summary>
        /// Creates a vanilla Enemy at the give locaiton, facing the given
        /// rotation, scaled to the given size, with a collision sphere of the
        /// given size.
        /// </summary>
        /// <param name="location">Location of Enemy in gameworld</param>
        /// <param name="rotation">
        /// Direction Enemy is facing in gameworld
        /// </param>
        /// <param name="scale">Scale of the Enemy</param>
        /// <param name="colliderSize">
        /// Size of collision sphere on enemy
        /// </param>
        public Enemy(Vector3 location, Vector3 rotation, Vector3 scale, float colliderSize)
            : base(location, rotation, scale)
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
    }
}
