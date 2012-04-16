using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ClearSkies.Managers;
using ClearSkies.Prefabs.Bullets;
using ParticleEngine;
using Microsoft.DirectX;
using ClearSkies.Content;
using ClearSkies.Scripts;

namespace ClearSkies.Prefabs.Enemies.Tanks
{
    abstract class Tank : Enemy
    {
        public Tank(Model tankModel, Vector3 location, Vector3 rotation, Vector3 scale, float colliderSize)
            : base(location, rotation, scale, colliderSize)
        {
            this.models.Add(tankModel);
        }

        #region Public Methods

        // this detects and handles collisions for the tank
        public override void detectCollision(Prefab collider)
        {
            base.detectCollision(collider);

            if (collider is Bullet)
            {
                this.scripts.Clear();
                this.alive = false;
                ParticleEmitterManager.spawnParticleEmitter(ParticleEmitterType.explosion, location);
            }
        }

        #endregion
    }
}
