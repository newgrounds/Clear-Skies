using ClearSkies.Content;
using ClearSkies.Managers;
using ClearSkies.Prefabs.Bullets;
using Microsoft.DirectX;
using ParticleEngine;

namespace ClearSkies.Prefabs.Enemies.Tanks
{
    abstract class Tank : Enemy
    {
        #region Fields

        private float driveSpeed;
        private float turnSpeed;

        #endregion

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
        public Tank(Model tankModel, Vector3 location, Vector3 rotation, Vector3 scale, float driveSpeed, float turnSpeed, float colliderSize)
            : base(location, rotation, scale, colliderSize)
        {
            this.driveSpeed = driveSpeed;
            this.turnSpeed = turnSpeed;

            this.models.Add(tankModel);
        }

        #endregion

        #region Getter and Setter Methods

        /// <summary>
        /// The speed at which the tank can drive.
        /// </summary>
        public float DriveSpeed
        {
            get { return this.driveSpeed; }
        }

        /// <summary>
        /// The speed at which the Tank can turn.
        /// </summary>
        public float TurnSpeed
        {
            get { return this.turnSpeed; }
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
            bool die = true;

            if (collider is Bullet)
            {
                Bullet collidingBullet = (Bullet)collider;
                die = collidingBullet.Owner != this;
            }

            if (die)
            {
                this.scripts.Clear();
                this.alive = false;
                ParticleEmitterManager.spawnParticleEmitter(ParticleEmitterType.explosion, location);
            }
        }

        #endregion
    }
}