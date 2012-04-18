using ClearSkies.Prefabs.Bullets;
using Microsoft.DirectX;
using ClearSkies.Prefabs.Enemies.Tanks;

namespace ClearSkies.Prefabs.Turrets
{
    /// <summary>
    /// A Turret the player will control in the game.
    /// </summary>
    abstract class Turret : Prefab
    {
        #region Fields

        protected TurretHead head;
        protected float colliderSize;
        protected float health;

        #endregion

        #region Initializer Methods

        /// <summary>
        /// Creates a Turret at the given location, facing the given rotation,
        /// scaled to the given amount, with the given TurretHead mounted on
        /// top, and with a collision sphere of the given size.
        /// </summary>
        /// <param name="location">Location of the Turret</param>
        /// <param name="rotation">Rotation the Turret is facing</param>
        /// <param name="scale">Scale of the Turret</param>
        /// <param name="head">TurretHead to be placed on top</param>
        /// <param name="colliderSize">Size of the collison Sphere</param>
        public Turret(Vector3 location, Vector3 rotation, Vector3 scale, TurretHead head, float colliderSize)
            : base(location, rotation, scale)
        {
            this.head = head;
            this.colliderSize = colliderSize;
            this.children.Add(head);

            this.health = 100f;
        }

        #endregion

        #region Getters and Setters

        /// <summary>
        /// The Head mounted on the Turret.
        /// </summary>
        public TurretHead Head
        {
            get { return this.head; }
        }

        /// <summary>
        /// The size of the collider sphere.
        /// </summary>
        public float ColliderSize
        {
            get { return this.colliderSize; }
        }

        /// <summary>
        /// The Turrets current health value.
        /// </summary>
        public float Health
        {
            get { return this.health; }
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Detects collisions from incoming Bullets.
        /// </summary>
        /// <param name="collider"></param>
        public override void detectCollision(Prefab collider)
        {
            base.detectCollision(collider);

            float damage = 0f;

            if (collider is Bullet)
            {
                Bullet collidingBullet = (Bullet)collider;

                if (collidingBullet.Owner != this)
                {
                    damage = collidingBullet.Damage;
                }
            }
            else if (collider is Tank)
            {
                damage = Settings.TANK_COLLIDE_DAMAGE;
            }

            health -= damage;

            if (health <= 0)
            {
                this.health = 0;
                this.alive = false;
                // TODO: Add death animation script
            }
        }

        #endregion
    }
}
