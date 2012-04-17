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

        private const float SPEED = 1f;
        private const float DAMAGE = 1f;
        private const float LIFESPAN = 2f;
        private Turret turret;
        private Prefab owner;
        //Random rand = new Random();

        #endregion

        #region Initializer Methods

        /// <summary>
        /// Creates a BasicBullet object at the given location facing the given rotation.
        /// Bullet will travel forward and use standard collisions.
        /// </summary>
        /// <param name="owner">The prefab that fired this bullet.</param>
        /// <param name="t">The turret to aim for.</param>
        public TankBullet(Prefab owner, Turret t)
            : base(owner, ContentLoader.BasicBulletModel, DAMAGE, LIFESPAN, SPEED)
        {
            this.owner = owner;
            this.turret = t;
            this.scripts.Add(new BulletLocationMovementScript(this, this.turret));
            this.scripts.Add(new BulletTurretCollisionScript(this));
            //speed = ((float)(rand.Next(0, 5)) * 0.1f) + 1f;
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
