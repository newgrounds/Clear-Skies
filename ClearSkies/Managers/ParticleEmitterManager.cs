using System.Collections.Generic;
using ClearSkies.Content;
using ClearSkies.Exceptions;
using ClearSkies.Prefabs;
using ClearSkies.Properties;
using Microsoft.DirectX;
using Microsoft.DirectX.Direct3D;
using ParticleEngine;

namespace ClearSkies.Managers
{
    class ParticleEmitterManager : Manager
    {
        #region Fields

        private static List<ParticleEmitter> managedParticleEmitters;
        private static bool initialized;
        private static Prefab camera;

        #endregion

        #region Initializer Methods

        /// <summary>
        /// Initailizes all data for use in the ParticleEmitterManager.
        /// </summary>
        /// <param name="playersCamera">The Camera that Particles will be 
        /// viewed from</param>
        public ParticleEmitterManager(Prefab playersCamera)
        {
            camera = playersCamera;
            managedParticleEmitters = new List<ParticleEmitter>();
            initialized = true;
        }

        #endregion

        #region Public Static Methods

        /// <summary>
        /// Spawns a ParticleEmitter of the specified type at the specified 
        /// location.
        /// </summary>
        /// <param name="type">The type ParticleEmitter to spawn</param>
        /// <param name="location">The location to spawn the ParticleEmitter 
        /// at</param>
        /// <returns>Returns a reference to the spawned ParticleEmitter
        /// </returns>
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

        /// <summary>
        /// Checks if the ParticleEmitterManager was initialized. This should 
        /// be called at the beginning of every public static method.
        /// </summary>
        private static void checkIfInitialized()
        {
            if (!initialized)
            {
                throw new UninitializedException(Resources.Particle_Emitter_Manager_Uninitialized_Exception);
            }
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Updates the managed ParticleEmitters and removes them if they no 
        /// longer meet the requirement of being alive.
        /// </summary>
        /// <param name="deltaTime">
        /// The time in seconds since last update
        /// </param>
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

        /// <summary>
        /// Draws the particle emitters to the screen.
        /// </summary>
        /// <param name="device">The Device to draw the particles to</param>
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
