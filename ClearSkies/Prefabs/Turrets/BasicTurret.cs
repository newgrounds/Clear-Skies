using ClearSkies.Content;
using Microsoft.DirectX;
using Microsoft.DirectX.DirectInput;

namespace ClearSkies.Prefabs.Turrets
{
    /// <summary>
    /// A Turret with a single gun barrel.
    /// </summary>
    class BasicTurret : Turret
    {
        #region Initializer Methods

        /// <summary>
        /// Creates a Basic Turret at the given location, facing the given
        /// rotation, scaled to the given amount, controlled by the given 
        /// keyboard.
        /// </summary>
        /// <param name="location">Locaiton of the Turret</param>
        /// <param name="rotation">Rotation the Turret is facing</param>
        /// <param name="scale">Scale of the Turret</param>
        /// <param name="keyboard">Keyboard that controlls the Turret</param>
        public BasicTurret(Vector3 location, Vector3 rotation, Vector3 scale, Device keyboard) : base(location, rotation, scale,
            new BasicTurretHead(location, rotation, scale, keyboard), Settings.BASIC_TURRET_COLLIDER_SIZE)
        {
            this.models.Add(ContentLoader.BasicTurretBaseModel);
        }

        #endregion
    }
}