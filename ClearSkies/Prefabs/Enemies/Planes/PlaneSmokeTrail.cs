using ClearSkies.Managers;
using Microsoft.DirectX;
using ParticleEngine;

namespace ClearSkies.Prefabs.Enemies.Planes
{
    /// <summary>
    /// Wrapper Prefab for the a StreamParticleEmitter.
    /// </summary>
    class PlaneSmokeTrail : Prefab
    {
        #region Fields

        private StreamParticleEmitter streamParticleEmitter;

        #endregion

        #region Initializer Methods

        /// <summary>
        /// Creates a PlaneSmokeTrail at the given locaiton.
        /// </summary>
        /// <param name="location">Location of the PlaneSmokeTrail</param>
        public PlaneSmokeTrail(Vector3 location) : base(location, Vector3.Empty, Vector3.Empty)
        {
            streamParticleEmitter = (StreamParticleEmitter)ParticleEmitterManager.spawnParticleEmitter(ParticleEmitterType.stream, location);
        }

        #endregion

        #region Getter and Setter Methods

        /// <summary>
        /// The location of the PlaneSmokeTrail.
        /// </summary>
        public override Vector3 Location
        {
            get { return base.Location; }
            set
            {
                base.Location = value;
                streamParticleEmitter.Location = value;
            }
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Stops the PlaneSmokeTrail from emitting Particles.
        /// </summary>
        public void stop()
        {
            streamParticleEmitter.Emit = false;
        }

        #endregion
    }
}