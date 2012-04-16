using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.DirectX;
using Microsoft.DirectX.Direct3D;

namespace ParticleEngine
{
    public class StreamParticleEmitter : ParticleEmitter
    {
        private Vector3 particleAcceleration;
        private float particleInitialSize;
        private float particleGrowthRate;
        private float particleLifespan;
        private float particlesPerSecond;
        private float timeSinceLastParticleSpawn;
        private bool emit;

        public StreamParticleEmitter(Texture particleTexture, Vector3 location, Vector3 particleAcceleration, float particleInitialSize,
            float particleGrowthRate, float particleLifespan, float particlesPerSecond)
            : base(particleTexture, location)
        {
            initialize(particleAcceleration, particleInitialSize, particleGrowthRate, particleLifespan, particlesPerSecond);
        }

        public StreamParticleEmitter(Texture particleTexture, Vector3 location, Vector3 particleAcceleration, float particleInitialSize,
            float particleGrowthRate, float particleLifespan, float particlesPerSecond, int seed)
            : base(particleTexture, location, seed)
        {
            initialize(particleAcceleration, particleInitialSize, particleGrowthRate, particleLifespan, particlesPerSecond);
        }

        private void initialize(Vector3 particleAcceleration, float particleInitialSize,
            float particleGrowthRate, float particleLifespan, float particlesPerSecond)
        {
            this.particleAcceleration = particleAcceleration;
            this.particleInitialSize = particleInitialSize;
            this.particleGrowthRate = particleGrowthRate;
            this.particleLifespan = particleLifespan;
            this.particlesPerSecond = particlesPerSecond;
            this.timeSinceLastParticleSpawn = 0f;
            this.emit = true;
        }

        public override void update(float deltaTime)
        {
            base.update(deltaTime);

            if (emit)
            {
                if (timeSinceLastParticleSpawn > 1f / particlesPerSecond)
                {
                    addParticle(location, particleAcceleration, particleInitialSize, particleGrowthRate, particleLifespan);
                    timeSinceLastParticleSpawn = 0f;
                }
                else
                {
                    timeSinceLastParticleSpawn += deltaTime;
                }
            }
            else
            {
                alive = this.particleList.Count > 0;
            }
        }

        public bool Emit
        {
            set { this.emit = value; }
        }
    }
}
