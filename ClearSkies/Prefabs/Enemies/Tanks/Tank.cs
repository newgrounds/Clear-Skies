using ClearSkies.Content;
using ClearSkies.Managers;
using ClearSkies.Prefabs.Bullets;
using Microsoft.DirectX;
using ParticleEngine;

namespace ClearSkies.Prefabs.Enemies.Tanks
{
    abstract class Tank : Enemy
    {
        #region Initializer Methods

        /// <summary>
        /// Creates a Tank represented by the given Model, at the given 
        /// location, facing the given rotation, scaled to the given amount,
        /// and with the a collision sphere of the given size.
        /// </summary>
        /// <param name="tankModel">Model that represents the Tank</param>
        /// <param name="location">Location of the Tank</param>
        /// <param name="rotation">Rotation of the Tank</param>
        /// <param name="scale">Scale of the Tank</param>
        /// <param name="colliderSize">
        /// Size of the Tanks collision sphere
        /// </param>
        public Tank(Model tankModel, Vector3 location, Vector3 rotation, Vector3 scale, float colliderSize)
            : base(location, rotation, scale, colliderSize)
        {
            this.models.Add(tankModel);
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Destorys the Tank if the collider Prefab is a Bullet not owned by 
        /// this Tank.
        /// </summary>
        /// <param name="collider">Prefab that collided with the Tank</param>
        public override void detectCollision(Prefab collider)
        {
            base.detectCollision(collider);

            if (collider is Bullet)
            {
                Bullet collidingBullet = (Bullet)collider;
                if (collidingBullet.Owner != this)
                {
                    this.scripts.Clear();
                    this.alive = false;
                    ParticleEmitterManager.spawnParticleEmitter(ParticleEmitterType.explosion, location);
                }
            }
        }

        #endregion
    }
}