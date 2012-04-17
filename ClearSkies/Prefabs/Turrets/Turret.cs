using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.DirectX;
using ClearSkies.Prefabs.Bullets;

namespace ClearSkies.Prefabs.Turrets
{
    abstract class Turret : Prefab
    {
        #region Fields

        protected TurretHead head;
        protected float colliderSize = 1.0f;
        protected float health = 100f;

        public Turret(Vector3 location, Vector3 rotation, Vector3 scale, TurretHead head, float colliderSize)
            : base(location, rotation, scale)
        {
            this.head = head;
            this.colliderSize = colliderSize;
            this.children.Add(head);
        }

        #endregion

        #region Getters and Setters

        public TurretHead Head
        {
            get { return this.head; }
        }

        /// <summary>
        /// The size of the spherical collider for this Enemy.
        /// </summary>
        public float ColliderSize
        {
            get { return this.colliderSize; }
        }

        public float Health
        {
            get { return this.health; }
        }

        #endregion

        #region Public Methods

        // this detects and handles collisions for the tank
        public override void detectCollision(Prefab collider)
        {
            base.detectCollision(collider);

            if (collider is Bullet)
            {
                if (health <= 0)
                    health = 0;
                else
                    health -= ((Bullet)collider).Damage;

                // for debugging
                //System.Console.WriteLine(health);
                // TODO: Add death animation script
            }
        }

        #endregion
    }
}
