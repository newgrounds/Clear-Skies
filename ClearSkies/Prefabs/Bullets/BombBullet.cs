using ClearSkies.Content;
using ClearSkies.Managers;
using ClearSkies.Scripts;
using ParticleEngine;

namespace ClearSkies.Prefabs.Bullets
{
    /// <summary>
    /// A bomb bullet that is dropped on Turret objects.
    /// </summary>
    class BombBullet : Bullet
    {
        #region Initializer Methods

        /// <summary>
        /// Creates a BombBullet at the given location facing the given 
        /// rotation.
        /// </summary>
        /// <param name="owner">The Prefab that owns this Bomb</param>
        public BombBullet(Prefab owner) : base(owner, ContentLoader.BombBulletModel, Settings.BOMB_BULLET_DAMAGE, 
            Settings.BOMB_BULLET_LIFESPAN, Settings.BOMB_BULLET_SPEED)
        {
            this.scripts.Add(new BombMovementScript(this, this.speed));
            this.scripts.Add(new BombCollisionScript(this));
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// If collider is not the Bombs owner it will explode.
        /// </summary>
        /// <param name="collider">Prefab that has collided witht the Bomb</param>
        public override void detectCollision(Prefab collider)
        {
            base.detectCollision(collider);
            if (owner != collider)
            {
                ParticleEmitterManager.spawnParticleEmitter(ParticleEmitterType.explosion, this.location);
                this.alive = false;
            }
        }

        #endregion
    }
}