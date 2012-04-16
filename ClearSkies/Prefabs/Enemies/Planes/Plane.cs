using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.DirectX;
using ClearSkies.Content;
using ClearSkies.Prefabs.Bullets;
using ClearSkies.Managers;
using ParticleEngine;

namespace ClearSkies.Prefabs.Enemies.Planes
{
    class Plane : Enemy
    {
        public Plane(Model model, Vector3 location, Vector3 rotation, Vector3 scale, float colliderSize) : 
            base(location, rotation, scale, colliderSize)
        {
            this.models.Add(model);
            this.children.Add(new PlaneSmokeTrail(location));
        }

        public override void detectCollision(Prefab collider)
        {
            base.detectCollision(collider);

            if (collider is Bullet)
            {
                this.scripts.Clear();
                this.alive = false;
                ParticleEmitterManager.spawnParticleEmitter(ParticleEmitterType.explosion, location);
                // TODO: Add death animation script

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
}
