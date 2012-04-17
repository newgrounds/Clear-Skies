using ClearSkies.Content;
using ClearSkies.Scripts;

namespace ClearSkies.Prefabs.Bullets
{
    /// <summary>
    /// A simple Bullet with no special effects.
    /// </summary>
    class BasicBullet : Bullet
    {
        #region Initializer Methods

        /// <summary>
        /// Creates a BasicBullet object at the given location facing the given rotation.
        /// Bullet will travel forward and use standard collisions.
        /// </summary>
        /// <param name="owner">The Prefab that spawned the bullet</param>
        public BasicBullet(Prefab owner) : base(owner, ContentLoader.BasicBulletModel, 
            Settings.BASIC_BULLET_DAMAGE, Settings.BASIC_BULLET_LIFESPAN, Settings.BASIC_BULLET_SPEED)
        {
            this.scripts.Add(new BulletStraightMovementScript(this));
            this.scripts.Add(new BulletPlainCollisionScript(this));
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Removes Bullet from game if collider is not from its owner.
        /// </summary>
        /// <param name="collider">Prefab collided with</param>
        public override void detectCollision(Prefab collider)
        {
            base.detectCollision(collider);
            this.alive = collider == this.owner;
        }

        #endregion
    }
}
