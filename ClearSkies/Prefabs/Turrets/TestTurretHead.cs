using ClearSkies.Content;
using Microsoft.DirectX;
using Microsoft.DirectX.DirectInput;

namespace ClearSkies.Prefabs.Turrets
{
    /// <summary>
    /// A simple dual barreled TurretHead used for testing.
    /// </summary>
    class TestTurretHead : TurretHead
    {
        #region Initializer Methods

        /// <summary>
        /// Creates a simple twin barreled TurretHead at the given location 
        /// facing the given rotation and controlled by the given kayboard 
        /// Device.
        /// </summary>
        /// <param name="location">Location of the TurretHead</param>
        /// <param name="rotation">Rotation the TurretHead is facing</param>
        /// <param name="scale">Scale of the TurretHead</param>
        /// <param name="keyboard">
        /// Keyboard Device used to controll the Turret
        /// </param>
        public TestTurretHead(Vector3 location, Vector3 rotation, Vector3 scale, Device keyboard)
            : base(location + Settings.TEST_TURRET_HEAD_OFFSET, rotation, scale, Settings.TEST_TURRET_ROTATION_SPEED, keyboard)
        {
            this.models.Add(ContentLoader.TestTurretHeadModel);

            addChild(new TurretBarrel(
                new Vector3(this.location.X, this.location.Y, this.location.Z) + Settings.TEST_TURRET_BARREL_ONE_OFFSET,
                new Vector3(this.rotation.X, this.rotation.Y, this.rotation.Z) + Settings.TEST_TURRET_BARREL_DEFAULT_ROTATION,
                scale,
                ContentLoader.TestTurretBarrelModel,
                keyboard
                ));

            addChild(new TurretBarrel(
                new Vector3(this.location.X, this.location.Y, this.location.Z) + Settings.TEST_TURRET_BARREL_TWO_OFFSET,
                new Vector3(this.rotation.X, this.rotation.Y, this.rotation.Z) + Settings.TEST_TURRET_BARREL_DEFAULT_ROTATION,
                scale,
                ContentLoader.TestTurretBarrelModel,
                keyboard
                ));
        }

        #endregion
    }
}
