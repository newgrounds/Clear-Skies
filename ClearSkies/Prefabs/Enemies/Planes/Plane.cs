﻿using ClearSkies.Content;
using ClearSkies.Managers;
using ClearSkies.Prefabs.Bullets;
using Microsoft.DirectX;
using ParticleEngine;

namespace ClearSkies.Prefabs.Enemies.Planes
{
    /// <summary>
    /// Creates a Plane Enemy that will be above the user.
    /// </summary>
    class Plane : Enemy
    {
        #region Fields

        private float flightSpeed;
        private float turnSpeed;

        #endregion

        #region Initializer Methods

        /// <summary>
        /// Creates a Plane Prefab that is represented with the given model, at
        /// the given locaiton, facing the given rotation, scaled to the give
        /// size, and with the specified collider size.
        /// </summary>
        /// <param name="model">Model that represents the Plane</param>
        /// <param name="location">Location of the Plane</param>
        /// <param name="rotation">Rotaiton the Plane is facing</param>
        /// <param name="scale">Scale of the Plane</param>
        /// <param name="colliderSize">Size of the collider sphere</param>
        public Plane(Model model, Vector3 location, Vector3 rotation, Vector3 scale, float flightSpeed, float turnSpeed, float colliderSize) : 
            base(location, rotation, scale, colliderSize)
        {
            this.flightSpeed = flightSpeed;
            this.turnSpeed = turnSpeed;
            this.models.Add(model);

            this.children.Add(new PlaneSmokeTrail(location));
        }

        #endregion

        #region Getter and Setter Methods

        /// <summary>
        /// The speed the Plane can fly at.
        /// </summary>
        public float FlightSpeed
        {
            get { return this.flightSpeed; }
        }

        /// <summary>
        /// The speed the Plane can turn at.
        /// </summary>
        public float TurnSpeed
        {
            get { return this.turnSpeed; }
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Creates an explosion if the collider is a Bullet object.
        /// </summary>
        /// <param name="collider">The Prefab that was collided with</param>
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

                    foreach (Prefab child in children)
                    {
                        if (child is PlaneSmokeTrail)
                        {
                            PlaneSmokeTrail smokeTrail = (PlaneSmokeTrail)child;
                            smokeTrail.stop();
                        }
                    }
                }
            }
        }

        #endregion
    }
}