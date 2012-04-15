using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.DirectX;
using ClearSkies.Prefabs.Enemies;
using ClearSkies.Scripts;

namespace ClearSkies.Prefabs.Bullets
{
    /// <summary>
    /// A Prefab used to represent the projectiles fired from Players and Enemies.
    /// </summary>
    abstract class Bullet : Prefab
    {
        #region Fields

        private float damage;
        private float lifespan;
        private float timeAlive;
        private float speed;
        private bool destroy;

        #endregion

        #region Initializer Methods

        /// <summary>
        /// Creates a single vanilla Bullet at the given location facing the 
        /// given rotation. The Bullet is visually represented by the given 
        /// Model, will travel that the given speed, inflict the given damage 
        /// to Enemies it collides with, and will remain alive for the given
        /// lifespan unless a collision occures.
        /// </summary>
        /// <param name="location">Location to place Bullet in scene</param>
        /// <param name="rotation">Rotation for Bullet to face</param>
        /// <param name="bulletModel">Model to represent Bullet</param>
        /// <param name="damage">Damage Bullet will inflict</param>
        /// <param name="lifespan">Time in seconds the bullet will remain in scene</param>
        /// <param name="speed">Speed bullet will travel at</param>
        public Bullet(Vector3 location, Vector3 rotation, Vector3 scale, Model bulletModel, float damage, float lifespan, float speed)
            : base(location, rotation, scale)
        {
            this.damage = damage;
            this.lifespan = lifespan;
            this.timeAlive = 0.0f;
            this.speed = speed;

            this.models.Add(bulletModel);
        }

        #endregion

        #region Getter and Setter Methods

        /// <summary>
        /// True if Bullet should destory itself. Used for animation.
        /// </summary>
        public bool Destroy
        {
            get { return this.destroy; }
            set { this.destroy = value; }
        }

        /// <summary>
        /// Speed bullet should travel at.
        /// </summary>
        public float Speed
        {
            get { return this.speed; }
        }

        /// <summary>
        /// The amount of damage this bullet does.
        /// </summary>
        public float Damage
        {
            get { return this.damage; }
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Updates the Bullet as a normal Prefab and checks to see if the 
        /// Bullet should still be alive.
        /// </summary>
        /// <param name="deltaTime"></param>
        public override void update(float deltaTime)
        {
            base.update(deltaTime);
            this.timeAlive += deltaTime;

            this.alive = this.alive && lifespan >= timeAlive && !destroy;
        }
        
        #endregion
    }
}
