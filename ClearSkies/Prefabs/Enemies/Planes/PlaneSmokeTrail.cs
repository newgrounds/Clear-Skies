using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ParticleEngine;
using ClearSkies.Managers;
using Microsoft.DirectX;

namespace ClearSkies.Prefabs.Enemies.Planes
{
    class PlaneSmokeTrail : Prefab
    {
        private StreamParticleEmitter streamParticleEmitter;

        public PlaneSmokeTrail(Vector3 location) : base(location, Vector3.Empty, Vector3.Empty)
        {
            streamParticleEmitter = (StreamParticleEmitter)ParticleEmitterManager.spawnParticleEmitter(ParticleEmitterType.stream, location);
        }

        public override Microsoft.DirectX.Vector3 Location
        {
            get
            {
                return base.Location;
            }
            set
            {
                base.Location = value;
                streamParticleEmitter.Location = value;
            }
        }

        public void stop()
        {
            streamParticleEmitter.Emit = false;
        }
    }
}
