using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using Microsoft.DirectX;
using Microsoft.DirectX.Direct3D;

namespace ParticleEngine
{
    public class ExpolsionParticleEmitter : ParticleEmitter
    {
        #region Fields

        private List<ParticleData> particleList;
        private Random randomizer;
        private Texture particleTexture;

        private float timeAlive;
        private float lifespan;
        private Color fade;

        #endregion

        #region Initializer Methods

        public ExpolsionParticleEmitter(Texture particleTexture, Vector3 location, Vector3 acceleration, int amountOfParticles, float size, float growthRate, float lifespan)
        {
            this.randomizer = new Random();
            initialize(particleTexture, location, acceleration, amountOfParticles, size, growthRate, lifespan);
        }

        public ExpolsionParticleEmitter(Texture particleTexture, Vector3 location, Vector3 acceleration, int amountOfParticles, float size, float growthRate, float lifespan, int seed)
        {
            this.randomizer = new Random(seed);
            initialize(particleTexture, location, acceleration, amountOfParticles, size, growthRate, lifespan);
        }

        private void initialize(Texture particleTexture, Vector3 location, Vector3 acceleration, int amountOfParticles, float size, float growthRate, float lifespan)
        {
            this.particleList = new List<ParticleData>();
            this.particleTexture = particleTexture;
            this.lifespan = lifespan;

            for (int i = 0; i < amountOfParticles; i++)
            {
                addParticle(location, acceleration, size, growthRate, lifespan);
            }
        }

        private void addParticle(Vector3 location, Vector3 acceleration, float size, float growthRate, float lifespan)
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

        #endregion

        #region Public Methods

        public void updateParticles(float deltaTime)
        {
            timeAlive += deltaTime;
            float relativeTime = timeAlive / lifespan;
            float inverseTime = 1.0f - relativeTime;
            int shadingTint = (int)(inverseTime * 255);
            if (shadingTint >= 0)
            {
                this.fade = Color.FromArgb(shadingTint, 0, 0, 0);
            }
            else
            {
                this.fade = Color.FromArgb(0, 0, 0, 0);
            }

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

                    particleList[i] = particle;
                }
            }
        }

        public void draw(Vector3 cameraRotation, Device device)
        {
            device.SetTexture(0, particleTexture);
            
            device.TextureState[0].AlphaOperation = TextureOperation.Modulate;
            device.TextureState[0].AlphaArgument0 = TextureArgument.Current;
            device.TextureState[0].AlphaArgument1 = TextureArgument.Diffuse;
            device.TextureState[0].AlphaArgument2 = TextureArgument.TextureColor;

            device.TextureState[0].ColorArgument0 = TextureArgument.Current;
            device.TextureState[0].ColorArgument1 = TextureArgument.Diffuse;
            device.TextureState[0].ColorArgument2 = TextureArgument.TextureColor;
            device.TextureState[0].ColorOperation = TextureOperation.Modulate;

            Material material = device.Material;
            Color diffuse = material.Diffuse;
            material.Diffuse = fade;
            material.Emissive = Color.White;
            device.Material = material;

            foreach (ParticleData particle in particleList)
            {
                device.Transform.World = 
                    Matrix.RotationYawPitchRoll(cameraRotation.X, cameraRotation.Y, cameraRotation.Z) *
                    Matrix.Translation(particle.location);

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

        #endregion
    }
}
