using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.DirectX;
using ClearSkies.Scripts;
using ClearSkies.Content;
using ClearSkies.Prefabs.Enemies;

namespace ClearSkies.Prefabs.Bullets
{
    /// <summary>
    /// A simple Bullet with no special effects.
    /// </summary>
    class BasicBullet : Bullet
    {
        #region Fields

        private const float SPEED = 1f;
        private const float DAMAGE = 1f;
        private const float LIFESPAN = 2f;

        #endregion

        #region Initializer Methods

        /// <summary>
        /// Creates a BasicBullet object at the given location facing the given rotation.
        /// Bullet will travel forward and use standard collisions.
        /// </summary>
        /// <param name="location">Location of the Bullet</param>
        /// <param name="rotation">Rotation the Bullet should face</param>
        public BasicBullet(Vector3 location, Vector3 rotation, Vector3 scale) : base(location, rotation, scale, ContentLoader.BasicBulletModel, DAMAGE, LIFESPAN, SPEED)
        {
            this.scripts.Add(new BulletStraightMovementScript(this));
            this.scripts.Add(new BulletPlainCollisionScript(this));
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Removes Bullet from game if collider is Enemy.
        /// </summary>
        /// <param name="collider">Prefab collided with</param>
        public override void detectCollision(Prefab collider)
        {
            base.detectCollision(collider);
            this.alive = !(collider is  Enemy);
        }

        #endregion
    }
}
