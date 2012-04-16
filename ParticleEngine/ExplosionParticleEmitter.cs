using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using Microsoft.DirectX;
using Microsoft.DirectX.Direct3D;

namespace ParticleEngine
{
    public class ExplosionParticleEmitter : ParticleEmitter
    {
        #region Fields

        private float timeAlive;
        private float lifespan;

        #endregion

        #region Initializer Methods

        public ExplosionParticleEmitter(Texture particleTexture, Vector3 location, Vector3 acceleration, int amountOfParticles, float size, float growthRate, float lifespan) :
            base(particleTexture, location)
        {
            this.randomizer = new Random();
            initialize(amountOfParticles, location, acceleration, size, growthRate, lifespan);
        }

        public ExplosionParticleEmitter(Texture particleTexture, Vector3 location, Vector3 acceleration, int amountOfParticles, float size, float growthRate, float lifespan, int seed) :
            base(particleTexture, location, seed)
        {
            this.randomizer = new Random(seed);
            initialize(amountOfParticles, location, acceleration, size, growthRate, lifespan);
        }

        private void initialize(int amountOfParticles, Vector3 location, Vector3 acceleration, float size, float growthRate, float lifespan)
        {
            this.lifespan = lifespan;
            this.timeAlive = 0;

            for (int i = 0; i < amountOfParticles; i++)
            {
                base.addParticle(location, acceleration, size, growthRate, lifespan);
            }
        }

        #endregion

        #region Getter and Setter Methods

        public override bool Alive
        {
            get { return timeAlive < lifespan; }
        }

        #endregion

        #region Public Methods

        public override void update(float deltaTime)
        {
            base.update(deltaTime);

            this.timeAlive += deltaTime;
            int shadingTint = (int)((1.0f - timeAlive / lifespan) * 255);

            for (int i = 0; i < particleList.Count; i++)
            {
                ParticleData particle = particleList[i];

                if (shadingTint >= 0)
                {
                    particle.modColor = Color.FromArgb(shadingTint, 0, 0, 0);
                }
                else
                {
                    particle.modColor = Color.FromArgb(0, 0, 0, 0);
                }

                particleList[i] = particle;
            }
        }   

        #endregion
    }
}
