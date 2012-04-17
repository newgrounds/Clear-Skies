using ClearSkies.Content;
using Microsoft.DirectX;
using Microsoft.DirectX.DirectInput;

namespace ClearSkies.Prefabs.Turrets
{
    /// <summary>
    /// The head of a Basic Turret with one gun barrel.
    /// </summary>
    class BasicTurretHead : TurretHead
    {
        #region Initializer Methods

        /// <summary>
        /// Creates a BasicTurretHead to be used with the BasicTurret at the
        /// give location, facing the given rotation, scaled to the given 
        /// amount, and controlled with the given keyboard.
        /// </summary>
        /// <param name="location">Location of the Turret</param>
        /// <param name="rotation">Rotation the Turret is facing</param>
        /// <param name="scale">Scale of the Turret</param>
        /// <param name="keyboard">Keyboard that controlls the Turret</param>
        public BasicTurretHead(Vector3 location, Vector3 rotation, Vector3 scale, Device keyboard)
            : base(location + Settings.BASIC_TURRET_HEAD_OFFSET, rotation, scale, Settings.BASIC_TURRET_ROTATION_SPEED, keyboard)
        {
            this.models.Add(ContentLoader.BasicTurretHeadModel);

            addChild(new TurretBarrel(
                new Vector3(this.location.X, this.location.Y, this.location.Z),
                new Vector3(this.rotation.X, this.rotation.Y, this.rotation.Z) + Settings.BASIC_TURRET_BARREL_DEFAULT_ROTATION,
                scale,
                ContentLoader.BasicTurretBarrelModel,
                keyboard
                ));
        }

        #endregion
    }
}
