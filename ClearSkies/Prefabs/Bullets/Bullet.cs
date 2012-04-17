using ClearSkies.Content;

namespace ClearSkies.Prefabs.Bullets
{
    /// <summary>
    /// A Prefab used to represent the projectiles fired from Players and 
    /// Enemies.
    /// </summary>
    abstract class Bullet : Prefab
    {
        #region Fields

        protected float damage;
        protected float lifespan;
        protected Prefab owner;
        protected float speed;
        protected float timeAlive;

        #endregion

        #region Initializer Methods

        /// <summary>
        /// Creates a single vanilla Bullet at the given location facing the 
        /// given rotation. The Bullet is visually represented by the given 
        /// Model, will travel that the given speed, inflict the given damage 
        /// to Enemies it collides with, and will remain alive for the given
        /// lifespan unless a collision occures.
        /// </summary>
        /// <param name="owner">The Prefab that owns the Bullet</param>
        /// <param name="bulletModel">Model to represent Bullet</param>
        /// <param name="damage">Damage Bullet will inflict</param>
        /// <param name="lifespan">Time in seconds the bullet will remain in scene</param>
        /// <param name="speed">Speed bullet will travel at</param>
        public Bullet(Prefab owner, Model bulletModel, float damage, float lifespan, float speed)
            : base(owner.Location, owner.Rotation, owner.Scale)
        {
            this.owner = owner;
            this.damage = damage;
            this.lifespan = lifespan;
            this.timeAlive = 0.0f;
            this.speed = speed;

            this.models.Add(bulletModel);
        }

        #endregion

        #region Getter and Setter Methods

        /// <summary>
        /// Speed bullet should travel at.
        /// </summary>
        public float Speed
        {
            get { return this.speed; }
        }

        /// <summary>
        /// The Prefab that owns the Bullet.
        /// </summary>
        public Prefab Owner
        {
            get { return this.owner; }
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
        /// <param name="deltaTime">
        /// The time in seconds since the last update
        /// </param>
        public override void update(float deltaTime)
        {
            base.update(deltaTime);
            this.timeAlive += deltaTime;

            this.alive = this.alive && lifespan >= timeAlive;
        }
        
        #endregion
    }
}