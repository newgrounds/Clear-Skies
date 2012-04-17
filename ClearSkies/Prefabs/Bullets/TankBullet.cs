using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.DirectX;
using ClearSkies.Scripts;
using ClearSkies.Content;
using ClearSkies.Prefabs.Turrets;

namespace ClearSkies.Prefabs.Bullets
{
    /// <summary>
    /// A tank Bullet with no special effects.
    /// </summary>
    class TankBullet : Bullet
    {
        #region Fields

        private static float speed = 0;
        private const float DAMAGE = 1f;
        private const float LIFESPAN = 2f;
        private Turret turret;
        Random rand = new Random();

        #endregion

        #region Initializer Methods

        /// <summary>
        /// Creates a BasicBullet object at the given location facing the given rotation.
        /// Bullet will travel forward and use standard collisions.
        /// </summary>
        /// <param name="location">Location of the Bullet</param>
        /// <param name="rotation">Rotation the Bullet should face</param>
        public TankBullet(Vector3 location, Vector3 rotation, Vector3 scale, Turret t)
            : base(location, rotation, scale, ContentLoader.BasicBulletModel, DAMAGE, LIFESPAN, speed)
        {
            this.turret = t;
            this.scripts.Add(new BulletLocationMovementScript(this, this.turret));
            this.scripts.Add(new BulletTurretCollisionScript(this));
            speed = ((float)(rand.Next(0, 5)) * 0.1f) + 1f;
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
            this.alive = !(collider is Turret);
        }

        #endregion
    }
}
