using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.DirectX;
using Microsoft.DirectX.Direct3D;
using System.Drawing;

namespace ParticleEngine
{
    public abstract class ParticleEmitter
    {
        protected bool alive;
        protected List<ParticleData> particleList;
        protected Texture particleTexture;
        protected Random randomizer;
        protected Vector3 location;

        public ParticleEmitter(Texture particleTexture, Vector3 location)
        {
            this.randomizer = new Random();
            initialize(particleTexture, location);
        }

        public ParticleEmitter(Texture particleTexture, Vector3 location, int seed)
        {
            this.randomizer = new Random(seed);
            initialize(particleTexture, location);
        }

        private void initialize(Texture particleTexture, Vector3 location)
        {
            this.particleList = new List<ParticleData>();
            this.particleTexture = particleTexture;
            this.location = location;
            this.alive = true;
        }

        protected void addParticle(Vector3 location, Vector3 acceleration, float size, float growthRate, float lifespan)
        {
            ParticleData particle = new ParticleData();
            particle.spawnLocation = location;
            particle.location = location;
            particle.growthRate = growthRate;

            particle.timeAlive = 0f;
            particle.lifespan = lifespan;
            particle.size = size;
            particle.modColor = Color.White;

            float particleDistance = (float)randomizer.NextDouble() * size;
            Vector4 displacement = new Vector4(particleDistance, 0f, 0f, 0f);
            displacement = Vector4.Transform(displacement,
                Matrix.RotationYawPitchRoll(
                    (float)(2 * Math.PI * randomizer.Next(360) / 180f),
                    (float)(2 * Math.PI * randomizer.Next(360) / 180f),
                    (float)(2 * Math.PI * randomizer.Next(360) / 180f)));

            particle.rotation = 2.0f * new Vector3(displacement.X, displacement.Y, displacement.Z);
            particle.acceleration = acceleration;

            particleList.Add(particle);
        }

        public virtual void update(float deltaTime)
        {
            for (int i = 0; i < particleList.Count; i++)
            {
                ParticleData particle = particleList[i];
                particle.timeAlive += deltaTime;

                if (particle.timeAlive > particle.lifespan)
                {
                    particleList.RemoveAt(i);
                }
                else
                {
                    //particle.location = 0.5f * particle.acceleration * relativeTime * relativeTime +
                    //    particle.rotation * relativeTime + particle.spawnLocation;
                    particle.location =
                        new Vector3(particle.acceleration.X * particle.rotation.X,
                            particle.acceleration.Y * particle.rotation.Y,
                            particle.acceleration.Z * particle.rotation.Z)
                            * deltaTime + particle.location;

                    Vector3 positionFromCenter = particle.location - particle.spawnLocation;
                    particle.size += deltaTime * particle.growthRate;

                    int shadingTint = (int)((1.0f - particle.timeAlive / particle.lifespan) * 255);
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
        }

        private void sortParticles(List<ParticleData> particles, int left, int right, Vector3 cameraLocation)
        {
            int i = left;
            int j = right;
            ParticleData pivot = particles[(left + right) / 2];

            while (i <= j)
            {
                float p1 = (particles[i].location - cameraLocation).Length();
                float p2 = (pivot.location - cameraLocation).Length();

                while ((particles[i].location - cameraLocation).Length() > p2)
                {
                    i++;
                }

                while ((particles[j].location - cameraLocation).Length() < p2)
                {
                    j--;
                }

                if (i <= j)
                {
                    // Swap
                    ParticleData tmp = particles[i];
                    particles[i] = particles[j];
                    particles[j] = tmp;

                    i++;
                    j--;
                }
            }

            // Recursive calls
            if (left < j)
            {
                sortParticles(particles, left, j, cameraLocation);
            }

            if (i < right)
            {
                sortParticles(particles, i, right, cameraLocation);
            }
        }

        public void draw(Vector3 cameraLocation, Vector3 cameraRotation, Device device)
        {
            if (particleList.Count > 0)
            {
                sortParticles(particleList, 0, particleList.Count - 1, cameraLocation);
            }

            device.RenderState.AlphaBlendEnable = true;
            device.RenderState.SourceBlend = Blend.SourceAlpha;
            device.RenderState.DestinationBlend = Blend.InvSourceAlpha; 

            device.SetTexture(0, particleTexture);

            device.TextureState[0].AlphaOperation = TextureOperation.Modulate;
            device.TextureState[0].AlphaArgument0 = TextureArgument.Current;
            device.TextureState[0].AlphaArgument1 = TextureArgument.Diffuse;
            device.TextureState[0].AlphaArgument2 = TextureArgument.TextureColor;

            device.TextureState[0].ColorArgument0 = TextureArgument.Current;
            device.TextureState[0].ColorArgument1 = TextureArgument.Diffuse;
            device.TextureState[0].ColorArgument2 = TextureArgument.TextureColor;
            device.TextureState[0].ColorOperation = TextureOperation.Modulate;

            foreach (ParticleData particle in particleList)
            {
                device.Transform.World =
                    Matrix.RotationYawPitchRoll(cameraRotation.X, cameraRotation.Y, cameraRotation.Z) *
                    Matrix.Translation(particle.location);

                Material material = device.Material;
                Color diffuse = material.Diffuse;
                material.Diffuse = particle.modColor;
                material.Emissive = Color.White;
                device.Material = material;

                VertexBuffer buffer = new VertexBuffer(typeof(CustomVertex.PositionNormalTextured), 4, device, 0, CustomVertex.PositionNormalTextured.Format, Pool.Default);
                CustomVertex.PositionNormalTextured[] vertices = (CustomVertex.PositionNormalTextured[])buffer.Lock(0, 0);

                float particleRadius = particle.size / 2;

                vertices[0] = new CustomVertex.PositionNormalTextured(new Vector3(-particleRadius, -particleRadius, 0f), new Vector3(0, 1, 0), 0, 1); // bottom right
                vertices[1] = new CustomVertex.PositionNormalTextured(new Vector3(-particleRadius, particleRadius, 0f), new Vector3(0, 1, 0), 0, 0); // top right
                vertices[2] = new CustomVertex.PositionNormalTextured(new Vector3(particleRadius, -particleRadius, 0f), new Vector3(0, 1, 0), 1, 1); // bottom left
                vertices[3] = new CustomVertex.PositionNormalTextured(new Vector3(particleRadius, particleRadius, 0), new Vector3(0, 1, 0), 1, 0); // top left

                buffer.Unlock();

                device.VertexFormat = CustomVertex.PositionNormalTextured.Format;
                device.SetStreamSource(0, buffer, 0);
                device.DrawPrimitives(PrimitiveType.TriangleStrip, 0, 2);
            }
        }
        
        public virtual bool Alive
        {
            get { return this.alive; }
            set { this.alive = false; }
        }

        public Vector3 Location
        {
            get { return location; }
            set { location = value; }
        }
    }
}
