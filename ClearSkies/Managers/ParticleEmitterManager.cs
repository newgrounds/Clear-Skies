using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ParticleEngine;
using Microsoft.DirectX;
using ClearSkies.Content;
using Microsoft.DirectX.Direct3D;
using ClearSkies.Prefabs;
using ClearSkies.Exceptions;
using ClearSkies.Properties;

namespace ClearSkies.Managers
{
    class ParticleEmitterManager : Manager
    {
        #region Fields

        private static List<ParticleEmitter> managedParticleEmitters = new List<ParticleEmitter>();
        private static bool initialized;
        private static Prefab camera;

        #endregion

        #region Initializer Methods

        public ParticleEmitterManager(Prefab playersCamera)
        {
            camera = playersCamera;
            initialized = true;
        }

        #endregion

        #region Public Static Methods

        public static ParticleEmitter spawnParticleEmitter(ParticleEmitterType type, Vector3 location)
        {
            checkIfInitialized();
            ParticleEmitter spawnedParticleEmitter = null;

            switch (type)
            {
                case ParticleEmitterType.explosion:
                    spawnedParticleEmitter = new ExplosionParticleEmitter(ContentLoader.ExplosionParticleTexture, location, Settings.EXPLOSION_PARTICLE_ACCELERATION,
                        Settings.EXPLOSION_PARTICLES, Settings.EXPLOSION_PARTICLE_SIZE, Settings.EXPLOSION_PARTICLE_GROWTH_RATE, Settings.EXPLOSION_PARTICLE_LIFESPAN);
                    break;
                case ParticleEmitterType.stream:
                    spawnedParticleEmitter = new StreamParticleEmitter(ContentLoader.CloudParticleTexture, location, Settings.SMOKE_PARTICLE_ACCELERATION,
                        Settings.SMOKE_PARTICLE_SIZE, Settings.SMOKE_PARTICLE_GROWTH_RATE, Settings.SMOKE_PARTICLE_LIFESPAN, Settings.SMOKE_PARTICLE_PARTICLES_PER_SECOND);
                    break;
            }

            if (spawnedParticleEmitter != null)
            {
                managedParticleEmitters.Add(spawnedParticleEmitter);
            }

            return spawnedParticleEmitter;
        }

        #endregion

        #region Private Static Methods

        private static void checkIfInitialized()
        {
            if (!initialized)
            {
                throw new UninitializedException(Resources.Particle_Emitter_Manager_Uninitialized_Exception);
            }
        }

        #endregion

        #region Public Methods

        public void update(float deltaTime)
        {
            for (int i = 0; i < managedParticleEmitters.Count; i++)
            {
                if (managedParticleEmitters[i].Alive)
                {
                    managedParticleEmitters[i].update(deltaTime);
                }
                else
                {
                    managedParticleEmitters.RemoveAt(i);
                }
            }
        }

        public void draw(Device device)
        {
            foreach (ParticleEmitter managedParticleEmitter in managedParticleEmitters)
            {
                managedParticleEmitter.draw(camera.Location, camera.Rotation, device);
            }
        }

        #endregion
    }
}
