using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.DirectX;
using ClearSkies.Content;
using ClearSkies.Scripts;
using ClearSkies.Prefabs.Enemies;
using ClearSkies.Managers;
using ParticleEngine;

namespace ClearSkies.Prefabs.Bullets
{
    /// <summary>
    /// A basic bullet that glows for extra visiability and explodes on impact with enemy.
    /// </summary>
    class BombBullet : Bullet
    {
        #region Initializer Methods

        /// <summary>
        /// Creates a TracerBullet at the given location facing the given rotation.
        /// </summary>
        /// <param name="location"></param>
        /// <param name="rotation"></param>
        public BombBullet(Prefab owner) : base(owner, ContentLoader.BombBulletModel, Settings.BOMB_BULLET_DAMAGE, Settings.BOMB_BULLET_LIFESPAN, Settings.BOMB_BULLET_SPEED)
        {
            this.scripts.Add(new BombMovementScript(this, speed));
            this.scripts.Add(new BombCollisionScript(this));
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
            ParticleEmitterManager.spawnParticleEmitter(ParticleEmitterType.explosion, this.location);
            this.alive = false;
        }

        #endregion
    }
}
