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

        private float colliderSize = 1f;

        private int score = 0;

        private float health = 100f;

        #endregion

        #region Initializer

        public Turret(Vector3 location, Vector3 rotation, Vector3 scale, TurretHead head)
            : base(location, rotation, scale)
        {
            this.head = head;
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

        public int Score
        {
            get { return this.score; }
            set { this.score = value; }
        }

        #endregion

        #region Public Methods

        // this detects and handles collisions for the tank
        public override void detectCollision(Prefab collider)
        {
            base.detectCollision(collider);

            if (collider is Bullet)
            {
                if (this.health <= 0)
                    this.health = 0;
                else
                    this.health -= ((Bullet)collider).Damage;

                // for debugging
                System.Console.WriteLine(this.health);
                // TODO: Add death animation script
            }
        }

        #endregion
    }
}
