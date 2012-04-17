using ClearSkies.Content;
using Microsoft.DirectX;
using Microsoft.DirectX.DirectInput;

namespace ClearSkies.Prefabs.Turrets
{
    /// <summary>
    /// A Turret for testing that contains a twin pair of gun barrels.
    /// </summary>
    class TestTurret : Turret
    {
        #region Initializer Methods

        /// <summary>
        /// Creates a TestTurret at the given location, facing the given
        /// rotation, scaled to the given amount, and controlled with the given
        /// keyboard.
        /// </summary>
        /// <param name="location">Location of the TestTurret</param>
        /// <param name="rotation">Rotation the TestTurret is facing</param>
        /// <param name="scale">Scale of the TestTurret</param>
        /// <param name="keyboard">
        /// Keyboard Device that controlls the TestTurret
        /// </param>
        public TestTurret(Vector3 location, Vector3 rotation, Vector3 scale, Device keyboard)
            : base(location, rotation, scale, new TestTurretHead(location, rotation, scale, keyboard), Settings.TEST_TURRET_COLLIDER_SIZE)
        {
            this.models.Add(ContentLoader.TestTurretBaseModel);
        }

        #endregion
    }
}