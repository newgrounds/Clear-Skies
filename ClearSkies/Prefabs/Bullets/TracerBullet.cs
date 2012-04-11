using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.DirectX;
using ClearSkies.Content;
using ClearSkies.Scripts;
using ClearSkies.Prefabs.Enemies;

namespace ClearSkies.Prefabs.Bullets
{
    /// <summary>
    /// A basic bullet that glows for extra visiability and explodes on impact with enemy.
    /// </summary>
    class TracerBullet : Bullet
    {
        #region Fields

        private const float SPEED = 1f;
        private const float DAMAGE = 3f;
        private const float LIFESPAN = 2f;

        #endregion

        #region Initializer Methods

        /// <summary>
        /// Creates a TracerBullet at the given location facing the given rotation.
        /// </summary>
        /// <param name="location"></param>
        /// <param name="rotation"></param>
        public TracerBullet(Vector3 location, Vector3 rotation) : base(location, rotation, ContentLoader.TracerBulletModel, DAMAGE, LIFESPAN, SPEED)
        {
            this.scripts.Add(new BulletStraightMovementScript(this));
            // TODO: explode script
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// If collider is instance of Enemy the Bullet will explode.
        /// </summary>
        /// <param name="collider">Prefab that has collided witht the Bullet</param>
        public override void detectCollision(Prefab collider)
        {
            base.detectCollision(collider);

            if (collider is Enemy)
            {
                this.scripts.Clear();
                // TODO: add explosion script
            }
        }

        #endregion
    }
}
